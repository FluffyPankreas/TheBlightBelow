using DarkMushroomGames.Architecture;
using UnityEngine;

namespace DarkMushroomGames.GameArchitecture.Managers
{
    public class TreasureEncounterManager : MonoBehaviourSingleton<TreasureEncounterManager>
    {
        [SerializeField,Tooltip("The list of artifacts that are available to earn in treasure rooms.")]
        private ArtifactTemplateLibrary treasureRoomArtifacts;

        [SerializeField,Tooltip("The panel that shows the relic selection.")]
        private GameObject relicSelectionPanel;
        public void Awake()
        {
            Debug.Assert(treasureRoomArtifacts != null, "The artifacts library has to be set.", gameObject);
        }
        public void OnEndEncounter()
        {
            RestSystems.EndEncounter();
        }
    }
}
