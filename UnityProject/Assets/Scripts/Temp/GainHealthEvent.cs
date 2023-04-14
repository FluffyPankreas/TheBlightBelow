using CCGKit;
using UnityEngine;

namespace Temp
{
    public class GainHealthEvent : MonoBehaviour
    {
        public IntVariable playerHealth;
        // Start is called before the first frame update
        void Start()
        {
            Debug.Log("PLAYER HEALTH: " + playerHealth.Value);
            playerHealth.Value = Mathf.RoundToInt(playerHealth.Value * 1.1f);
        }

    }
}
