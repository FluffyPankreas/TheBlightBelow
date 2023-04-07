using System.Collections.Generic;
using UnityEngine;
using DarkMushroomGames.Architecture;

namespace DarkMushroomGames.GameArchitecture
{
    public class ModifierQueue : MonoBehaviourSingleton<ModifierQueue>
    {
        [SerializeField] private List<ModifierInformation> modifierQueue = new();

        public void AddToQueue(ModifierType type, int value )
        {
            // Todo: Add the event raise here.
            modifierQueue.Add(new ModifierInformation(type,value));
        }
    }
}
