using UnityEditor;
using ArtifactEffects;

namespace Editor
{
    [CustomEditor(typeof(DamageTestEffect))]
    public sealed class DamageTestEffectEditor : EffectEditor
    {
        public override void OnInspectorGUI()
        {
            //base.OnInspectorGUI();
            DrawDefaultInspector();
        }
    }
}
