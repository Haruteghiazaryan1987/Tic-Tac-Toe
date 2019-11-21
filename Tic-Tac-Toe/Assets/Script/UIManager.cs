using Assets.Script;
using System;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private MenuView menuView;
    [SerializeField] private GameView gameView;
    [SerializeField] private ScoreView scoreView;
    [SerializeField] private PopUpController popUpController;
    public event Action OnYesClikc;
    public event Action OnNoClikc;

    private void Awake()
    {
        menuView.Setup(this);
        gameView.Setup(this);
        scoreView.Setup(this);
        gameManager.ItemClicked += ItemClic;
        gameManager.OnStartGame += StartGameView;

        OnYesClikc += gameView.RestGame;
        OnNoClikc += gameView.ReturnТoМenu;
    }
    public void ItemClic(ItemTypes player)
    {
        gameView.UpdateBoard(player);
    }

    public void GetList(List<GameSave> gameSave)
    {
        scoreView.InstantiateTextPref(gameSave);
    }

    public void GetHighScore(string name, int score)
    {
        scoreView.GetHighScoreText(name,score);
    }
    public void UpdatePlayerScore(ItemTypes player,int score)
    {
        gameView.UpdateScorePlayer(player, score);
    }
    public void StartGameView(Config config)
    {
        gameView.StartGame(config);
    }
    public void MovesOver(string PlayerScore)
    {
        popUpController.Show("", PlayerScore, OnYesClikc, OnNoClikc);
    }

//    public void PopupShow(string info)
//    {
//        popUpController.ShowTitle(info,OnYesClikc);
//    }

    public void OpenGameView(int gridSize,string namePl1,string namePl2)
    {
        menuView.gameObject.SetActive(false);
        scoreView.gameObject.SetActive(false);
        gameView.gameObject.SetActive(true);
        gameManager.StartGame(gridSize,namePl1,namePl2);
    }

    public void OpenMainHighScore()
    {
        menuView.gameObject.SetActive(false);
        gameView.gameObject.SetActive(false);
        scoreView.gameObject.SetActive(true);
    }

    public void OpenMainMenu()
    {
        menuView.gameObject.SetActive(true);
        gameView.gameObject.SetActive(false);
        scoreView.gameObject.SetActive(false);
        gameManager.ResetGame();
    }
    public void OpenMainMenuScoreView()
    {
        menuView.gameObject.SetActive(true);
        gameView.gameObject.SetActive(false);
        scoreView.gameObject.SetActive(false);
    }
    public void RestartGame()
    {
        gameManager.RestartGame();
    }
}
