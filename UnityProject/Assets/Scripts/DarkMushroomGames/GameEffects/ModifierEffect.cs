using DarkMushroomGames.GameArchitecture;
using UnityEngine;

namespace DarkMushroomGames.GameEffects
{
    public class ModifierEffect : Effect
    {
        [Tooltip("The type of modifier that has to be applied. These types are define as scriptable objects.")]
        public ModifierType ModifierType;
        
        [Tooltip("The value that the modifier tpe will be modified by.")]
        public int ModifierValue;
        
        public override void Resolve()
        {
            throw new System.NotImplementedException();
        }
    }
}
