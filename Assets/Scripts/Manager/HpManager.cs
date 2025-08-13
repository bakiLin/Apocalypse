using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class HpManager : MonoBehaviour
{
    [Inject]
    private MoneyManager moneyManager;

    [Inject]
    private AudioManager audioManager;

    [Inject]
    private SaveManager saveManager;

    [SerializeField]
    private GameObject gameOverWindow;

    [SerializeField]
    private Image fillHp;

    [SerializeField]
    private int medkitPrice;

    public bool IsInfected;

    private void OnEnable()
    {
        saveManager.OnLoadHealth += LoadHealth;
    }

    private void OnDisable()
    {
        saveManager.OnLoadHealth -= LoadHealth;
    }

    private void LoadHealth(float value)
    {
        fillHp.fillAmount = value;
        CheckGameOver();
    }

    public void CheckInfection(bool state)
    {
        if ((state && IsInfected) || (!state && !IsInfected)) 
            WrongChoice();
        else
        {
            audioManager.Play("good");
            moneyManager.CorrectAnswer();
        }
    }

    private void WrongChoice()
    {
        audioManager.Play("bad");
        fillHp.fillAmount -= .1f;
        CheckGameOver();
        saveManager.SetHealth(fillHp.fillAmount);
    }

    private void CheckGameOver()
    {
        if (fillHp.fillAmount == 0f)
        {
            gameOverWindow.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void ReduceHp(int amount = 1)
    {
        fillHp.fillAmount -= .1f * amount;
        saveManager.SetHealth(fillHp.fillAmount);
    }

    public void UseMedkit()
    {
        if (moneyManager.Money >= medkitPrice && fillHp.fillAmount < 1f)
        {
            moneyManager.AddMoney(-medkitPrice);
            fillHp.fillAmount += .4f;
            saveManager.SetHealth(fillHp.fillAmount);
        }
    }
}
