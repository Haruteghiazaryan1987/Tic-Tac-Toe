using System.Collections.Generic;
using Assets.Script;
using UnityEngine;
using UnityEngine.UI;

public class ScoreView : MonoBehaviour
{
    [SerializeField] private Button menu;
    [SerializeField] private Text highScoreText;
    [SerializeField] private RectTransform textPrefab;
    [SerializeField] private RectTransform content;
    private Leaderboarditem lidBord;
    private UIManager ui;
    
    public void Setup(UIManager ui)
    {
        this.ui = ui;
        menu.onClick.AddListener(ReturnТoМenu);
    }

    public void GetHighScoreText(string name, int score)
    {
        highScoreText.text = name + "   Score" + score;
    }
    public void InstantiateTextPref(List<GameSave> gamSave)
    {
        if (content.childCount != 0)
        {
            for (int i = 0; i < content.childCount; i++)
            {
                Destroy(content.GetChild(i).gameObject);
            }
        }
        for (int i = gamSave.Count-1 ; i >-1; i--)
        {
            GameSave gs = gamSave[i];
            var obj = Instantiate(textPrefab, content);
            obj.GetComponent<Leaderboarditem>().Setup(gs);
        }
    }

    public void ReturnТoМenu()
    {
        ui.OpenMainMenuScoreView();
    }

}
