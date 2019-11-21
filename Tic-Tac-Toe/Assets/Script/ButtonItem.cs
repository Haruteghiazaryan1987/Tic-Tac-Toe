using UnityEngine;
using UnityEngine.UI;
public delegate void OnButtonClickDelegate(int x, int y,ButtonItem buttonItem);

public class ButtonItem : MonoBehaviour
{
    public OnButtonClickDelegate OnButtonClicked;

   [SerializeField]private Button button;
   [SerializeField]private Image image;
    private int x;
    private int y;
    private int type=-1;
    private void Start()
    {
        button.onClick.AddListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        OnButtonClicked?.Invoke(x, y,this);
    }

    public void SetCoord(int x,int y)
    {
        this.x = x;
        this.y = y;
    }

    public void SetType(int _type)
    {
        type = _type;
    }

    public void SetSprite(Sprite sprite)
    {
        image.sprite = sprite;
    }

    public void SetInteractable(bool value)
    {
        button.interactable = value;
    }
}
