// Copyright (C) 2019-2022 gamevanilla. All rights reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement,
// a copy of which is available at http://unity3d.com/company/legal/as_terms.

using System.Collections.Generic;
using System.Linq;
using Temp;
using UnityEngine;
using UnityEngine.UI;

namespace CCGKit
{
    public class CardPileView : MonoBehaviour
    {
        public GameObject CardPrefab;
        public GameObject Content;

        public HandPresentationSystem HandPresentationSystem;

        private List<GameObject> widgets = new List<GameObject>(16);

        public void AddCards(List<RuntimeCard> cards)
        {
            var sortedCards = cards.OrderBy(x => x.Template.Name).ToList();
            foreach (var card in sortedCards)
            {
                var widget = Instantiate(CardPrefab, Content.transform, false);
                widget.GetComponent<CardWidget>().SetInfo(card);
               
                widgets.Add(widget);
            }
        }

        public void HackyAddCards(List<RuntimeCard> cards, UpgradeCanvas caller, string upgradeType)
        {
            var sortedCards = cards.OrderBy(x => x.Template.Name).ToList();
            foreach (var card in sortedCards)
            {
                var widget = Instantiate(CardPrefab, Content.transform, false);
                widget.GetComponent<CardWidget>().SetInfo(card);
                widget.GetComponent<Button>().onClick.AddListener(() =>
                {
                    caller.UpgradeCard(widget.GetComponent<CardWidget>().Card,upgradeType);
                    transform.parent.gameObject.SetActive(false);
                });  
                widgets.Add(widget);
            }
             
        }

        private void OnEnable()
        {
            HandPresentationSystem.SetHandCardsInteractable(false);
        }

        private void OnDisable()
        {
            foreach (var widget in widgets)
            {
                Destroy(widget);
            }

            widgets.Clear();
            
            HandPresentationSystem.SetHandCardsInteractable(true);
        }
    }
}