using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Creature : MonoBehaviour
{
    [FormerlySerializedAs("_attacks")] [SerializeField,Tooltip("The possible attacks the creature can make each round.")]
    private List<CreatureAttack> attacks;

    /// <summary>
    /// Allow the creature to make an attack against the player.
    /// </summary>
    public void MakeAttack()
    {
        var attackIndex = Random.Range(0, attacks.Count);
        Debug.Log("Attack being made: " + attacks[attackIndex].AttackName);

        attacks[attackIndex].MakeAttack();
    }
}
