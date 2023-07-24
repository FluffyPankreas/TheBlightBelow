using DarkMushroomGames.GameArchitecture.Managers;
using UnityEngine;

namespace DarkMushroomGames.UI
{
    public class GoldRewardWidget : MonoBehaviour
    {
        private int _goldValue = 0;
        
        public void SetupWidget(int goldValue)
        {
            Debug.Log("Setting widget gold value to: " + goldValue);
            _goldValue = goldValue;
        }
        
        public void OnWidgetClicked()
        {
            CharacterManager.Instance.AddGold(_goldValue);
            Destroy(gameObject);
        }
    }
}
