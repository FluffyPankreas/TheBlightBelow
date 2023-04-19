// Copyright (C) 2019-2022 gamevanilla. All rights reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement,
// a copy of which is available at http://unity3d.com/company/legal/as_terms.

using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace CCGKit
{
    /// <summary>
    /// This component is attached to the EndBattlePopup prefab.
    /// </summary>
    public class EndBattlePopup : MonoBehaviour
    {
#pragma warning disable 649
        [SerializeField]
        private TextMeshProUGUI titleText;
        [SerializeField]
        private TextMeshProUGUI descriptionText;

        [SerializeField]
        private Button continueButton;
        [FormerlySerializedAs("rewardButton")] [SerializeField]
        private Button cardRewardButton;
        [SerializeField] 
        private Button artifactRewardButton;
        [SerializeField] 
        private Button goldRewardButton;
        

        [SerializeField]
        private GameEvent cardRewardEvent;
        [SerializeField]
        private GameEvent artifactRewardEvent;
        [SerializeField]
        private GameEvent goldRewardEvent;
        [SerializeField]
        private Canvas popupCanvas;
#pragma warning restore 649

        private CanvasGroup canvasGroup;

        private const string VictoryText = "Victory";
        private const string DefeatText = "Defeat";
        private const string DefeatDescriptionText = "The dungeon run was too hard this time... better luck next time!";
        private const float FadeInTime = 0.4f;

        private void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
            var gameInfo = FindObjectOfType<GameInfo>();
            if (gameInfo.Encounter.ArtifactRewards != null)
            {
                if (gameInfo.Encounter.ArtifactRewards.GetTemplateArtifacts().Count > 0)
                {
                    artifactRewardButton.gameObject.SetActive(true);
                }
            }
        }

        public void Show()
        {
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
            canvasGroup.DOFade(1.0f, FadeInTime);
        }

        public void SetVictoryText()
        {
            titleText.text = VictoryText;
            descriptionText.text = string.Empty;
        }

        public void SetDefeatText()
        {
            Destroy(cardRewardButton.gameObject);
            titleText.text = DefeatText;
            descriptionText.text = DefeatDescriptionText;
            continueButton.gameObject.SetActive(false);
        }

        public void OnContinueButtonPressed()
        {
            Transition.LoadLevel("Map", 0.5f, Color.black);
        }

        public void OnCardRewardButtonPressed()
        {
            Destroy(cardRewardButton.gameObject);
            popupCanvas.gameObject.SetActive(false);
            cardRewardEvent.Raise();
        }

        public void OnArtifactRewardButtonPressed()
        {
            Debug.Log("Artifact reward button pressed.");
            Destroy(artifactRewardButton.gameObject);
            artifactRewardEvent.Raise();
        }

        public void OnGoldRewardButtonPressed()
        {
            Debug.Log("Gold reward button pressed.");
            Destroy(goldRewardButton.gameObject);
            goldRewardEvent.Raise();
        }
    }
}
