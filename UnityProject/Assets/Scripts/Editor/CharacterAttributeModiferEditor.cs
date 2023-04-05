using ArtifactEffects;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(CharacterAttributeModifier))]
    public class CharacterAttributeModiferEditor : EffectEditor
    {
        public void OnEnable()
        {
        }

        public override void OnInspectorGUI()
        {
            Debug.Log("CharacterAttributeModifierEditor");
            base.OnInspectorGUI();

        }

    }
}
