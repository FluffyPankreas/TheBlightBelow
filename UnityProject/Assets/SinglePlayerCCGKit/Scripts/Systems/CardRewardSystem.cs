// Copyright (C) 2019-2022 gamevanilla. All rights reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement,
// a copy of which is available at http://unity3d.com/company/legal/as_terms.

using GameArchitecture;
using UnityEngine;
using UnityEngine.Serialization;

namespace CCGKit
{
    public class CardRewardSystem : BaseSystem
    {
        public Canvas Canvas;
        public CardRewardView View;
        public IntVariable PlayerGold;

        [FormerlySerializedAs("RewardCards")] public CardTemplateLibrary rewardCardsTemplate;

        public void OnPlayerRedeemedReward()
        {
            Canvas.gameObject.SetActive(true);
            View.AddCards(rewardCardsTemplate);

            var encounter = FindObjectOfType<GameInfo>()?.Encounter;
            var goldReward = Random.Range(encounter.GoldRewardLow, encounter.GoldRewardHigh);
            PlayerGold.Value += goldReward;

        }
    }
}