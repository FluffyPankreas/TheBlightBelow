using UnityEditor;
using UnityEngine;

namespace DarkMushroomGames.GameEffects.Editor
{
    [CustomEditor(typeof(ModifierEffect))]
    public class ModifierEffectEditor : EffectEditor
    {
        private SerializedProperty _modifierType;
        private SerializedProperty _modifierValue;
        public new void OnEnable()
        {
            base.OnEnable();

            _modifierType = serializedObject.FindProperty("Type");
            _modifierValue = serializedObject.FindProperty("Value");

            Debug.Assert(_modifierType != null,
                "The value could not be found for 'Type'. Please check that the script has not been updated without updating the editor.");
            Debug.Assert(_modifierValue != null,
                "The value could not be found for 'Value'. Please check that the script has not been updated without updating the editor.");
        }
        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(_modifierType);
            EditorGUILayout.PropertyField(_modifierValue);

            serializedObject.ApplyModifiedProperties();
        }
    }
}

