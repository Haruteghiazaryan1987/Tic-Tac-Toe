using UnityEngine;
using UnityEngine.UI;


namespace Assets.Script
{
    public class TextPlayer:MonoBehaviour
    {
        [SerializeField] private Text player1;
        [SerializeField] private Text player2;
        [SerializeField]private Color playablePlayerColor;
        [SerializeField]private Color observerPlayerColor;
        [SerializeField]private GameView gameView;
        private string pl1;
        private string pl2;
        private GameView gv;

        public void Setup(GameView gv)
        {
            this.gv = gv;
            player2.color = observerPlayerColor;
            player1.color = playablePlayerColor;
        }
        public void PlayersName(string pl1Name,string pl2Name)
        {
            pl1 = pl1Name;
            pl2 = pl2Name;
            player1.text = pl1 + "  " + "X" + "  " + 0;
            player2.text = pl2 + "  " + "O" + "  " + 0;
        }
        public void ResetTextPlay()
        {
            player1.text = pl1 + "  " + "X" + "  " + 0;
            player2.text = pl2 + "  " + "O" + "  " + 0;
        }
        public void TextColor(ItemTypes player)
        {
            if (player ==(ItemTypes) 1)
            {
                player2.color = playablePlayerColor;
                player1.color = observerPlayerColor;
            }
            else
            {
                player2.color = observerPlayerColor;
                player1.color = playablePlayerColor;
            }
        }
        public void TextScore(ItemTypes player,int score)
        {
            if (player == ItemTypes. X)
            {
                player1.text = pl1 + "  " + player + "  " + score;
            }
            else
            {
                player2.text = pl2 + "  " + player + "  " + score;
            }
        }
    }
}
