using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HitPoints))]
public class Player : MonoBehaviour
{
    [SerializeField,Tooltip("The amount of mana the player has to spend each turn.")]
    private int maxMana = 10;

    private HitPoints _hitPoints;
    private int _currentMana;

    public void Awake()
    {
        
        _hitPoints = GetComponent<HitPoints>();
        ResetForRound();
    }
    
    public void ResetForRound()
    {
        _currentMana = maxMana;
        _hitPoints.ResetHealth();
    }
}
