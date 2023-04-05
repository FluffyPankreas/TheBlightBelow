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
            )
            {
                drawHeaderCallback = rect => { EditorGUI.LabelField(rect, "Artifact Effects"); }
            };

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

                foreach (var effectType in allEffectTypes)
                {
                    menu.AddItem(
                        new GUIContent(TypeName(effectType)),
                        false,
                        CreateEffectCallback,
                        effectType
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
                if (EditorUtility.DisplayDialog(
                        "Warning!",
                        "Are you sure you want to delete this effect?",
                        "Yes",
                        "No")
                   )
                {
                    var index = effects.index;
                    var so = effects.serializedProperty.GetArrayElementAtIndex(index);
                    AssetDatabase.RemoveObjectFromAsset(so.objectReferenceValue);
                    AssetDatabase.SaveAssets();

                    effects.serializedProperty.DeleteArrayElementAtIndex(index);
                    effects.serializedProperty.serializedObject.ApplyModifiedProperties();
                }
            };
        }
        
        /// <summary>
        /// Adds the functionality to draw each effect in the artifact list. 
        /// </summary>
        private void SetArtifactEffectListDrawElementCallback()
        {
            /*
                //Get the SerializedObject for the ObjectInScene instance
                SerializedObject objSerialized = new SerializedObject(objectInScene);

                // Create an instance of the custom editor for ObjectInScene
                ObjectInScene.ObjectInSceneEditor editor = Editor.CreateEditor(objectInScene) as ObjectInScene.ObjectInSceneEditor;

                // Call the OnInspectorGUI method of the custom editor with the SerializedObject argument
                editor.OnInspectorGUI();

                // Apply any changes made to the SerializedObject
                objSerialized.ApplyModifiedProperties();
            */
            _artifactEffects.drawElementCallback = (rect, index, active, focused) =>
            {
                //var currentElement = _artifactEffects.serializedProperty.GetArrayElementAtIndex(index);
                
                var currentEffect = ((ArtifactTemplate)serializedObject.targetObjects[0]).ArtifactEffects[index];
                var editor = CreateEditor(currentEffect) as EffectEditor;
                editor!.OnInspectorGUI();
            };
        }

        private void CreateEffectCallback(object effectType)
        {
            var effects = (serializedObject.targetObjects[0] as ArtifactTemplate)?.ArtifactEffects;

            var newEffectToAdd = ScriptableObject.CreateInstance((Type)effectType);// Create a new instance of the effect.
            newEffectToAdd.name = ((Type)effectType).Name;
            AssetDatabase.AddObjectToAsset(newEffectToAdd, serializedObject.targetObjects[0]);
            AssetDatabase.SaveAssets();
            
            Debug.Log("Created Type: " + newEffectToAdd.GetType());
            effects!.Add((Effect)newEffectToAdd);
            
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