using CCGKit;
using UnityEngine;

namespace Temp
{
    public class RemoveGoldEvent : MonoBehaviour
    {
        public IntVariable playerGold;
        
        // Start is called before the first frame update
        void Start()
        {
            playerGold.Value = Mathf.RoundToInt(playerGold.Value * 0.9f);
        }

    }
}
