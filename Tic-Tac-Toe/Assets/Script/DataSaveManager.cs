using System.Collections.Generic;
using System.IO;
using UnityEngine;
using  Newtonsoft.Json;

namespace Assets.Script
{
    public class DataSaveManager<T>
    {
        private string fileName;
        public List<T> GameSaves { get; private set; }
       
        public DataSaveManager( string fileName  = "save.json")
        {
            GameSaves = new List<T>();
            this.fileName = fileName;
        }
        public void Save()
        {
            string path = Path.Combine(Application.persistentDataPath, fileName);
            string json = JsonConvert.SerializeObject(GameSaves);
            File.WriteAllText(path, json);
        }

        public void Load()
        {
            string path = Path.Combine(Application.persistentDataPath, fileName);
            if (!File.Exists(path))
            {
                return ;
            }
            string json = File.ReadAllText(path);
            GameSaves = JsonConvert.DeserializeObject<List<T>>(json);

            if (GameSaves == null)
            {
                GameSaves = new List<T>();
            }
        }
        public int GetGameNumber()
        {
            int gameNumber =GameSaves.Count;
            return gameNumber;
        }
    }
    public struct GameSave
    {
        public int gameNumber;
        public Player player1;
        public Player player2;
    }

    public struct Player
    {
        public string Name { get;  set; }
        public int Score { get;  set; }

        public Player(string name, int score)
        {
            this.Name = name;
            this.Score = score;
        }
    }
}
