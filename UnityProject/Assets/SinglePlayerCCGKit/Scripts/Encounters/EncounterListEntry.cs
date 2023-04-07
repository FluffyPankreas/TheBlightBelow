// Copyright (C) 2019-2022 gamevanilla. All rights reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement,
// a copy of which is available at http://unity3d.com/company/legal/as_terms.

using System;
using System.Collections.Generic;
using DarkMushroomGames.GameArchitecture;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CCGKit
{
    [Serializable]
    public class EncounterListEntry
    {
        public string Name;
        public NodeType EncounterType;
        public Sprite Background;
        public int GoldRewardLow;
        public int GoldRewardHigh;
        public List<AssetReference> Enemies = new List<AssetReference>();
        public ArtifactTemplateLibrary ArtifactRewards;
    }
}