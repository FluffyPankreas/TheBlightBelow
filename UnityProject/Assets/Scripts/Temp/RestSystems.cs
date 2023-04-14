using System;
using System.Collections;
using System.Collections.Generic;
using CCGKit;
using UnityEngine;
using Random = UnityEngine.Random;

public class RestSystems : MonoBehaviour
{
    [SerializeField, Tooltip("The HP variable that needs to be set.")]
    private IntVariable playerHP;

    [SerializeField, Tooltip("The percentage of max HP to heal the player by(value between 0 and 1).")]
    private float percentToHeal = 0.35f;

    [SerializeField, Tooltip("The maximum HP of the character. This is a terrible terrible hack.")]
    private float maxHpHackToGetItWorking = 75;
    
    private TurnManagementSystem _turnManagementSystem;

    public void Awake()
    {
        _turnManagementSystem = GameObject.FindObjectOfType<TurnManagementSystem>();
    }

    public void PrepareForBattle()
    {
        var gameInfo = FindObjectOfType<GameInfo>();

        gameInfo.combatBonus = true;
        
        if (Random.Range(0f, 1f) < 0.5f)
        {
            gameInfo.defenseBonus = 10;
            gameInfo.strengthBonus = 0;
        }
        else
        {
            gameInfo.defenseBonus = 0;
            gameInfo.strengthBonus = 2;
        }
        
        EndEncounter();
    }

    public void EndRest()
    {
        var bootstrap = GameObject.FindObjectOfType<GameBootstrap>();

        var maxHP = maxHpHackToGetItWorking;
        var amountToHeal = Mathf.RoundToInt(maxHP * percentToHeal);

        var newHealth = playerHP.Value + amountToHeal;
        newHealth = Mathf.FloorToInt(Mathf.Clamp(newHealth, 0, maxHpHackToGetItWorking));
        playerHP.Value = newHealth;
        
        EndEncounter();
    }

    public void EndEncounter()
    {
        
        var gameInfo = FindObjectOfType<GameInfo>();
        gameInfo.PlayerWonEncounter = true;
        
        var turnManagementSystem = FindObjectOfType<TurnManagementSystem>();
        turnManagementSystem.SetEndOfGame(true);
        
        Transition.LoadLevel("Map", 0.5f, Color.black);
    }
}
