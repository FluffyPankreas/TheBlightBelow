﻿// Copyright (C) 2019-2022 gamevanilla. All rights reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement,
// a copy of which is available at http://unity3d.com/company/legal/as_terms.

using System;
using DarkMushroomGames.GameArchitecture;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CCGKit
{
    /// <summary>
    /// The type corresponding to the "Deal X damage" card effect.
    /// </summary>
    [Serializable]
    public class DealDamageRangeEffect : IntegerRangeEffect, IEntityEffect
    {
        [SerializeField,Tooltip("The damage effect associated with this effect.")]
        private ModifierType damageType;
        
        public override string GetName()
        {
            return $"Deal {LowValue.ToString()}-{HighValue} damage";
        }

        public override void Resolve(RuntimeCharacter instigator, RuntimeCharacter target)
        {
            var targetHp = target.Hp;
            var targetShield = target.Shield;
            var hp = targetHp.Value;
            var shield = targetShield.Value;
            var damage = Random.Range(LowValue, HighValue);

            foreach (var modifierInformation in ModifierQueue.Instance.GetModifiers())
            {
                if (modifierInformation.type == damageType)
                {
                    damage += modifierInformation.value;
                }
            }
            
            var strength = instigator.Status.GetValue("Strength");
            damage += strength;

            var weak = instigator.Status.GetValue("Weak");
            if (weak > 0)
                damage = (int)Mathf.Floor(damage * 0.75f);

            if (damage >= shield)
            {
                var newHp = hp - (damage - shield);
                if (newHp < 0)
                    newHp = 0;
                targetHp.SetValue(newHp);
                targetShield.SetValue(0);
            }
            else
            {
                targetShield.SetValue(shield - damage);
            }
        }
    }
}
