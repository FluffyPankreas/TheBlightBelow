using System.Collections.Generic;
using UnityEngine;
using DarkMushroomGames.Architecture;
using UnityEngine.Serialization;

namespace DarkMushroomGames.GameArchitecture
{
    public class ModifierQueue : MonoBehaviourSingleton<ModifierQueue>
    {
        private readonly List<ModifierInformation> _modifierQueue = new();

        public CCGKit.GameEvent onEnqueueEvent;

        public void Awake()
        {
            Debug.Assert(onEnqueueEvent != null,
                "The onEnqueueEvent isn't assigned. This will prevent other systems from responding properly. Please make sure you are using a prefab for the singleton.",
                gameObject);
        }
        
        public void AddToQueue(ModifierType type, int value )
        {
            onEnqueueEvent!.Raise();
            _modifierQueue.Add(new ModifierInformation(type,value));
        }
    }
}
