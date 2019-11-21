using System;
using System.Collections.Generic;
using Assets.Script.Advertisement;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.Monetization;
using UnityEngine.Advertisements;
using ShowResult = UnityEngine.Advertisements.ShowResult;

namespace Assets.Script
{
    public class GameManager : MonoBehaviour
    {
        
        public event Action<Config> OnStartGame;
        public event Action<int,int,ItemTypes> OnItemClick;
        public event Action<ItemTypes> ItemClicked;
        [SerializeField] private ResourceManager resourceManager;
        [SerializeField] private BoardManager boardManger;
        [SerializeField] private ScoreManager scoreManger;
        [SerializeField] private UIManager uIManager;
        [SerializeField] private AdManager adManager;
        private GameSave gameSave;
        private DataSaveManager<GameSave> dataManager;
        Player highScoredPlayer ;
        private int gameNumber;
        private ItemTypes currentType;
        private int itemTypesCount;
        private Config config;
        private float time;
        private UnityAdvertisement unityAdvertisement;
        private string gameID = "3264824";
        private bool testMod = false;

        public ScoreManager ScoreManager => scoreManger;
        private void Awake()
        {
            unityAdvertisement=new UnityAdvertisement(gameID,testMod);
            adManager.Init();
            dataManager = new DataSaveManager<GameSave>();
            highScoredPlayer=new Player("",0);
            dataManager.Load();
            gameNumber=dataManager.GetGameNumber();
            boardManger.Setup(this);
            boardManger.OnBoardUpdateScore += BoardUpdate;
            scoreManger.OnUpdateScore += UpdateDictionary;
            unityAdvertisement.ResultShow += ResultShow;
            SetHighScoreText();
            SetList();
        }
        public void StartGame(int gridSize,string pl1Name,string pl2Name)
        {
            time = Time.time;
            AnalyticsEvent.Custom("Start", new Dictionary<string, object>()
            {
                {"grid size", $"{gridSize+"X"+gridSize}"}
            });
            gameNumber++;
            gameSave = new GameSave();
            gameSave.gameNumber = gameNumber;
            gameSave.player1.Name = pl1Name;
            gameSave.player1.Score = 0;
            gameSave.player2.Name = pl2Name;
            gameSave.player1.Score = 0;
            
            currentType = ItemTypes.X;
            itemTypesCount = Enum.GetNames(typeof(ItemTypes)).Length;
            Config config = new Config(gridSize, gameSave);
            this.config = config;
            OnStartGame?.Invoke(config);

        }

        private void AddShow()
        {
            if (gameNumber % 5 == 0)
            {
                //adManager.ShowAd(placementId);
                //unityAdvertisement.Show(AdType.Rewarded);
                adManager.ShowAd(AdType.Rewarded);
            }
            else if (gameNumber % 1 == 0)

            {
              //  adManager.ShowAd(placementIdVideo);
              //unityAdvertisement.Show(AdType.Interstitial);
              adManager.ShowAd(AdType.Interstitial);
            }
        }

        private void ResultShow(ShowResult result)
        {
            switch(result)
            {
                case ShowResult.Finished:
                    Debug.Log ("Ad Finished. Rewarding player...");
                    break;
                case ShowResult.Skipped:
                    Debug.Log ("Ad skipped. Son, I am dissapointed in you");
                    break;
                case ShowResult.Failed:
                    Debug.Log("I swear this has never happened to me before");
                    break;
            }
        }

        public void SetList()
        {
            uIManager.GetList(dataManager.GameSaves);
        }
        public void SetHighScoreText()
        {
            GetHighScore();
            uIManager.GetHighScore(highScoredPlayer.Name,highScoredPlayer.Score);
        }
        public void GetHighScore()
        {
            for (int i = 0; i < dataManager.GameSaves.Count; i++)
            {
                GameSave gs = dataManager.GameSaves[i];
                Player p;
                if (gs.player1.Score > gs.player2.Score)
                    p = gs.player1;
                else p = gs.player2;

                if (p.Score > highScoredPlayer.Score)
                    highScoredPlayer = p;
            }
        }
        public void UpdateDictionary(ItemTypes player,int score)
        {
            uIManager.UpdatePlayerScore(player, score);
        }
        public void BoardUpdate(ItemTypes player , int score)
        {
            scoreManger.UpdateScore(player, score);
        }
        public void MovesOver()
        {
            AddShow();

            Winner();
            dataManager.GameSaves.Add(gameSave);
            dataManager.Save();
            SetHighScoreText();
            SetList();
            int t = (int) (Time.time - time);
            AnalyticsEvent.Custom("End Game", new Dictionary<string, object>
            {
                {"Time", t}
            });
        }
        public void Winner()
        {
            string playScore = gameSave.player1.Score > gameSave.player2.Score?
                $"{gameSave.player1.Name}    {gameSave.player1.Score}":
                $"{gameSave.player2.Name}    {gameSave.player2.Score}";
            uIManager.MovesOver(playScore);
            AnalyticsEvent.Custom ("Start Game",new Dictionary<string, object>
            {
                {"Game Number",gameSave.gameNumber},
                {"Player1 Name",gameSave.player1.Name},
                {"Player1 Score",gameSave.player1.Score},
                {"Player2 Name",gameSave.player2.Name},
                {"Player2 Score",gameSave.player2.Score}
            });
        }
        public void ResetGame()
        {
            boardManger.ResetBoard();
        }
        public void RestartGame()
        {
            boardManger.RestartGame();
            scoreManger.ResetDictionary();
        }
        public void OnItemButtonClick(int x, int y, ButtonItem buttonItem)
        {
            buttonItem.SetType((int)currentType);
            buttonItem.SetSprite(resourceManager.GetSpriteByItemType(currentType));
            buttonItem.SetInteractable(false);
            OnItemClick?.Invoke(x, y,currentType);
            ItemClicked?.Invoke(currentType);

            var index = (((int)currentType) + 1) % itemTypesCount;
            currentType = (ItemTypes)index;
        }

        private void UpdatePlayerScore(ItemTypes type, int score)
        {
            switch (type)
            {
                case ItemTypes.X:
                    gameSave.player1.Score = score;
                    break;
                case ItemTypes.O:
                    gameSave.player2.Score = score;
                    break;
            }
        }

        private void OnEnable()
        {
            scoreManger.OnUpdateScore += UpdatePlayerScore;
        }

        private void OnDisable()
        {
            scoreManger.OnUpdateScore -= UpdatePlayerScore;
        }
    }
    
}
