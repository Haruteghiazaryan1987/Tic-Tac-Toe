
using System;
using UnityEngine;
using UnityEngine.UI;

public class PopUpController : MonoBehaviour
{
    [SerializeField] Text titleText;
    [SerializeField] Text infoText;
    [SerializeField] Button yesButton;
    [SerializeField] Button noButton;
    [SerializeField] GameObject popUpController;

    public void Show(string title, string infoText, Action onYes, Action onNo)
    {
        popUpController.SetActive(true);
        this.infoText.text = infoText;
        yesButton.onClick.RemoveAllListeners();
        yesButton.onClick.AddListener(() =>
        {
            onYes?.Invoke();
            Close();
        });
        noButton.onClick.RemoveAllListeners();
        noButton.onClick.AddListener(() =>
        {
            onNo?.Invoke();
            Close();
        });
    }
    private void Close()
    {
        popUpController.SetActive(false);
    }
}
