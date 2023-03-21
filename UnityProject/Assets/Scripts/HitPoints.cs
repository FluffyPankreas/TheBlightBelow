using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPoints : MonoBehaviour
{
    [SerializeField,Tooltip("The maximum hit points possible.")]
    private int maxHitPoints = 100;
    
    [SerializeField, Tooltip("The current hit points available.")]
    private int currentHitPoints;

    [SerializeField, Tooltip("Define whether health should reset on awake. ")]
    private bool resetHealth = true;
    public void Awake()
    {
        if (resetHealth)
            currentHitPoints = maxHitPoints;
    }
    
    public int CurrentHitPoints
    {
        get { return currentHitPoints; }
    }
    
    public void Heal(int healAmount)
    {
        currentHitPoints += healAmount;
        Mathf.Clamp(currentHitPoints, 0, maxHitPoints);
    }

    public void Damage(int damageAmount)
    {
        currentHitPoints -= damageAmount;
        Mathf.Clamp(currentHitPoints, 0, maxHitPoints);
    }
}
