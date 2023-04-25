// Copyright (C) 2019-2022 gamevanilla. All rights reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement,
// a copy of which is available at http://unity3d.com/company/legal/as_terms.

using System.Collections.Generic;
using GameArchitecture;
using GameSystems;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

namespace CCGKit
{
    /// <summary>
    /// This component is responsible for bootstrapping the game/battle scene. This process
    /// mainly consists on:
    ///     - Creating the player character and the associated UI widgets.
    ///     - Creating the enemy character/s and the associated UI widgets.
    ///     - Starting the turn sequence.
    /// </summary>
    public class GameBootstrap : MonoBehaviour
    {
#pragma warning disable 649
        [Header("Configuration")]
        [SerializeField]
        private PlayableCharacterConfiguration playerConfig;
        [SerializeField]
        private List<NonPlayableCharacterConfiguration> enemyConfig;
        [SerializeField]
        private AssetReference characterTemplate;

        [Header("Systems")]
        [SerializeField]
        private DeckDrawingSystem deckDrawingSystem;
        [SerializeField]
        private HandPresentationSystem handPresentationSystem;
        [SerializeField]
        private EnergyResetSystem energyResetSystem;
        [SerializeField]
        private TurnManagementSystem turnManagementSystem;
        [SerializeField]
        private CardWithArrowSelectionSystem cardWithArrowSelectionSystem;
        [SerializeField]
        private EnemyBrainSystem enemyBrainSystem;
        [SerializeField]
        private EffectResolutionSystem effectResolutionSystem;
        [SerializeField]
        private PoisonResolutionSystem poisonResolutionSystem;
        [SerializeField] 
        private StatusResolutionSystem statusResolutionSystem;
        [SerializeField]
        private CharacterDeathSystem characterDeathSystem;

        [Header("Character pivots")]
        [SerializeField]
        public Transform playerPivot;
        [SerializeField]
        public List<Transform> enemyPivots1;
        [SerializeField]
        public List<Transform> enemyPivots2;

        [Header("UI")]
        [SerializeField]
        private Canvas canvas;
        [SerializeField]
        private EnergyWidget energyWidget;
        [SerializeField]
        private DeckWidget deckWidget;
        [SerializeField]
        private DiscardPileWidget discardPileWidget;
        [SerializeField]
        private EndTurnButton endTurnButton;
        [SerializeField]
        private SpriteRenderer background;
        [SerializeField] 
        private GameObject combatCanvas;
        [SerializeField] 
        private GameObject restCanvas;
        [SerializeField] 
        private GameObject upgradeCanvas;
        [SerializeField] 
        private GameObject unknownCanvas;
        [SerializeField] 
        private GameObject treasureCanvas;
        
        [Header("Pools")]
        [SerializeField]
        private ObjectPool cardPool;

        [Header("Decks")] 
        [SerializeField, Tooltip("The runtime variable card library that is being used for the run,")]
        private CardTemplateLibrary runDeck;

        [SerializeField, Tooltip("The full library that the character can earn.")]
        private CardTemplateLibrary fullDeck;

        [Header("Player config variables")] 
        [SerializeField, Tooltip("Runtime variable that tracks the player's gold.")]
        private IntVariable gold;
        
#pragma warning restore 649

        private Camera mainCamera;

        //private List<CardTemplate> playerDeck = new List<CardTemplate>();

        private GameObject player;
        private List<GameObject> enemies = new List<GameObject>();

        private GameInfo gameInfo;

        private readonly string saveDataPrefKey = "save";

        private int numAssetsToLoad;
        private int numAssetsLoaded;

        private void Awake()
        {
            mainCamera = Camera.main;

            combatCanvas.SetActive(false);
            restCanvas.SetActive(false);
            cardPool.gameObject.SetActive(false);
            
            cardPool.Initialize();
            
            Addressables.InitializeAsync().Completed += op =>
            {
                CreatePlayer(characterTemplate);
                numAssetsToLoad++;

                var gameInfo = FindObjectOfType<GameInfo>();
                var idx = 0;
                foreach (var template in gameInfo.Encounter.Enemies)
                {
                    CreateEnemy(template, idx++);
                    numAssetsToLoad++;
                }

                background.sprite = gameInfo.Encounter.Backgrounds[UnityEngine.Random.Range(0,gameInfo.Encounter.Backgrounds.Count)];
                
                switch (gameInfo.Encounter.EncounterType)
                {
                    case NodeType.Rest:
                        Debug.Log("Rest encounter.");
                        restCanvas.SetActive(true);
                        break;
                    case NodeType.Merchant:
                        Debug.Log("Merchant encounter.");
                        restCanvas.SetActive(true);
                        break;
                    case NodeType.Treasure:
                        Debug.Log("Treasure encounter.");
                        SceneManager.LoadScene($"TreasureEncounter", LoadSceneMode.Additive);
                        //treasureCanvas.SetActive(true);
                        break;
                    case NodeType.Unknown:
                        Debug.Log("Unknown encounter.");
                        unknownCanvas.SetActive(true);
                        break;
                    case NodeType.Altar:
                        Debug.Log("Altar encounter..");
                        upgradeCanvas.SetActive(true);
                        break;
                    default:
                        Debug.Log("Monster encounter.");
                        combatCanvas.SetActive(true);
                        cardPool.gameObject.SetActive(true);
                        break;
                }
            };
        }

        private void CreatePlayer(AssetReference templateRef)
        {
            var handle = Addressables.LoadAssetAsync<HeroTemplate>(templateRef);
            handle.Completed += op =>
            {
                var characterTemplate = op.Result;
                player = Instantiate(characterTemplate.Prefab, playerPivot);
                
                Assert.IsNotNull(player);

                var hp = playerConfig.Hp;
                var energy = playerConfig.Energy;
                var shield = playerConfig.Shield;
                var drawCount = playerConfig.DrawCount;
                
                hp.Value = characterTemplate.MaximumHp;
                energy.Value = characterTemplate.Energy;
                shield.Value = 0;
                
                energyResetSystem.SetDefaultMana(characterTemplate.Energy);

                if (PlayerPrefs.HasKey(saveDataPrefKey))
                {
                    var json = PlayerPrefs.GetString(saveDataPrefKey);
                    var saveData = JsonUtility.FromJson<SaveData>(json);
                    hp.Value = saveData.Hp;
                    shield.Value = saveData.Shield;
                    gold.Value = saveData.Gold;
                    
                
                    runDeck.Cards.Clear();
                    foreach (var id in saveData.Deck)
                    {

                        var card = fullDeck.Cards.Find(c => c.Id == id);
                        if (card == null)
                        {
                            card = characterTemplate.RewardDeck.Cards.Find(c => c.Id == id);
                        }
                        if (card != null)
                        {
                            runDeck.Add(card);
                        }
                    }
                }
                else
                {
                    gold.Value = characterTemplate.StartingGoldAmount;
                    runDeck.Cards.Clear();
                    foreach (var cardTemplate in characterTemplate.StartingDeck.Cards)
                    {
                        runDeck.Add(cardTemplate);
                    }
                }

                var gameInfo = FindObjectOfType<GameInfo>();
                if (gameInfo != null)
                {
                    gameInfo.SaveData.Hp = hp.Value;
                    gameInfo.SaveData.Shield = shield.Value;
                    gameInfo.SaveData.Deck.Clear();
                    foreach (var card in runDeck.Cards)
                    {
                        gameInfo.SaveData.Deck.Add(card.Id);
                    }
                }

                CreateHpWidget(playerConfig.HpWidget, player, hp, characterTemplate.MaximumHp, shield);
                CreateStatusWidget(playerConfig.StatusWidget, player);

                energyWidget.Initialize(energy);

                var obj = player.GetComponent<CharacterObject>();
                obj.Template = characterTemplate;
                obj.Character = new RuntimeCharacter
                { 
                    Hp = hp, 
                    Shield = shield,
                    Energy = energy, 
                    Status = playerConfig.Status,
                    MaxHp = characterTemplate.MaximumHp
                };
                obj.Character.Status.Value.Clear();

                playerConfig.DrawCount.Value = characterTemplate.BaseDrawAmount;
                
                numAssetsLoaded++;
                InitializeGame();
            };
        }

        private void CreateEnemy(AssetReference templateRef, int index)
        {
            var handle = Addressables.LoadAssetAsync<EnemyTemplate>(templateRef);
            handle.Completed += op =>
            {
                var pivots = enemyPivots1;
                var gameInfo = FindObjectOfType<GameInfo>();
                if (gameInfo.Encounter.Enemies.Count == 2)
                {
                    pivots = enemyPivots2;
                }

                var template = op.Result;
                var enemy = Instantiate(template.Prefab, pivots[index]);
                Assert.IsNotNull(enemy);

                var initialHp = Random.Range(template.HpLow, template.HpHigh + 1);
                var hp = enemyConfig[index].Hp;
                var shield = enemyConfig[index].Shield;
                hp.Value = initialHp;
                shield.Value = 0;

                CreateHpWidget(enemyConfig[index].HpWidget, enemy, hp, initialHp, shield);
                CreateStatusWidget(enemyConfig[index].StatusWidget, enemy);
                CreateIntentWidget(enemyConfig[index].IntentWidget, enemy);

                var obj = enemy.GetComponent<CharacterObject>();
                obj.Template = template;
                obj.Character = new RuntimeCharacter 
                { 
                    Hp = hp, 
                    Shield = shield,
                    Status = enemyConfig[index].Status,
                    MaxHp = enemyConfig[index].Hp.Value 
                };
                obj.Character.Status.Value.Clear();

                enemies.Add(enemy);

                numAssetsLoaded++;
                InitializeGame();
            };
        }

        private void InitializeGame()
        {
            if (numAssetsLoaded != numAssetsToLoad)
            {
                return;
            }

            deckDrawingSystem.Initialize(deckWidget, discardPileWidget);
            
            //var deckSize = deckDrawingSystem.LoadEncounterDrawDeck(playerDeck);
            deckDrawingSystem.ShuffleDeck();

            handPresentationSystem.Initialize(cardPool, deckWidget, discardPileWidget);

            var playerCharacter = player.GetComponent<CharacterObject>();
            var enemyCharacters = new List<CharacterObject>(enemies.Count);
            foreach (var enemy in enemies)
            {
                enemyCharacters.Add(enemy.GetComponent<CharacterObject>());
            }
            cardWithArrowSelectionSystem.Initialize(playerCharacter, enemyCharacters);
            enemyBrainSystem.Initialize(playerCharacter, enemyCharacters);
            effectResolutionSystem.Initialize(playerCharacter, enemyCharacters);
            poisonResolutionSystem.Initialize(playerCharacter, enemyCharacters);
            statusResolutionSystem.Initialize(playerCharacter, enemyCharacters);
            characterDeathSystem.Initialize(playerCharacter, enemyCharacters);

            turnManagementSystem.BeginGame();
        }

        private void CreateHpWidget(GameObject prefab, GameObject character, IntVariable hp, int maxHp, IntVariable shield)
        {
            var hpWidget = Instantiate(prefab, canvas.transform, false);
            var pivot = character.transform;
            var canvasPos = mainCamera.WorldToViewportPoint(pivot.position + new Vector3(0.0f, -0.5f, 0.0f));
            hpWidget.GetComponent<RectTransform>().anchorMin = canvasPos;
            hpWidget.GetComponent<RectTransform>().anchorMax = canvasPos;
            hpWidget.GetComponent<HpWidget>().Initialize(hp, maxHp, shield);
        }

        private void CreateStatusWidget(GameObject prefab, GameObject character)
        {
            var widget = Instantiate(prefab, canvas.transform, false);
            var pivot = character.transform;
            var canvasPos = mainCamera.WorldToViewportPoint(pivot.position + new Vector3(0.0f, -1.0f, 0.0f));
            widget.GetComponent<RectTransform>().anchorMin = canvasPos;
            widget.GetComponent<RectTransform>().anchorMax = canvasPos;
        }

        private void CreateIntentWidget(GameObject prefab, GameObject character)
        {
            var widget = Instantiate(prefab, canvas.transform, false);
            var pivot = character.transform;
            var size = character.GetComponent<BoxCollider2D>().bounds.size;
            var canvasPos = mainCamera.WorldToViewportPoint(
                pivot.position + new Vector3(-0.5f, size.y + 0.7f, 0.0f));
            widget.GetComponent<RectTransform>().anchorMin = canvasPos;
            widget.GetComponent<RectTransform>().anchorMax = canvasPos;
        }
    }
}
