using System;
using GameArchitecture;
using UnityEngine;
using UnityEngine.UI;

namespace DarkMushroomGames.UI
{
    [RequireComponent(typeof(Image))]
    public class ArtifactTemplateWidget : MonoBehaviour
    {
        [SerializeField,Tooltip("The artifact template information that this widget will be rendering.")]
        private ArtifactTemplate artifactInformation;

        private Image _widgetImage;

        public ArtifactTemplate ArtifactInformation
        {
            set
            {
                artifactInformation = value;
                UpdateWidget();
            }
        }
        
        public void Awake()
        {
            _widgetImage.GetComponent<Image>();
        }

        private void UpdateWidget()
        {
            _widgetImage = artifactInformation.ArtifactIcon;            
        }
    }
}
