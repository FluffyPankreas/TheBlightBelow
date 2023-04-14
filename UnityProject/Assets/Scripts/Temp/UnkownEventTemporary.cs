using System.Collections.Generic;
using CCGKit;
using UnityEngine;

namespace Temp
{
    public class UnknownEventTemporary : MonoBehaviour
    {
        [SerializeField]
        private List<GameObject> eventPanels;
        public void Start()
        {
            Debug.Log("UnknownEventPanels");
            
            var gameInfo = GameObject.FindObjectOfType<GameInfo>();
            var encounterName = gameInfo.Encounter.Name;
            Debug.Log("Name: " + encounterName);
            foreach (var panel in eventPanels)
            {
                panel.SetActive(false);
            }
            
            switch (encounterName)
            {
                case "ReturnToTown":
                    eventPanels[0].SetActive(true);
                    ReturnToTownEvent();
                    break;
                case "Bestiary":
                    eventPanels[1].SetActive(true);
                    BestiaryEntryEvent();
                    break;
                case "RemoveSomeGold":
                    eventPanels[2].SetActive(true);
                    RemoveGoldEvent();
                    break;
                case "GainSomeGold":
                    eventPanels[3].SetActive(true);
                    GainGoldEvent();
                    break;
                case "BetGold":
                    eventPanels[4].SetActive(true);
                    BetGoldEvent();
                    break;
                case "GainHealth":
                    eventPanels[5].SetActive(true);
                    GainHealthEvent();
                    break;
                case "GainMaxHealth":
                    eventPanels[6].SetActive(true);
                    GainMaxHealthEvent();
                    break;
            }
        }

        private void ReturnToTownEvent()
        {
            Debug.Log("Return to town event.");
        }

        private void BestiaryEntryEvent()
        {
            Debug.Log("Bestiary event.");
            
        }

        private void RemoveGoldEvent()
        {
            Debug.Log("Remove gold event.");
            
        }

        private void GainGoldEvent()
        {
            Debug.Log("Gain gold event.");
            
        }

        private void BetGoldEvent()
        {
            Debug.Log("Bet gold event.");
            
        }

        private void GainHealthEvent()
        {
            Debug.Log("Gain health event.");
            
        }

        private void GainMaxHealthEvent()
        {
            Debug.Log("Gain max health event.");
            
        }
    }
}
