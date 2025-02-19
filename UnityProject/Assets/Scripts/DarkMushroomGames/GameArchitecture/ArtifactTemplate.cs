using System;
using System.Collections.Generic;
using DarkMushroomGames.GameEffects;
using UnityEngine;

namespace DarkMushroomGames.GameArchitecture
{
    [Serializable]
    [CreateAssetMenu(
        menuName = "Dark Mushroom/Artifacts/Template",
        fileName = "ArtifactTemplate")]
    public class ArtifactTemplate : ScriptableObject
    {
        [SerializeField,Tooltip("The name of the artifact.")]
        private string artifactName;
        
        [SerializeField,Tooltip("The icon that will show up in the UI.")]
        private Sprite artifactIcon;

        [SerializeField, Tooltip("The list of effects that the artifact possesses.")]
        private List<Effect> artifactEffects;

        /// <summary>
        /// The name of the artifact.
        /// </summary>
        public string ArtifactName => artifactName;
        
        /// <summary>
        /// The icon of the artifact for UI purposes.
        /// </summary>
        public Sprite ArtifactIcon => artifactIcon;
        
        /// <summary>
        /// The effects that the artifact will apply to the game.
        /// </summary>
        public List<Effect> ArtifactEffects => artifactEffects;

        /// <summary>
        /// Applies all the effects of the artifact. 
        /// </summary>
        public void ApplyEffects()
        {
            foreach (var effect in artifactEffects)
            {
                effect.Resolve();
            }
        }
    }
}
