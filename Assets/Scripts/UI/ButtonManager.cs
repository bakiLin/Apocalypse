using TMPro;
using UnityEngine;
using Zenject;

public class ButtonManager : MonoBehaviour
{
    [Inject]
    private AudioManager audioManager;

    [Inject]
    private SaveManager saveManager;

    [SerializeField]
    private GameObject dropdown, scanButton, leverButton;

    [SerializeField]
    private TextMeshProUGUI timerText;

    [SerializeField]
    private GameObject[] UI, windows;

    public bool IsLeverActive;

    private void Awake()
    {
        Time.timeScale = 1f;
    }

    public void SetWindow(GameObject window)
    {
        if (Time.timeScale == 1f || !window.activeSelf)
        {
            audioManager.Play("guide");
            foreach (GameObject item in UI) item.SetActive(false);
            foreach (GameObject item in windows) item.SetActive(false);
            scanButton.SetActive(false);
            leverButton.SetActive(false);
            window.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            foreach (GameObject item in UI) item.SetActive(true);
            leverButton.SetActive(IsLeverActive);
            scanButton.SetActive(!IsLeverActive);
            window.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    public void TimerTextChange(ref int time)
    {
        string seconds = (time % 60).ToString();
        if (time % 60 < 10) seconds = $"0{seconds}";
        timerText.text = $"{Mathf.Floor(time / 60)}:{seconds}";
        time--;
    }

    public void ResetTimerText()
    {
        timerText.text = "";
    }

    public void Dropdown()
    {
        dropdown.SetActive(!dropdown.activeSelf);
    }
}
