using System.Collections.Generic;
using CCGKit;
using GameArchitecture;
using UnityEngine;

namespace Temp
{
    public class UpgradeCanvas : MonoBehaviour
    {
        public GameObject cardPileCanvas;
        public CardPileView cardPileView;

        public CardTemplateLibrary runDeck;

        [SerializeField, Tooltip("Mutate Button")]
        private GameObject mutateButton;

        [SerializeField, Tooltip("Purify Button")]
        private GameObject purifyButton;

        public void Start()
        {
            mutateButton.SetActive(false);
            purifyButton.SetActive(false);

            if (Random.Range(0f, 1f) > 0.5f)
            {
                mutateButton.SetActive(true);
            }
            else
            {
                purifyButton.SetActive(true);
            }
        }
        
        public void OnClickMutate()
        {
            List<RuntimeCard> cardsToShow = new List<RuntimeCard>();
            foreach (var card in runDeck.Cards)
            {
                if (card.ChaosUpgrade != null)
                {
                    var tempRunTimeCard = new RuntimeCard() {Template = card};
                    cardsToShow.Add(tempRunTimeCard);
                }
            }

            cardPileView.HackyAddCards(cardsToShow, this,"C");

            cardPileCanvas.SetActive(true);
        }

        public void OnClickPurify()
        {
            List<RuntimeCard> cardsToShow = new List<RuntimeCard>();
            foreach (var card in runDeck.Cards)
            {
                if (card.OrderUpgrade != null)
                {
                    var tempRunTimeCard = new RuntimeCard() {Template = card};
                    cardsToShow.Add(tempRunTimeCard);
                }
            }

            cardPileView.HackyAddCards(cardsToShow, this,"O");

            cardPileCanvas.SetActive(true);
        }

        public void UpgradeCard(CardTemplate cardToUpgrade, string upgradeType)
        {
            if (upgradeType.Equals("C"))
            {
                // Do a Chaos upgrade.
                Debug.Log("Attempting to do a chaos upgrade for: " + cardToUpgrade.Name );
                var upgradeTemplate = cardToUpgrade.ChaosUpgrade;
                runDeck.Cards.Remove(cardToUpgrade);
                runDeck.Cards.Add(upgradeTemplate);
            }

            if (upgradeType.Equals("O"))
            {
                // Do an order upgrade.
                Debug.Log("Attempting to do an order upgrade for: " + cardToUpgrade.Name );
                var upgradeTemplate = cardToUpgrade.OrderUpgrade;
                runDeck.Cards.Remove(cardToUpgrade);
                runDeck.Cards.Add(upgradeTemplate);
            }
            
            var gameInfo = FindObjectOfType<GameInfo>();
            if (gameInfo != null)
            {
                gameInfo.SaveData.Deck.Clear();
                foreach (var card in runDeck.Cards)
                {
                    gameInfo.SaveData.Deck.Add(card.Id);
                }
            }
            
            mutateButton.SetActive(false);
            purifyButton.SetActive(false);
        }
    }
}
