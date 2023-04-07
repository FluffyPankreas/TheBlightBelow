using DarkMushroomGames.Architecture;

namespace DarkMushroomGames.GameArchitecture.Managers
{
    /// <summary>
    /// This manager is responsible for making changes to the character. This includes things like processing
    /// changes to max health, current health and other runtime effects. It will listen to events that require
    /// changes to those characteristics. 
    /// </summary>
    public class CharacterManager : MonoBehaviourSingleton<CharacterManager>
    {
        /// <summary>
        /// Handles events for when the modifier queue changes. It checks for relevant modifiers and applies
        /// effects appropriately. 
        /// </summary>
        public void OnModifierQueueChanged()
        {
            
        }
    }
}
