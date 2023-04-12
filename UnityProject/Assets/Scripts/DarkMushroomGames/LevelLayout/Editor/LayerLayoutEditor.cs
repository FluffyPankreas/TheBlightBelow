using UnityEditor;
using UnityEngine;

namespace DarkMushroomGames.LevelLayout.Editor
{
    [CustomEditor(typeof(LayerLayout))]
    public class LayerLayoutEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            var currentTarget = serializedObject.targetObject as LayerLayout;

            base.OnInspectorGUI();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.BeginVertical();
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

            EditorGUILayout.EndVertical();
            EditorGUILayout.EndHorizontal();
        }
    }
}
