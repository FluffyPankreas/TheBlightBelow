using UnityEditor;
using UnityEngine;

namespace DarkMushroomGames.StageLayout.Editor
{
    [CustomEditor(typeof(StageCoordinator))]
    public class StageCoordinatorEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (GUILayout.Button("Test randomized stage"))
            {
                TestRandomizedLayers();
                TestRandomizedLighting();
            }

            if (GUILayout.Button("Test randomized layers"))
            {
                TestRandomizedLayers();
            }

            if (GUILayout.Button("Test randomized lighting"))
            {
                TestRandomizedLighting();
            }
        }

        private void TestRandomizedLayers()
        {
            var stageLayers =
                FindObjectsByType<StageLayer>(FindObjectsInactive.Include, FindObjectsSortMode.None);

            foreach (var stageLayer in stageLayers)
            {
                stageLayer.RandomizeSetPieces();
            }
        }

        private void TestRandomizedLighting()
        {
            ((StageCoordinator)target).RandomizeLightSetup();
        }
    }
}
