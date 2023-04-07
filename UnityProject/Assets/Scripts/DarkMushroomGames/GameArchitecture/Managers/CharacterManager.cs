using DarkMushroomGames.Architecture;
using UnityEngine;

namespace DarkMushroomGames.GameArchitecture.Managers
{
    /// <summary>
    /// This manager is responsible for making changes to the character. This includes things like processing
    /// changes to max health, current health and other runtime effects. It will listen to events that require
    /// changes to those characteristics. 
    /// </summary>
    public class CharacterManager : MonoBehaviourSingleton<CharacterManager>
    {
        //Todo: Add runtime variables for the character attributes here(eg. currentHealth, maxHealth)
        //Todo: Add ways to initialize variables for when encounters start.
        //Todo: Add ways to initialize variables for when runs start/load.
        
        /// <summary>
        /// Handles events for when the modifier queue changes. It checks for relevant modifiers and applies
        /// effects appropriately. 
        /// </summary>
        public void OnModifierQueueChanged()
        {
            
        }

        /// <summary>
        /// Handles events for when the player turn starts.
        /// </summary>
        public void OnPlayerTurnStart()
        {
            Debug.Log("OnPlayerTurnStart: " + this.name, gameObject);
            
        }
    }
}
