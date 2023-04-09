// Copyright (C) 2019-2022 gamevanilla. All rights reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement,
// a copy of which is available at http://unity3d.com/company/legal/as_terms.

using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CCGKit
{
    /// <summary>
    /// The widget used to display the current number of cards in the player's discard pile.
    /// </summary>
    public class DiscardPileWidget : MonoBehaviour
    {
#pragma warning disable 649
        [SerializeField]
        private TextMeshProUGUI textLabel;
#pragma warning restore 649

        private int discardPileSize;
        
        [SerializeField,Tooltip("The button that allows the player to open the deck.")]
        private Button deckButton;

        [SerializeField, Tooltip("The card pile canvas.")]
        private Canvas cardPile;

        public void Update()
        {
            deckButton.enabled = !cardPile.gameObject.activeSelf;
        }

        public void SetAmount(int amount)
        {
            discardPileSize = amount;
            textLabel.text = amount.ToString();
        }

        public void AddCard()
        {
            SetAmount(discardPileSize + 1);
        }
    }
}
