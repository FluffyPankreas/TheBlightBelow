using System;
using System.Collections.Generic;
using UnityEngine;
using CCGKit;
using UnityEngine.Serialization;

namespace GameArchitecture
{
    [Serializable]
    [CreateAssetMenu(
        menuName = "Game Architecture/Card library",
        fileName = "CardTemplateLibrary",
        order = 3)]
    public class CardTemplateLibrary : ScriptableObject
    {
#if UNITY_EDITOR
        [FormerlySerializedAs("variablePurpose")] [Multiline,Tooltip("Give a short description of what the card library represents.")]
        public string libraryPurpose = string.Empty;
#endif
        public string Name;
        [SerializeField]
        private List<CardTemplate> _cards = new List<CardTemplate>();
        
        public int Count => _cards.Count;

        public List<CardTemplate> Cards
        {
            get { return _cards; }
        }

        public void Add(CardTemplate newCard)
        {
            _cards.Add(newCard);
        }
        
        public void ShuffleLibrary()
        {
            _cards.Shuffle();
        }
    }
}
