using System;
using CCGKit;
using UnityEditor;
using UnityEngine;

namespace DarkMushroomGames.CardEffects
{
    [Serializable]
    public class ExhaustCardEffect : Effect
    {
        public override string GetName()
        {
            return "Exhaust";
        }
#if UNITY_EDITOR
        

        public override void Draw(Rect rect)
        {
            var numberFieldStyle = new GUIStyle(EditorStyles.numberField);
            numberFieldStyle.fixedWidth = 40;
        }

        public override int GetNumRows()
        {
            return 2;
        }
#endif
    }
}