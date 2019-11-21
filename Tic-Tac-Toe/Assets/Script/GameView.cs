using UnityEngine;
using Assets.Script;
using UnityEngine.UI;

public class GameView : MonoBehaviour
{
    [SerializeField] private RectTransform container;
    [SerializeField] private GridLayoutGroup containerLayout;
    [SerializeField] private Button  menuButton;
    [SerializeField] private Button restartButton;
    [SerializeField] private TextPlayer textPlayer;

    private UIManager ui;
    public void Setup(UIManager ui)
    {
        this.ui = ui;
        textPlayer.Setup(this);
        menuButton.onClick.AddListener(ReturnТoМenu);
        restartButton.onClick.AddListener(RestGame);
    }
    public void ReturnТoМenu()
    {
        ui.OpenMainMenu();
        textPlayer.ResetTextPlay();
    }
    public void RestGame()
    {
        ui.RestartGame();
        textPlayer.ResetTextPlay();
    }
    public void StartGame(Config config)
    {
        configureBoard(config);
        textPlayer.PlayersName(config.gameSave.player1.Name, config.gameSave.player2.Name);
    }
    private void configureBoard(Config config)
    {
        float containerSize = container.sizeDelta.y;
        float spacing = containerLayout.spacing.x;
        containerLayout.spacing = Vector2.one * spacing;
        float cellSize = (containerSize - (config.Size + 1) * spacing) / config.Size;
        containerLayout.cellSize = Vector2.one * cellSize;
    }
    public void UpdateScorePlayer(ItemTypes player,int score)
    {
        textPlayer.TextScore(player,score);
    }
    public void UpdateBoard(ItemTypes type)
    {
        textPlayer.TextColor(type);
    }
}
