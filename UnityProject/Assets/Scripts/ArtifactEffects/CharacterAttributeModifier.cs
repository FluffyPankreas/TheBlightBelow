using UnityEngine;

namespace ArtifactEffects
{
    public class CharacterAttributeModifier : ArtifactEffect
    {
        [SerializeField, Tooltip("The value for the effect.")]
        private int _value;
        
        public int Value => _value;

        
      //something like happens once or everytime?
      //queue to process the effect?
      //overaidble modifier effect.
      public override void Effect()
      {
          throw new System.NotImplementedException();
      }
    }
}
