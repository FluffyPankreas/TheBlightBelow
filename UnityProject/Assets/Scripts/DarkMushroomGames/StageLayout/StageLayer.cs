using System.Collections.Generic;
using UnityEngine;

namespace DarkMushroomGames.StageLayout
{
    public class StageLayer : MonoBehaviour
    {
        [SerializeField, Tooltip("The number of set pieces to activate when the level is loaded.")]
        private int activeSetPieceAmount = 1;

        public void Start()
        {
            RandomizeSetPieces();
        }
        
        /// <summary>
        /// Selects a number of set pieces top turn on.
        /// </summary>
        public void RandomizeSetPieces()
        {
            var setPieces = transform.GetComponentsInChildren<SetPiece>(true);

            // Don't do anything if there aren't any set pieces found. 
            if(setPieces.Length<=0) return;
            
            foreach (var setPiece in setPieces)
            {
                setPiece.gameObject.SetActive(false);
            }

            var indexBag = new List<int>();
            for (int i = 0; i < setPieces.Length; i++)
            {
                indexBag.Add(i);
            }
            
            for (int i = 0; i < activeSetPieceAmount; i++)
            {
                int bagPull = Random.Range(0, indexBag.Count);
                int randomIndex = indexBag[bagPull];
                indexBag.RemoveAt(bagPull);

                setPieces[randomIndex].gameObject.SetActive(true);
            }

        }

    }
}
