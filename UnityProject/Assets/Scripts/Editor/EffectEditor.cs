using System;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(ArtifactEffects.Effect))]
    public class EffectEditor : UnityEditor.Editor
    {
        private SerializedProperty _name;
        public void OnEnable()
        {
            _name = serializedObject.FindProperty("Name");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            
            EditorGUILayout.BeginHorizontal();
                EditorGUILayout.PropertyField(_name);
            EditorGUILayout.EndHorizontal();
            
            serializedObject.ApplyModifiedProperties();
        }
    }
}
