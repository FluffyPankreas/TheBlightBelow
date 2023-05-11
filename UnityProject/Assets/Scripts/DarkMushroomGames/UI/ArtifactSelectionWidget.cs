using UnityEngine;
using UnityEngine.UI;
using DarkMushroomGames.GameArchitecture;
using DarkMushroomGames.GameArchitecture.Managers;

namespace DarkMushroomGames.UI
{
    public class ArtifactSelectionWidget : MonoBehaviour
    {
        [SerializeField,Tooltip("The widget's image.")]
        private Image widgetImage;

        private ArtifactTemplate _artifact = null;
        public void SetupWidget(ArtifactTemplate templateToShow)
        {
            _artifact = templateToShow;
            widgetImage.sprite = _artifact.ArtifactIcon;
        }

        public void OnArtifactSelected()
        {
            CharacterManager.Instance.PickupArtifact(_artifact);
        }
    }
}
