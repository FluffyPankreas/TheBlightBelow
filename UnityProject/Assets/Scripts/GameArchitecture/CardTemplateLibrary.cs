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
        [Multiline,Tooltip("Give a short description of where this variable is meant to be used.")]
        public string variablePurpose = string.Empty;
        [Tooltip("Indicate whether this is going to change at runtime.")]
        public bool meantToBeUsedAtRuntime = false;
#endif
        public string Name;
        [SerializeField]
        private List<CardTemplate> _cards = new List<CardTemplate>();
        
        //TODO: Add card added event?
        //TODO: Add deck empty event?
        //TODO: Add card drawn event?

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
