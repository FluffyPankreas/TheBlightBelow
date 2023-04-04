using UnityEngine;

namespace ArtifactEffects
{
    [CreateAssetMenu(
        menuName = "Game Architecture/DamageEffect",
        fileName = "ArtifactTemplate")]
    public class DamageTestEffect : Effect
    {
        public int LowDiceValue;
        public int HighDiceValue;
        public int DiceCount;
        public override void Resolve()
        {
            throw new System.NotImplementedException();
        }
    }
}
