using UnityEditor;

namespace DarkMushroomGames.GameEffects.Editor
{
    [CustomEditor(typeof(Effect))]
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

            EditorGUILayout.LabelField(serializedObject.targetObject.name);
            EditorGUILayout.Space();
            EditorGUILayout.BeginHorizontal();
                EditorGUILayout.PropertyField(_name);
            EditorGUILayout.EndHorizontal();
            
            serializedObject.ApplyModifiedProperties();
        }
    }
}
