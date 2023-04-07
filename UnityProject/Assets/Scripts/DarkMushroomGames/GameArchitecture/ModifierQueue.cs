using System.Collections.Generic;
using CCGKit;
using UnityEngine;
using DarkMushroomGames.Architecture;

namespace DarkMushroomGames.GameArchitecture
{
    public class ModifierQueue : MonoBehaviourSingleton<ModifierQueue>
    {
        [SerializeField,Tooltip("The event raised when a new modifier is queued.")]
        private GameEvent onEnqueueEvent;

        private readonly List<ModifierInformation> _modifierQueue = new();
        
        protected override void SingletonAwakened()
        {
            Debug.Assert(onEnqueueEvent != null,
                "The onEnqueueEvent isn't assigned. This will prevent other systems from responding properly. Please make sure you are using a prefab for the singleton.",
                gameObject);
        }
        
        public void AddToQueue(ModifierType type, int value )
        {
            onEnqueueEvent!.Raise();
            _modifierQueue.Add(new ModifierInformation(type,value));
            Debug.Log("Added modifier effect: " + type.name, type);
        }

        public List<ModifierInformation> GetModifiers()
        {
            return _modifierQueue;
        }

        /// <summary>
        /// Event handler for when the encounter ends.
        /// </summary>
        public void OnEncounterEnd()
        {
            // Clear all modifiers before the next encounter.
            _modifierQueue.Clear();
        }
    }
}
