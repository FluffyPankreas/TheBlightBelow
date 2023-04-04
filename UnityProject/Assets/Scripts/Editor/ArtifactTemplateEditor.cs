using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using GameArchitecture;
using ArtifactEffects;

namespace Editor
{
    [CustomEditor(typeof(ArtifactTemplate))]
    public class ArtifactTemplateEditor : UnityEditor.Editor
    {
        private SerializedProperty _artifactName;
        private SerializedProperty _artifactIcon;
        private ReorderableList _artifactEffects;
        
        public void OnEnable()
        {
            _artifactName = serializedObject.FindProperty("artifactName");
            _artifactIcon = serializedObject.FindProperty("artifactIcon");

            _artifactEffects = new ReorderableList(
                serializedObject,
                serializedObject.FindProperty("artifactEffects"),
                true,
                true,
                true,
                true
            );
            
            _artifactEffects.drawHeaderCallback = (Rect rect) => { EditorGUI.LabelField(rect, "Artifact Effects"); };

            SetArtifactEffectListDropDownCallback();
            SetArtifactEffectRemoveCallback();
            SetArtifactEffectListDrawElementCallback();
            
        }

        
        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(_artifactName);
            EditorGUILayout.PropertyField(_artifactIcon);
            
            
            _artifactEffects.DoLayoutList();

            serializedObject.ApplyModifiedProperties();
        }
    
        /// <summary>
        /// Adds the functionality that allows the artifact effect list dropdown to add new effects.
        /// </summary>
        private void SetArtifactEffectListDropDownCallback()
        {
            _artifactEffects.onAddDropdownCallback = (rect, list) =>
            {
                var menu = new GenericMenu();

                var allEffectTypes = GetInheritedClasses(typeof(Effect));

                foreach (var subType in allEffectTypes)
                {
                    menu.AddItem(
                        new GUIContent(TypeName(subType)),
                        false,
                        CreateEffectCallback<DrawAtStartTestEffect>,
                        subType
                    );
                }
                menu.ShowAsContext();
            };
        }
        
        /// <summary>
        /// Adds the functionality that cleans up the effects properly when delete is selected.
        /// </summary>
        private void SetArtifactEffectRemoveCallback()
        {
            _artifactEffects.onRemoveCallback = (effects) =>
            {
                Debug.Log("Removing an item.");
                if (EditorUtility.DisplayDialog(
                        "Warning!",
                        "Are you sure you want to delete this effect?",
                        "Yes",
                        "No")
                   )
                {
                    ReorderableList.defaultBehaviours.DoRemoveButton(effects);
                }
            };
        }
        
        /// <summary>
        /// Adds the functionality to draw each effect in the artifact list. 
        /// </summary>
        private void SetArtifactEffectListDrawElementCallback()
        {
            /*_artifactEffects.drawElementCallback =
                (Rect rect, int index, bool isActive, bool isFocused) =>
                {
                    var element = _artifactEffects.serializedProperty.GetArrayElementAtIndex(index);
                    
                    rect.y += 2;
                };*/
        }

        private void CreateEffectCallback<T>(object selectedEffect) where T : Effect
        {
            var effects = (serializedObject.targetObjects[0] as ArtifactTemplate)?.ArtifactEffects;

            var newEffectToAdd = ScriptableObject.CreateInstance((Type)selectedEffect);// Create a new instance of the effect. 
            AssetDatabase.AddObjectToAsset(newEffectToAdd, serializedObject.targetObjects[0]);
            
            Debug.Log("Created Type: " + newEffectToAdd.GetType());
            effects.Add((Effect)newEffectToAdd);
            
            serializedObject.ApplyModifiedProperties();
        }
        
        private List<Type> GetInheritedClasses(Type abstractType) 
        {
            return Assembly.GetAssembly(abstractType).GetTypes().
                Where(
                    theType => 
                        theType.IsClass &&
                        !theType.IsAbstract &&
                        theType.IsSubclassOf(abstractType)
                        ).ToList();
        }

        private string TypeName(Type type)
        {
            var typeName = type.ToString();
            var lastIndexOfPeriod = typeName.LastIndexOf('.');
            
            if (lastIndexOfPeriod != -1)
            {
                typeName = typeName[(lastIndexOfPeriod + 1)..];
            }
            
            var splitUp = Regex.Split(typeName, @"(?<!^)(?=[A-Z])");

            typeName = "";
            foreach (var word in splitUp)
            {
                typeName += word;
                typeName += " ";
            }

            typeName.Remove(typeName.Length - 1);

            return typeName;
        }


    }
}