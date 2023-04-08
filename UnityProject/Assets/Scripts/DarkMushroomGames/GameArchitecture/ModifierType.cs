using UnityEngine;


namespace DarkMushroomGames.GameArchitecture
{
    [CreateAssetMenu(menuName = "Dark Mushroom/Types/Modifier Type", fileName = "ModifierType")]
    public class ModifierType : ScriptableObject
    {
        [SerializeField,Tooltip("Give a short description of what the type is likely to affect and/or used for.")]
        private string description;
    }
}
