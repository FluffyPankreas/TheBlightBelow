using ArtifactEffects;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(CharacterAttributeModifier))]
    public class CharacterAttributeModiferEditor : EffectEditor
    {
        public override void OnInspectorGUI()
        {
            //base.OnInspectorGUI();
            DrawDefaultInspector();
        }

    }
}
