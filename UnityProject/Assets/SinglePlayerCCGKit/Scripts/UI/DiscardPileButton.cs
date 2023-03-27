// Copyright (C) 2019-2022 gamevanilla. All rights reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement,
// a copy of which is available at http://unity3d.com/company/legal/as_terms.

using System.Collections.Generic;
using UnityEngine;

namespace CCGKit
{
    public class DiscardPileButton : MonoBehaviour
    {
        public Canvas Canvas;
        public CardPileView View;

        public DeckDrawingSystem DeckDrawingSystem;

        public void OnButtonPressed()
        {
            List<RuntimeCard> tempDeck = new List<RuntimeCard>();
            foreach (var cardTemplate in DeckDrawingSystem.GetDiscardPile().Cards)
            {
                var tempRunTimeCard = new RuntimeCard() {Template = cardTemplate};
                
                tempDeck.Add(tempRunTimeCard);
            }
            View.AddCards(tempDeck);
            
            Canvas.gameObject.SetActive(true);
        }
    }
}