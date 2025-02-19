using CCGKit;
using DarkMushroomGames.Architecture;
using UnityEngine;

namespace DarkMushroomGames.GameArchitecture.Managers
{
    //Todo: Add runtime variables for the character attributes here(eg. currentHealth, maxHealth)
    //Todo: Add ways to initialize variables for when encounters start.
    //Todo: Add ways to initialize variables for when runs start/load.
    
    /// <summary>
    /// This manager is responsible for making changes to the character. This includes things like processing
    /// changes to max health, current health and other runtime effects. It will listen to events that require
    /// changes to those characteristics. 
    /// </summary>
    public class CharacterManager : MonoBehaviourSingleton<CharacterManager>
    {
        [SerializeField, Tooltip("The runtime variable that holds the artifacts obtained so far in the run.")]
        private ArtifactTemplateLibrary artifactInventory;
        
        [Header("Runtime Variables")]
        [SerializeField,Tooltip("The runtime variable for updating the player defense during an encounter.")]
        private IntVariable playerEncounterDefense;
        
        [SerializeField,Tooltip("The player's current gold.")]
        private IntVariable playerGold;
        
        [Header("Modifiers to process")]
        [SerializeField,Tooltip("The modifier type that the manager associates with processing defensive effects.")]
        private ModifierType defenseType;

        /// <summary>
        /// Handles events for when the modifier queue changes. It checks for relevant modifiers and applies
        /// effects appropriately. 
        /// </summary>
        public void OnModifierQueueChanged()
        {
            // Debug.Log("OnModifierQueueChanged():" + this.name, gameObject);
        }

        public void PickupArtifact(ArtifactTemplate newArtifact)
        {
            // Debug.Log("New artifact picked up: " + newArtifact.ArtifactName);
            artifactInventory.AddArtifactTemplate(newArtifact);
        }

        /// <summary>
        /// Adds gold to the current amount of gold the player has. 
        /// </summary>
        /// <param name="amountToAdd">The amount to add to the current gold.</param>
        public void AddGold(int amountToAdd)
        {
            playerGold.Value += amountToAdd;
        }

        public void OnEncounterStart()
        {
            Debug.Log("OnEncounterStart():" + this.name, gameObject);
            foreach (var artifact in artifactInventory.GetTemplateArtifacts())
            {
                artifact.ApplyEffects();
            }
        }
        
        /// <summary>
        /// Handles events for when the player turn starts.
        /// </summary>
        public void OnPlayerTurnStart()
        {
            // Debug.Log("OnPlayerTurnStart(): " + this.name, gameObject);
        }

        /// <summary>
        /// Handles events for when the player turn ends.
        /// </summary>
        public void OnPlayerTurnEnd()
        {
            // Debug.Log("OnPlayerTurnEnd(): " + this.name, gameObject);
            
            var additionalDefense = playerEncounterDefense.Value;
            foreach (var modifierInfo in ModifierQueue.Instance.GetModifiers())
            {
                if (modifierInfo.type == defenseType)
                {
                    additionalDefense += modifierInfo.value;
                }
            }

            
            playerEncounterDefense.SetValue(additionalDefense);
            
        }
        
    }
}
