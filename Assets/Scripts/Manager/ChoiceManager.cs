using System;
using UnityEngine;
using Zenject;

public class ChoiceManager : MonoBehaviour
{
    [Inject]
    private HpManager hpManager;

    [Inject]
    private LampManager lampManager;

    [SerializeField]
    private GameObject choiceUI;

    private NpcMovement npcMovement;

    private bool isScanning;

    private bool isActive = true;

    public Action OnScan;

    public void ChoiceWindow(NpcMovement value)
    {
        isScanning = true;
        npcMovement = value;

        if (isActive)
        {
            choiceUI.SetActive(true);
            lampManager.TurnOn();
        }
    }

    public void MakeChoice(bool value)
    {
        isScanning = false;
        choiceUI.SetActive(false);
        hpManager.CheckInfection(value);
        npcMovement.WalkChoice(value);
        OnScan?.Invoke();
    }

    public void LockChoice(bool state)
    {
        isActive = !state;
        if (state) choiceUI.SetActive(!state);
        else if (isScanning) choiceUI.SetActive(!state);
    }
}
