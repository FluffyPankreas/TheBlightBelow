using CCGKit;
using UnityEngine;

namespace GameSystems
{
    public class StatusResolutionSystem : BaseSystem
    {
        public void OnPlayerTurnEnded()
        {
            var weak = Player.Character.Status.GetValue("Weak");
            if (weak > 0)
                Player.Character.Status.SetValue("Weak", --weak);
        }
    }
}
