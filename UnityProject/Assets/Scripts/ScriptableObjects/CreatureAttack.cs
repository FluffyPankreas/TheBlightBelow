using UnityEngine;

[CreateAssetMenu(menuName = "Creature attack", fileName = "New Creature Attack")]
public class CreatureAttack : ScriptableObject
{
    [SerializeField,Tooltip("The name of the attack.")]
    private string attackName;
    
    [SerializeField,Tooltip("The amount of dice to throw each time the attack happens.")]
    public int diceCount = 1;
    
    [SerializeField,Tooltip("The minimum amount the dice can roll.")]
    public int diceMin = 1;
    
    [SerializeField,Tooltip("The maximum amount the dice can roll.")]
    public int diceMax = 6;

    public string AttackName
    {
        get { return attackName; }
    }

    public int MakeAttack()
    {

        var damageValue = 0;
        
        for (var i = 0; i < diceCount; i++)
        {
            damageValue += Random.Range(diceMin, diceMax);
        }


        Debug.Log("Attack made for " + damageValue.ToString() + " points of damage.");
        return damageValue;
    }
}
