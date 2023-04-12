using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace DarkMushroomGames.StageLayout
{
    public class StageCoordinator : MonoBehaviour
    {
        [SerializeField,Tooltip("The potential light setups that the stage can use.")]
        private List<LightSetup> lightSetups;

        public void Start()
        {
            RandomizeLightSetup();
        }

        public void RandomizeLightSetup()
        {
            var setupToApply = lightSetups[Random.Range(0, lightSetups.Count)];
            
            foreach (var l in setupToApply.lights)
            {
                l.intensity = setupToApply.lightIntensity;
                l.color = setupToApply.lightColor;
            }
        }
    }
}
