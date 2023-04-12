// Copyright (C) 2019-2022 gamevanilla. All rights reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement,
// a copy of which is available at http://unity3d.com/company/legal/as_terms.

using System;
using GameArchitecture;
using UnityEngine;
using UnityEngine.Serialization;

namespace CCGKit
{
    [Serializable]
    [CreateAssetMenu(
        menuName = "Single-Player CCG Kit/Templates/Hero",
        fileName = "Hero",
        order = 1)]
    public class HeroTemplate : CharacterTemplate
    {
        [FormerlySerializedAs("Hp")] public int MaximumHp;
        public int Energy;
        [FormerlySerializedAs("DrawCount")] public int BaseDrawAmount;
        public CardTemplateLibrary StartingDeck;
        public CardTemplateLibrary RewardDeck;
        public int StartingGoldAmount;
    }
}
