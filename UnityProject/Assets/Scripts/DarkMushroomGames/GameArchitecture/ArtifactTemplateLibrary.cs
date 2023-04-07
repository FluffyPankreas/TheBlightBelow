using System;
using System.Collections.Generic;
using GameArchitecture;
using UnityEngine;
using UnityEngine.Serialization;

namespace DarkMushroomGames.GameArchitecture
{
    [Serializable]
    [CreateAssetMenu(
        menuName = "Dark Mushroom/Artifact Template Library",
        fileName = "ArtifactTemplateLibrary")]
    public class ArtifactTemplateLibrary : ScriptableObject
    {
        [Tooltip("Short description of how this library is meant to be used."), Multiline(3)]
        public string libraryPurpose = string.Empty;

        [SerializeField,Tooltip("The artifacts contained in this library.")]
        private List<ArtifactTemplate> artifacts = new List<ArtifactTemplate>();

        /// <summary>
        /// Adds a new template to the library. 
        /// </summary>
        /// <param name="newArtifactTemplate">The artifact template that will be added.</param>
        public void AddArtifactTemplate(ArtifactTemplate newArtifactTemplate)
        {
            artifacts.Add(newArtifactTemplate);
        }

        /// <summary>
        /// Gets the list of artifact templates in this library.
        /// </summary>
        /// <returns>The list of artifacts that are in the library.</returns>
        public List<ArtifactTemplate> GetTemplateArtifacts()
        {
            return artifacts;
        }
        
    }
}
