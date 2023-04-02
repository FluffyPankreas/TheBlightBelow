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

            SetDrawHeaderCallback();
            SetDrawElementCallback();

            SetOnAddCallback();
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

                    // Define the section for "Name".
                    EditorGUI.PropertyField(
                        new Rect(
                            rect.x,
                            rect.y,
                            nameWidth,
                            EditorGUIUtility.singleLineHeight),
                        element.FindPropertyRelative("Name"),
                        GUIContent.none
                    );

                    // Define the section for "Number"
                    EditorGUI.PropertyField(
                        new Rect(
                            rect.x + nameWidth + horizontalPadding,
                            rect.y + verticalPadding,
                            numberWidth,
                            EditorGUIUtility.singleLineHeight),
                        element.FindPropertyRelative("Number"),
                        GUIContent.none
                    );

                };
        }

        private void SetOnAddCallback()
        {
            _artifactEffects.onAddCallback = (rl) =>
            {
                var index = rl.serializedProperty.arraySize;
                rl.serializedProperty.arraySize++;
                rl.index = index;
                
                var element = rl.serializedProperty.GetArrayElementAtIndex(index);
                element.FindPropertyRelative("Name").stringValue = "NEW NAME";
                element.FindPropertyRelative("Number").intValue = index * 10;
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
                        CreateEffectCallback,
                        subType
                    );    
                }
                
                menu.ShowAsContext();
            };
        }

        private void CreateEffectCallback(object selectedEffect)
        {
            var index = _artifactEffects.serializedProperty.arraySize;
            _artifactEffects.serializedProperty.arraySize++;
            _artifactEffects.index = index;

            var newElement = _artifactEffects.serializedProperty.GetArrayElementAtIndex(index);
            newElement.FindPropertyRelative("Name").stringValue = "Name assigned here.";
            newElement.FindPropertyRelative("Number").intValue = index * 10;

            serializedObject.ApplyModifiedProperties();
        }
        
        private IEnumerable<Type> GetInheritedClasses(Type abstractType) 
        {
            return Assembly.GetAssembly(abstractType).GetTypes().
                Where(
                    theType => 
                        theType.IsClass &&
                        !theType.IsAbstract &&
                        theType.IsSubclassOf(abstractType)
                        ).ToArray();
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