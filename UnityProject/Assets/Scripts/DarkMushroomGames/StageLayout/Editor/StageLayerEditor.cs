using UnityEditor;
using UnityEngine;

namespace DarkMushroomGames.StageLayout.Editor
{
    [CustomEditor(typeof(StageLayer))]
    public class StageLayerEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            var currentTarget = serializedObject.targetObject as StageLayer;

            base.OnInspectorGUI();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.BeginVertical();

            if (GUILayout.Button("Test randomization"))
            {
                currentTarget!.RandomizeSetPieces();
            }

            if (GUILayout.Button("Turn On all set pieces"))
            {
                foreach (var setPiece in currentTarget!.GetComponentsInChildren<SetPiece>(true))
                {
                    setPiece.gameObject.SetActive(true);
                }    
            }

            EditorGUILayout.EndVertical();

            EditorGUILayout.BeginVertical();
            if (GUILayout.Button("Attach Set Piece components"))
            {
                foreach (Transform child in currentTarget!.transform)
                {
                    if (child.GetComponent<SetPiece>() == null)
                    {
                        Undo.RecordObject(child, "Adding set pieces.");
                        child.gameObject.AddComponent<SetPiece>();
                    }
                }
            }
            
            if (GUILayout.Button("Zero out Set Pieces"))
            {
                foreach (Transform child in currentTarget!.transform)
                {
                    if (child.GetComponent<SetPiece>() != null)
                    {
                        Undo.RecordObject(child, "Zero out child transform.");
                        child.localPosition = new Vector3(child.localPosition.x, child.localPosition.y, 0);
                    }
                }
            }

            EditorGUILayout.EndVertical();
            EditorGUILayout.EndHorizontal();
        }
    }
}
