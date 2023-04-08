using DarkMushroomGames.GameArchitecture;
using UnityEngine;
using UnityEngine.Serialization;

namespace DarkMushroomGames.GameEffects
{
    public class ModifierEffect : Effect
    {
        [Tooltip("The type of modifier that has to be applied. These types are define as scriptable objects.")]
        public ModifierType Type;
        
        [Tooltip("The value that the modifier type will be modified by.")]
        public int Value;
        
        public override void Resolve()
        {
            ModifierQueue.Instance.AddToQueue(Type,Value);
        }
    }
}
