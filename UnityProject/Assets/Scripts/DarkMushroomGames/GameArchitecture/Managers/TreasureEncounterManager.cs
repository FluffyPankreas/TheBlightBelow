using System.Runtime.CompilerServices;
using DarkMushroomGames.UI;
using UnityEngine;
using UnityEngine.UI;

namespace DarkMushroomGames.GameArchitecture.Managers
{
    public class TreasureEncounterManager : MonoBehaviour
    {
        [SerializeField,Tooltip("The list of artifacts that are available to earn in treasure rooms.")]
        private ArtifactTemplateLibrary treasureRoomArtifacts;

        [SerializeField,Tooltip("The panel that shows the relic selection.")]
        private GameObject relicSelectionPanel;
        
        [SerializeField,Tooltip("The selection group to put new relics under.")]
        private Transform relicSelectionGroup;

        [SerializeField, Tooltip("The panel for showing a gold reward.")]
        private GameObject goldRewardPanel;

        [SerializeField,Tooltip("The widget that will allow the player to select a specific relic reward.")]
        private ArtifactSelectionWidget artifactSelectionWidget;

        private int _numberOfRelics = 3;
        
        public void Awake()
        {
            Debug.Assert(treasureRoomArtifacts != null, "The artifacts library has to be set.", gameObject);
            Debug.Assert(relicSelectionPanel != null, "The relic panel needs to be setup in the editor.", gameObject);
            Debug.Assert(goldRewardPanel != null, "The gold reward panel needs to be setup in the editor.", gameObject);

            DisableAllPanels();
        }

        public void Start()
        {
            var randomNumber = Random.Range(1, 2);
            

            switch (randomNumber)
            {
                case 0:
                    RelicReward();
                    break;
                case 1:
                    GoldReward();
                    break;
                default:
                    Debug.Log("Something went wrong with the random number. " + randomNumber);
                    break;
            }
            RelicReward();
            
            
            //Choose a reward to get
                //Relics
                //Gold
                //Cards
                //Health
                //Story?
        }


        private void DisableAllPanels()
        {
            relicSelectionPanel.SetActive(false);
            goldRewardPanel.SetActive(false);
        }
        
        private void RelicReward()
        {
            Debug.Log("Offering relic rewards.");
            for (int i = 0; i < _numberOfRelics; i++)
            {
               //create the thing 
               var randomRelic = treasureRoomArtifacts.GetTemplateArtifacts()[UnityEngine.Random.Range(0, treasureRoomArtifacts.Count)];
               var newRelicSelection = Instantiate(artifactSelectionWidget, relicSelectionGroup);
               newRelicSelection.SetupWidget(randomRelic);
               newRelicSelection.GetComponent<Button>().onClick.AddListener(
                   () => relicSelectionPanel.SetActive(false)
                   );// Disable the panel once the player has selected a relic reward.
            }
            
            relicSelectionPanel.SetActive(true);
        }
        
        private void GoldReward()
        {
            Debug.Log("Offering gold reward.");
            //TODO: Fix this. This is not a real system, just a hack job to get the gold rewards sort of working.
            goldRewardPanel.GetComponentInChildren<GoldRewardWidget>().SetupWidget(Random.Range(10, 300));
            goldRewardPanel.SetActive(true);
        }

        private void CardReward()
        {
            Debug.Log("Offering card rewards.");
        }

        public void OnEndEncounter()
        {
            RestSystems.EndEncounter();
        }
    }
}
