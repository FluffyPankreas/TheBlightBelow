using UnityEditor;
using ArtifactEffects;

namespace Editor
{
    [CustomEditor(typeof(DamageTestEffect))]
    public sealed class DamageTestEffectEditor : EffectEditor
    {
        public new void OnEnable()
        {
            base.OnEnable();
        }
        
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            //DrawDefaultInspector();
        }
    }
}
