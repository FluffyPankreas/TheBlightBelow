﻿// Copyright (C) 2019-2022 gamevanilla. All rights reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement,
// a copy of which is available at http://unity3d.com/company/legal/as_terms.

using System;

namespace CCGKit
{
    /// <summary>
    /// The type corresponding to the "Gain X shield" card effect.
    /// </summary>
    [Serializable]
    public class GainShieldEffect : IntegerEffect
    {
        public override string GetName()
        {
            return $"Gain {Value.ToString()} Defence";
        }

        public override void Resolve(RuntimeCharacter instigator, RuntimeCharacter target)
        {
            var targetShield = target.Shield;
            targetShield.SetValue(targetShield.Value + Value);
        }
    }
}
