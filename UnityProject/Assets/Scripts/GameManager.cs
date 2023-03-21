using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using TMPro;
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

        [SerializeField, Tooltip("The label that shows the round number.")]
        private TMP_Text roundNumberLabel; 
        
        private int _roundNumber = 1;
        
        public void Start()
        {
            ResetGame();
        }

        public void ResetGame()
        {
            Debug.Log("Resetting the game.");

            _roundNumber = 1;

            player.ResetForRound();
            enemyCreature.GetComponent<HitPoints>()!.ResetHealth();
        }

        public void EndTurn()
        {
            _roundNumber++;
        }

        public void Update()
        {
            roundNumberLabel.text = _roundNumber.ToString();
        }
}
