// Copyright (C) 2019-2022 gamevanilla. All rights reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement,
// a copy of which is available at http://unity3d.com/company/legal/as_terms.

using UnityEngine;

namespace CCGKit
{
    /// <summary>
    /// This system is responsible for managing the game's turn sequence, which always follows
    /// the order:
    ///     - Player turn.
    ///     - Enemies turn.
    /// It creates event-like entities with the Event component attached to them when the turn
    /// sequence advances (beginning of player turn, end of player turn, beginning of enemies
    /// turn, end of enemies turn).
    /// </summary>
    public class TurnManagementSystem : MonoBehaviour
    {
        public GameEvent EncounterStart;
        public GameEvent PlayerTurnBegan;
        public GameEvent PlayerTurnEnded;
        public GameEvent EnemyTurnBegan;
        public GameEvent EnemyTurnEnded;
        public GameEvent EncounterEnded;

        public IntVariable playerShield;
        public StatusVariable playerStatus;

        public StatusTemplate strengthStatus; // Todo: Refine this so it's better.
        
        private bool isEnemyTurn;
        private float accTime;

        private bool isEndOfGame;

        private const float EnemyTurnDuration = 3.0f;

        protected void Update()
        {
            if (isEnemyTurn)
            {
                accTime += Time.deltaTime;
                if (accTime >= EnemyTurnDuration)
                {
                    accTime = 0.0f;
                    EndEnemyTurn();
                    BeginPlayerTurn();
                }
            }
        }

        public void BeginGame()
        {
            EncounterStart!.Raise();
            BeginPlayerTurn();
        }

        public void BeginPlayerTurn()
        {
            PlayerTurnBegan.Raise();
            
            var gameInfo = FindObjectOfType<GameInfo>();

            if (gameInfo.combatBonus && gameInfo.Encounter.EncounterType == NodeType.Enemy)
            {
                Debug.Log("There is a combat bonus from a rest site. Defense: " + gameInfo.defenseBonus +
                          " | Strength: " + gameInfo.strengthBonus);
                // Add strength and/or defense
                playerShield.Value += gameInfo.defenseBonus;

                if (gameInfo.strengthBonus > 0)
                {
                    var currentValue = playerStatus.GetValue(strengthStatus.Name);
                    playerStatus.SetValue(strengthStatus, currentValue + gameInfo.strengthBonus);
                }
            }
        }

        public void EndPlayerTurn()
        {
            PlayerTurnEnded.Raise();
            BeginEnemyTurn();
        }

        public void BeginEnemyTurn()
        {
            EnemyTurnBegan.Raise();
            isEnemyTurn = true;
        }

        public void EndEnemyTurn()
        {
            EnemyTurnEnded.Raise();
            isEnemyTurn = false;
        }

        public void SetEndOfGame(bool value)
        {
            isEndOfGame = value;
            if (isEndOfGame)
            {
                EncounterEnded.Raise();
            }
        }

        public bool IsEndOfGame()
        {
            return isEndOfGame;
        }
    }
}
