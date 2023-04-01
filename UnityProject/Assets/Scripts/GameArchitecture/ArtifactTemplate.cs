using UnityEngine;
using UnityEngine.Serialization;

namespace GameArchitecture
{
    [CreateAssetMenu(
        menuName = "Game Architecture/Artifact Template",
        fileName = "ArtifactTemplate")]
    public class ArtifactTemplate : ScriptableObject
    {
        [SerializeField,Tooltip("The name of the artifact.")]
        private string artifactName;
        
        [SerializeField,Tooltip("The icon that will show up in the UI.")]
        private Sprite artifactIcon;
        
        public string ArtifactName => artifactName;
         public Sprite ArtifactIcon => artifactIcon;
    }
}
