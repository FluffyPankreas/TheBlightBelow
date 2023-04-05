using UnityEditor;
using ArtifactEffects;

namespace Editor
{
    [CustomEditor(typeof(DrawAtStartTestEffect))]
    public class DrawAtStartTestEffectEditor : EffectEditor
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
