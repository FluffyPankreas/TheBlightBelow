using System;
using System.Collections.Generic;
using DarkMushroomGames.GameArchitecture;
using UnityEngine;

namespace DarkMushroomGames.UI
{
    /// <summary>
    /// Displays artifacts that the player has managed to acquire throughout the run.
    /// </summary>
    public class ArtifactDisplayPanel : MonoBehaviour
    {
        [SerializeField,Tooltip("The widgetPrefab to use to show each artifact.")]
        private ArtifactTemplateWidget widget;

        [SerializeField,Tooltip("The artifact inventory that the player has built up over the run.")]
        private ArtifactTemplateLibrary runInventory;

        private List<ArtifactTemplateWidget> _artifactWidgets = new List<ArtifactTemplateWidget>();

        public void Awake()
        {
            Debug.Assert(widget != null,
                "The widget has to be assigned, otherwise the panel won't be able to instantiate them.", gameObject);
            Debug.Assert(runInventory != null, "The artifact inventory has to be assigned.", gameObject);
        }

        public void Start()
        {
            SetupPanel();
        }

        public void Update()
        {
            // check whether the things are the same.
            if (_artifactWidgets.Count != runInventory.GetTemplateArtifacts().Count)
            {
                SetupPanel();
            }
        }

        private void SetupPanel()
        {
            
            foreach (var aWidget in _artifactWidgets)
            {
                Destroy(aWidget);
            }
            
            _artifactWidgets.Clear();
            foreach (var templateArtifact in runInventory.GetTemplateArtifacts())
            {
                var newWidget = Instantiate(widget, transform);
                newWidget.ArtifactInformation = templateArtifact;
                _artifactWidgets.Add(newWidget);
            }
        }
    }
}
