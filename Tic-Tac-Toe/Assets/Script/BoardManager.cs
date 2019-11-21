using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Script;
using System;
using UnityEngine.UI;

public class BoardManager : MonoBehaviour
{
    public event Action<ItemTypes, int> OnBoardUpdateScore;
    [Header("Board Properties")]
    [SerializeField] private ButtonItem itemPrefab;
    [SerializeField] private RectTransform gridContainer;
    [SerializeField] private RectTransform linePrefab;
    [SerializeField] private RectTransform lineRendererPrefab;
    [Space]

    private Config config;
    private int[,] board;
    private Vector2[,] boardPos;
    private int clickedSum;
    private int buttonN;
    private GameManager gm;
    private void Awake()
    {
        gm.OnStartGame += StartGame;
        gm.OnItemClick += UpdateBoard;
    }
    public void StartGame(Config config)
    {
        this.config = config;
        EmptyBoard();
        createItems();
        BoardPosition();
        buttonN = this.config.Size * this.config.Size;
       
    }

    public void Setup(GameManager gm)
    {
        this.gm = gm;
    }
    public void createItems()
    {
        for (int y = 0; y < config.Size; y++)
        {
            for (int x = 0; x < config.Size; x++)
            {
                var item = Instantiate(itemPrefab, gridContainer);
                item.SetCoord(x, y);
                item.OnButtonClicked += OnItemButtonClick;
            }
        }
    }
    public void EmptyBoard()
    {
        board = new int[config.Size, config.Size];
        for (int yy = 0; yy < config.Size; yy++)
        {
            for (int xx = 0; xx < config.Size; xx++)
            {
                board[xx, yy] = -1;
            }
        }
    }
    public void MovesFinished()
    {
        gm.MovesOver();
    }
    private void OnItemButtonClick(int x, int y, ButtonItem buttonItem)
    {
        gm.OnItemButtonClick(x, y, buttonItem);
        clickedSum++;
        if (clickedSum == buttonN)
        {
            MovesFinished();
        }
    }
    private void UpdateBoard(int x,int y,ItemTypes item)
    {
        board[x, y] = (int)item;
        if (Check(x, y, (int)item) == 1)
            OnBoardUpdateScore?.Invoke(item, 1);
    }
    private int Check( int x, int y,int indSprite)
    {
        int n = 0;

        int m = 1;
        int xx = x;
        while (++xx < config.Size)
        {
            
            if (board[xx, y] == indSprite)
            {

                m++;
                if (m == 3)
                {
//                    InstLineRenderer(boardPos[xx,y],boardPos[xx-2,y]);
                    InstLine((xx - 1), y, 90);
                    for (int i = 0; i < 3; i++)
                    {
                        board[xx - i, y] = 2;
                    }
                    n++;
                }
            }
            else
                break;
        }

        m = 1;
        xx = x;
        while (--xx >= 0)
        {
            if (board[xx, y] == indSprite)
            {
                m++;
                if (m == 3)
                {
                    InstLine((xx + 1), y, 90);
                    for (int i = 0; i < 3; i++)
                    {
                        board[xx + i, y] = 2;
                    }
                    n++;
                }
            }
            else
                break;
        }

        m = 1;
        int yy = y;
        while (++yy < config.Size)
        {
            if (board[x, yy] == indSprite)
            {
                m++;
                if (m == 3)
                {
                    InstLine(x, (yy - 1), 0);
                    for (int i = 0; i < 3; i++)
                    {
                        board[x, yy - i] = 2;
                    }
                    n++;
                }
            }
            else
                break;
        }
        m = 1;
        yy = y;
        while (--yy >=0)
        {
            if (board[x, yy] == indSprite)
            {
                m++;
                if (m == 3)
                {
                    InstLine(x, (yy + 1), 0);
                    for (int i = 0; i < 3; i++)
                    {
                        board[x, yy + i] = 2;
                    }
                    n++;
                }
            }
            else
                break;
        }

        m = 1;
        xx = x;
        yy = y;
        while (--xx >= 0 && ++yy < config.Size)
        {
            if (board[xx, yy] == indSprite)
            {
                m++;
                if (m == 3)
                {
                    InstLine((xx + 1), (yy - 1), -45);
                    for (int i = 0; i < 3; i++)
                    {
                        board[xx + i, yy - i] = 2;
                    }
                    n++;
                }
            }
            else
                break;
        }
        m = 1;
        xx = x;
        yy = y;
        while (++xx < config.Size && --yy >=0)
        {
            if (board[xx, yy] == indSprite)
            {
                m++;
                if (m == 3)
                {
                    InstLine((xx - 1), (yy + 1), -45);
                    for (int i = 0; i < 3; i++)
                    {
                        board[xx - i, yy + i] = 2;
                    }
                    n++;
                }
            }
            else
                break;
        }

        m = 1;
        xx = x;
        yy = y;
        while (++yy < config.Size && ++xx < config.Size)
        {
            if (board[xx, yy] == indSprite)
            {
                m++;
                if (m == 3)
                {
                    InstLine((xx - 1), (yy - 1), 45);
                    for (int i = 0; i < 3; i++)
                    {
                        board[xx - i, yy - i] = 2;
                    }
                    n++;
                }
            }
            else
                break;
        }

        m = 1;
        xx = x;
        yy = y;
        while (--yy >= 0 && --xx >=0)
        {
            if (board[xx, yy] == indSprite)
            {
                m++;
                if (m == 3)
                {
                    InstLine((xx + 1), (yy + 1), 45);
                    for (int i = 0; i < 3; i++)
                    {
                        board[xx + i, yy + i] = 2;
                    }
                    n++;
                }
            }
            else
                break;
        }

        return n;
    }
    private void InstLine(int x, int y, float angle)
    {
        var line = Instantiate(linePrefab, gridContainer);
        //line.transform.parent = gridContainer.transform;
        line.anchoredPosition = boardPos[x, y];
        line.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void InstLineRenderer(Vector2 pos1, Vector2 pos2)
    {
        var line = Instantiate(lineRendererPrefab, gridContainer);
        line.transform.SetParent(gridContainer.transform);
        LineRenderer rendLine= line.GetComponent<LineRenderer>();
        rendLine.material=new Material(Shader.Find("Sprites/Default"));
        Vector3[] pos=new Vector3[2];
        pos[0]= new Vector3(pos1.x,pos1.y);
        pos[1] = new Vector3(pos2.x,pos2.y);
    }

    public void FlushBoard()
    {
        int childs = gridContainer.childCount;
        for (int i = 0; i < childs; i++)
        {
            Destroy(gridContainer.GetChild(i).gameObject);
        }
        gm.ScoreManager.ResetDictionary();
    }

    private void BoardPosition()
    {
        var containerSize = gridContainer.sizeDelta.y;
        var spaceing = 20;
        float cellSize = (containerSize - (config.Size + 1) * spaceing) / config.Size;
        boardPos= new Vector2[config.Size, config.Size];
        for (int y = 0; y < config.Size; y++)
        {
            for (int x = 0; x < config.Size; x++)
            {
                boardPos[x, y] = new Vector2(cellSize / 2 + spaceing + x * (cellSize + spaceing), -(cellSize / 2 + spaceing) - y * (cellSize + spaceing));
            }
        }

    }
    public void RestartGame()
    {
        ResetBoard();
        createItems();
    }
    public void ResetBoard()
    {
        EmptyBoard();
        FlushBoard();
        clickedSum = 0;
    }
}
