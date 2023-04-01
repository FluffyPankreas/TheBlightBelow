using System;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using GameArchitecture;

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

            SetDrawElementCallback();
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(_artifactName);
            EditorGUILayout.PropertyField(_artifactIcon);
            _artifactEffects.DoLayoutList();

            serializedObject.ApplyModifiedProperties();
        }

        private int minNameWidth = 50;
        private int numberWidth = 30;
        private int verticalPadding = 1;
        private int horizontalPadding = 5;

        private void SetDrawElementCallback()
        {
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
    }
}