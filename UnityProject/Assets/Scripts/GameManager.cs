using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // The game manager will run the game.
        // controls the start of rounds
        // controls the end of rounds.
        // controls the game win/lose conditions.
        // Manages which room/creature/treasure spawns(eventually).
        
        [SerializeField,Tooltip("The player in the game.")]
        private Player player;
        
        [SerializeField,Tooltip("The creature that the player is fighting.")]
        private Creature enemyCreature;

        private int _roundNumber = 1;
        
        public void Awake()
        {
            ResetGame();
        }
        
        public void ResetGame()
        {
            Debug.Log("Resetting the game.");

            _roundNumber = 1;
            player.GetComponent<HitPoints>()!.ResetHealth();
            enemyCreature.GetComponent<HitPoints>()!.ResetHealth();
        }
}
