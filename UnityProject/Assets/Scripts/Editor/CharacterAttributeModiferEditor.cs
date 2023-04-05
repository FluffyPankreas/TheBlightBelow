using System;
using ArtifactEffects;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(CharacterAttributeModifier))]
    public class CharacterAttributeModiferEditor : EffectEditor
    {
        private SerializedProperty valueToShow;

        public new void OnEnable()
        {
            base.OnEnable();
            valueToShow = serializedObject.FindProperty("value");
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            
            serializedObject.Update();
            EditorGUILayout.PropertyField(valueToShow);
            serializedObject.ApplyModifiedProperties();
        }

    }
}
