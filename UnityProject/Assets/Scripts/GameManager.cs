using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // The game manager will run the game.
        // controls the start of rounds
        // controls the end of rounds.
        // controls the game win/lose conditions.
        // Manages which room/creature/treasure spawns(eventually).
        
        [SerializeField,Tooltip("The creature that the player is fighting.")]
        private Creature enemyCreature;

        [SerializeField,Tooltip("The player in the game.")]
        private Player player;
        
        public void Awake()
        {
            ResetGame();
        }
        
        public void ResetGame()
        {
            
        }
}
