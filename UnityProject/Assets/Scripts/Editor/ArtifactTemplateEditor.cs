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

            for (int i = 0; i < serializedObject.targetObjects.Length; i++)
            {
                Debug.Log("targetObjects[" + i + "]" + ": " + serializedObject.targetObjects[i]);
                
            }

            SetDrawHeaderCallback();
            SetDrawElementCallback();

            //SetOnAddCallback();
            SetOnDropDownCallback();
        }

        
        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(_artifactName);
            EditorGUILayout.PropertyField(_artifactIcon);
            
            _artifactEffects.DoLayoutList();

            serializedObject.ApplyModifiedProperties();
        }

        private void SetDrawHeaderCallback()
        {
            _artifactEffects.drawHeaderCallback = (Rect rect) => { EditorGUI.LabelField(rect, "Artifact Effects"); };
        }

        private void SetDrawElementCallback()
        {
            int minNameWidth = 50;
            int numberWidth = 30;
            int verticalPadding = 1;
            int horizontalPadding = 5;

            var nameWidth = minNameWidth;

            _artifactEffects.drawElementCallback =
                (Rect rect, int index, bool isActive, bool isFocused) =>
                {
                    if (rect.width - numberWidth > minNameWidth)
                        nameWidth = (int)(rect.width - numberWidth);

                    var element = _artifactEffects.serializedProperty.GetArrayElementAtIndex(index);
                    
                    rect.y += 2;
        
        
                    UnityEditor.Editor editor;
 
                    CreateCachedEditor(element.objectReferenceValue, typeof(CharacterAttributeModiferEditor), ref editor);  
 
                    if (editor != null) {
                        rect.y += EditorGUIUtility.singleLineHeight + 2;
 
                        GUILayout.BeginArea(rect);
                        editor.OnInspectorGUI();
                        GUILayout.EndArea();
                    }

                    
                };
        }

        private void SetOnAddCallback()
        {
            _artifactEffects.onAddCallback = (rl) =>
            {
                var newItem = ScriptableObject.CreateInstance<DamageTestEffect>();
                
                Debug.Log("ScriptableObject: " + newItem);
                newItem.LowDiceValue = 1;
                newItem.HighDiceValue = 6;
                newItem.DiceCount = 2;
                
                serializedObject.FindProperty("artifactEffects").arraySize++;
                var newItemIndex = serializedObject.FindProperty("artifactEffects").arraySize - 1;
                serializedObject.FindProperty("artifactEffects").GetArrayElementAtIndex(newItemIndex).objectReferenceValue = newItem;
                serializedObject.ApplyModifiedProperties();
                
                var property = serializedObject.FindProperty("artifactEffects").GetArrayElementAtIndex(newItemIndex);
                Debug.Log("Property: " + property.objectReferenceValue);
                
                serializedObject.ApplyModifiedProperties();
            };
        }
        private void SetOnRemoveCallback()
        {
            _artifactEffects.onRemoveCallback = (effects) =>
            {
                Debug.Log("Removing an item.");
                if (EditorUtility.DisplayDialog(
                        "Warning!",
                        "Are you sure you want to delete the wave?",
                        "Yes",
                        "No")
                   )
                {
                    ReorderableList.defaultBehaviours.DoRemoveButton(effects);
                }
            };
        }

        private void SetOnDropDownCallback()
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