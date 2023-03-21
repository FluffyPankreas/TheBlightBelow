using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPoints : MonoBehaviour
{
    [SerializeField,Tooltip("The maximum hit points possible.")]
    private int maxHitPoints;
    
    [SerializeField, Tooltip("The current hit points available.")]
    private int currentHitPoints;

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
