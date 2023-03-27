// Copyright (C) 2019-2022 gamevanilla. All rights reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement,
// a copy of which is available at http://unity3d.com/company/legal/as_terms.

using System.Collections.Generic;
using GameArchitecture;
using UnityEngine;
using UnityEngine.Serialization;

namespace CCGKit
{
    /// <summary>
    /// This system is responsible for drawing cards from the player's deck into the player's hand.
    /// Drawing cards from the deck can happen in the following situations:
    ///     - At the beginning of a game.
    ///     - At the beginning of the player's turn.
    ///     - As the result of a card effect ("Draw X cards").
    /// This system processes request-like entities with the component DrawCardsFromDeck attached to
    /// them. This component has an Amount field indicating the number of cards to draw. If there are
    /// not enough cards to draw from the deck, the discard pile is shuffled into the deck and cards
    /// are then drawn again from it.
    /// </summary>
    public class DeckDrawingSystem : MonoBehaviour
    {
        public HandPresentationSystem HandPresentationSystem;

        [SerializeField, Tooltip("The run deck to load intially")]
        private CardTemplateLibrary runDeck;
        
        [SerializeField, Tooltip("The runtime variable for the draw deck.")]
        private CardTemplateLibrary drawDeck;
        
        [SerializeField,Tooltip("The runtime variable for the discard deck,")]
        private CardTemplateLibrary discardDeck;
        
        private List<RuntimeCard> hand;

        private DeckWidget deckWidget;
        private DiscardPileWidget discardPileWidget;

        // Change these values to the ones that make the most sense for your game.
        private const int InitialDeckCapacity = 30;
        private const int InitialDiscardPileCapacity = 30;
        private const int InitialHandCapacity = 30;

        public void Initialize(DeckWidget boostrapDeckWidget, DiscardPileWidget bootstrapDiscardPileWidget)
        {
            deckWidget = boostrapDeckWidget;
            discardPileWidget = bootstrapDiscardPileWidget;

            InitializeDrawDeck(runDeck);
        }

        private void Start()
        {
            drawDeck.Cards.Clear();
            discardDeck.Cards.Clear();
            
            hand = new List<RuntimeCard>(InitialHandCapacity);
        }

        public CardTemplateLibrary GetDeck()
        {
            return drawDeck;
        }

        public CardTemplateLibrary GetDiscardPile()
        {
            return discardDeck;
        }

        private void InitializeDrawDeck(CardTemplateLibrary runDeck)
        {
            foreach (var template in runDeck.Cards)
            {
                drawDeck.Add(template);
            }

            deckWidget.SetAmount(drawDeck.Count);
            discardPileWidget.SetAmount(0);
        }

        public void ShuffleDeck()
        {
            drawDeck.ShuffleLibrary();
        }

        public void DrawCardsFromDeck(IntVariable amount)
        {
            DrawCardsFromDeck(amount.Value);
        }

        public void DrawCardsFromDeck(int amount)
        {
            var deckSize = drawDeck.Count;
            // If there are enough cards in the deck, just draw the cards from it.
            if (deckSize >= amount)
            {
                var prevDeckSize = deckSize;

                var drawnCards = new List<RuntimeCard>(amount);
                for (var i = 0; i < amount; i++)
                {
                    var card = drawDeck.Cards[0];
                    drawDeck.Cards.RemoveAt(0);


                    var newRuntimeCard = new RuntimeCard() {Template = card}; 
                    hand.Add(newRuntimeCard);
                    drawnCards.Add(newRuntimeCard);
                }

                HandPresentationSystem.CreateCardsInHand(drawnCards, prevDeckSize);
            }
            
            // If there are not enough cards in the deck, first shuffle the discard pile into
            // the deck and then draw from the deck normally.
            else
            {
                for (var i = 0; i < discardDeck.Count; i++)
                    drawDeck.Add(discardDeck.Cards[i]);

                discardDeck.Cards.Clear();

                HandPresentationSystem.UpdateDiscardPileSize(discardDeck.Count);

                // Prevent trying to draw more cards than those available.
                if (amount > drawDeck.Count + discardDeck.Count)
                {
                    amount = drawDeck.Count + discardDeck.Count;
                }
                DrawCardsFromDeck(amount);
            }
        }

        public void MoveCardToDiscardPile(RuntimeCard card)
        {
            var idx = hand.IndexOf(card);
            hand.RemoveAt(idx);
            discardDeck.Add(card.Template);
        }

        public void MoveCardsToDiscardPile()
        {
            foreach (var card in hand)
                discardDeck.Add(card.Template);

            hand.Clear();
        }
    }
}
