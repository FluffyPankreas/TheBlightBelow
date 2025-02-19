// Copyright (C) 2019-2022 gamevanilla. All rights reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement,
// a copy of which is available at http://unity3d.com/company/legal/as_terms.

using UnityEngine;

namespace CCGKit
{
    /// <summary>
    /// This system is responsible for keeping the current GameInfo's save data
    /// up-to-date.
    /// </summary>
    public class SavePlayerDataSystem : BaseSystem
    {
        public void OnPlayerHpChanged(int hp)
        {
            var gameInfo = FindObjectOfType<GameInfo>();
            gameInfo.SaveData.Hp = hp;
        }

        public void OnPlayerShieldChanged(int shield)
        {
            var gameInfo = FindObjectOfType<GameInfo>();
            gameInfo.SaveData.Shield = shield;
        }

        public void OnPlayerGoldChanged(int gold)
        {
            Debug.Log("Changing gold value to " + gold);
            var gameInfo = FindObjectOfType<GameInfo>();
            gameInfo.SaveData.Gold = gold;
        }
    }
}