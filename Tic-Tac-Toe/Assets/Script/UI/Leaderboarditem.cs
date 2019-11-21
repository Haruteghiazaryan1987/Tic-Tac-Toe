using System;
using Assets.Script;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Script
{
    public class Leaderboarditem : MonoBehaviour
    {
        [SerializeField] private Text numberGame;
        [SerializeField] private Text player1Text;
        [SerializeField] private Text player2Text;
        [SerializeField] private Color green;
        [SerializeField] private Color white;
        [SerializeField] private Color blue;

        public void Setup(GameSave data)
        {
            green = new Color(0, 255, 0);
            string pl1Name = ($"{data.player1.Name}".Length>10)? $"{data.player1.Name}".Remove(8)+"...":$"{data.player1.Name}" ;
            player1Text.text = pl1Name + "-" + $"{data.player1.Score}";
            string pl2Name =($"{data.player2.Name}".Length>10)? $"{data.player2.Name}".Remove(8)+"...":$"{data.player2.Name}" ;
            player2Text.text = pl2Name+ "-" + $"{data.player2.Score}";
            numberGame.text = $"{data.gameNumber}";
            if (data.player1.Score > data.player2.Score)
            {
                player1Text.color = blue;
                
            }

            if (data.player1.Score < data.player2.Score)
            {
                player2Text.color = blue;
            }
        }
    }
}