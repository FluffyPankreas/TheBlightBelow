using UnityEngine;

namespace DarkMushroomGames.LevelLayout
{
    public class LayerLayout : MonoBehaviour
    {
        [SerializeField,Tooltip("The number of set pieces to activate when the level is loaded.")]
        private int activeSetPieces;
        
        /// <summary>
        /// Selects a number of set pieces top turn on.
        /// </summary>
        public void RandomizeSetPieces()
        {
            
        }

    }
}
