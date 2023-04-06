using UnityEditor;

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

            _modifierType = serializedObject.FindProperty("ModifierType");
            _modifierValue = serializedObject.FindProperty("ModifierValue");
            
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

