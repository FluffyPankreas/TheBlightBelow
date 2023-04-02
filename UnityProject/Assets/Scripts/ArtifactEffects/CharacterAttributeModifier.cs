using UnityEngine;
using UnityEngine.Serialization;

namespace ArtifactEffects
{
    public class CharacterAttributeModifier : Effect
    {
        [SerializeField, Tooltip("The value for the effect.")]
        private int _value;
        
        public int Value => _value;

      public override void Resolve()
      {
          Debug.Log("Attempting to resolve CharacterAttributeModifier");
      }
    }
}
