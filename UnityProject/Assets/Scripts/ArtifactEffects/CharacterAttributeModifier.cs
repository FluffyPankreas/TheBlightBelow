using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace ArtifactEffects
{
    [Serializable]
    public class CharacterAttributeModifier : Effect
    {
        [SerializeField, Tooltip("The value for the effect.")]
        private int value;
        
        public int Value => value;

        public override void Resolve()
        {
            throw new System.NotImplementedException();
        }
    }
}
