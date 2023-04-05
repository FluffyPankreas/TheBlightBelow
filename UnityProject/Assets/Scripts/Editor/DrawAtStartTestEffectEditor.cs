using UnityEditor;
using ArtifactEffects;

namespace Editor
{
    [CustomEditor(typeof(DrawAtStartTestEffect))]
    public class DrawAtStartTestEffectEditor : EffectEditor
    {
        public override void OnInspectorGUI()
        {
            //base.OnInspectorGUI();
            DrawDefaultInspector();
        }
    }
}
