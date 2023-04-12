using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace DarkMushroomGames.StageLayout
{
    [Serializable]
    public class LightSetup
    {
        [FormerlySerializedAs("LightColor")] [Tooltip("The color that will be applied to the lights.")]
        public Color lightColor = Color.white;
        
        [Tooltip("The intensity that will be applied to the light.")]
        public int lightIntensity = 1;
        
        [Tooltip("The lights that this setup will apply to.")]
        public List<Light> lights;
    }
}
