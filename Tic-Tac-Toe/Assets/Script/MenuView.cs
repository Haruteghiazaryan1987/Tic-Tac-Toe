using UnityEngine;
using UnityEngine.UI;

public class MenuView : MonoBehaviour
{
    [SerializeField] private Button startGameButton;
    [SerializeField] private Button highScore;
    [SerializeField] private InputField inputField;
    [SerializeField] private InputField nameInputPl1;
    [SerializeField] private InputField nameInputPl2;
    private string player1;
    private string player2;
    private int gridSize=3;
    private UIManager ui;

    public void Setup(UIManager ui)
    {
        this.ui = ui;
        player1 = "Player1" + Random.Range(1000, 10000);
        player2 = "Player2" + Random.Range(1000, 10000);
        startGameButton.onClick.AddListener(OnStartButtonClick);
        highScore.onClick.AddListener(OnHighScoreClick);
    }

    private void OnHighScoreClick()
    {
        ui.OpenMainHighScore();
    }

    private void OnStartButtonClick()
    {
        ui.OpenGameView(gridSize,player1,player2);
    }
    public void GetGridSize()
    {
        gridSize = int.Parse(inputField.text);
        if (gridSize < 3)
            gridSize = 3;
        if (gridSize > 10)
            gridSize = 10;
    }
    public void GetPlayer1Name()
    {
        player1= string.Intern(nameInputPl1.text);
        if(player1.Length==0)
            player1="Player-" + Random.Range(1000, 10000);
    }
    public void GetPlayer2Name()
    {
        player2 = string.Intern(nameInputPl2.text);
        if (player2.Length == 0)
            player2 = "Player-" + Random.Range(1000, 10000);
    }
}
