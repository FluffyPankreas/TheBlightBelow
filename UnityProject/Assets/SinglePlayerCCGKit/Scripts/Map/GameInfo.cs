// Copyright (C) 2019-2022 gamevanilla. All rights reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement,
// a copy of which is available at http://unity3d.com/company/legal/as_terms.

using UnityEngine;

namespace CCGKit
{
    public class GameInfo : MonoBehaviour
    {
        public Coordinate PlayerCoordinate;
        
        public EncounterListEntry Encounter;
        public bool PlayerWonEncounter;

        public SaveData SaveData = new SaveData();

        public bool combatBonus = false;

        public int defenseBonus = 10;
        public int strengthBonus = 2;
        
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}