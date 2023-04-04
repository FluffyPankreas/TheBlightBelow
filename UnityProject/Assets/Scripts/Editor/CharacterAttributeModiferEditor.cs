using ArtifactEffects;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(CharacterAttributeModifier))]
    public class CharacterAttributeModiferEditor : UnityEditor.Editor
    {
        private SerializedProperty _name;
        private SerializedProperty _number;
        private SerializedProperty _value;

        public void OnEnable()
        {
            Debug.Log("OnEnable()");
            _value = serializedObject.FindProperty("value");
            _name = serializedObject.FindProperty("Name");
            _number = serializedObject.FindProperty("Number");
        }

        public override void OnInspectorGUI()
        {
            Debug.Log("Name: " + _name.stringValue);
            serializedObject.Update();

            EditorGUILayout.LabelField("BLAD DIE BA EROAJ ");
            EditorGUILayout.BeginVertical();
                EditorGUILayout.PropertyField(_name);
                EditorGUILayout.PropertyField(_number);
                EditorGUILayout.PropertyField(_value);
            EditorGUILayout.EndVertical();
            

            serializedObject.ApplyModifiedProperties();
        }
    }
}
