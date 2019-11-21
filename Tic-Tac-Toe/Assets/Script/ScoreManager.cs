using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Script
{
    public class ScoreManager: MonoBehaviour
    {
        [SerializeField] private GameManager gameManager;
        
        public event Action<ItemTypes,int> OnUpdateScore;
        private ItemTypes player;
        private Dictionary<ItemTypes, int> players;
        private Winner winner;
        private int maxScore;
        private int itemTypesCount;
        public void Awake()
        {
            players = new Dictionary<ItemTypes, int>();
            itemTypesCount = Enum.GetNames(typeof(ItemTypes)).Length;
            player = new ItemTypes();
            var index = (((int) player) + 1) % itemTypesCount;
            player = (ItemTypes) index;
            players.Add(ItemTypes.O, 0);
            players.Add(ItemTypes.X, 0);
        }

        public void ResetDictionary()
        {
            ClearPlayersDictionary();
            players.Add(ItemTypes.O, 0);
            players.Add(ItemTypes.X, 0);
        }
        public void UpdateScore(ItemTypes _player, int score)
        {
            players[_player] += score;
            OnUpdateScore?.Invoke(_player, players[_player]);
        }
        public void ClearPlayersDictionary()
        {
            players.Clear();
        }
    }
    public struct Winner
    {
        public ItemTypes winner;
        public int score;

        public Winner(ItemTypes winner, int score)
        {
            this.winner = winner;
            this.score = score;
        }
    }
}