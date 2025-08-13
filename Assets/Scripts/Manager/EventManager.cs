using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using Zenject;
using Random = System.Random;

public class EventManager : MonoBehaviour
{
    [Inject]
    private Lever lever;

    [Inject]
    private ChoiceManager choiceManager;

    [Inject]
    private HpManager hpManager;

    [Inject]
    private MoneyManager moneyManager;

    [Inject]
    private AudioManager audioManager;

    [Inject]
    private ButtonManager buttonManager;

    [SerializeField]
    private int eventCooldown, escapeTime, rainTime;

    [SerializeField]
    private float glowSpeed;

    [SerializeField]
    private Volume volume;

    [SerializeField]
    private ParticleSystem particle;

    private Vignette vignette;

    private bool isCovered;

    private Random random = new Random();

    private CancellationTokenSource cts;

    public bool IsCovered { get => isCovered; set => isCovered = value; }

    public Action OnEventEnd;

    private void Awake()
    {
        volume.profile.TryGet<Vignette>(out vignette);
    }

    private void Start()
    {
        TimeBeforeEventAsync();
    }

    private async void TimeBeforeEventAsync()
    {
        cts = new CancellationTokenSource();
        await UniTask.Delay(eventCooldown * 1000);
        choiceManager.LockChoice(true);

        int index = random.Next(0, 2);
        if (index == 0) EscapeEventAsync(cts.Token);
        else RainEventAsync();
    }

    private async void EscapeEventAsync(CancellationToken token)
    {
        audioManager.Play("escape", true);
        lever.SetUp();
        VignetteGlowAsync(cts.Token);

        int time = escapeTime;
        while (time >= 0)
        {
            if (token.IsCancellationRequested) return;
            buttonManager.TimerTextChange(ref time);
            await UniTask.Delay(1000);
        }

        EventEnd("escape");
        lever.SetDown();
        hpManager.ReduceHp(2);
    }

    private async void RainEventAsync()
    {
        audioManager.Play("rain", true);
        particle.Play();
        VignetteGlowAsync(cts.Token);

        int time = rainTime;
        while (time >= 0)
        {
            if (!isCovered && time != rainTime && (rainTime - time) % 7 == 0)
                hpManager.ReduceHp();
            buttonManager.TimerTextChange(ref time);
            await UniTask.Delay(1000);
        }

        EventEnd("rain");
        particle.Stop();
        moneyManager.AddMoney(50);
    }

    private void EventEnd(string eventName)
    {
        audioManager.Stop(eventName, true);
        cts.Cancel();
        buttonManager.ResetTimerText();
        choiceManager.LockChoice(false);
        ResetVignetteAsync();
    }

    private async void VignetteGlowAsync(CancellationToken token)
    {
        bool up = true;
        while (!token.IsCancellationRequested)
        {
            if (up)
            {
                if (vignette.intensity.value < .25f)
                    vignette.intensity.value += Time.deltaTime * glowSpeed;
                else
                    up = false;
            }
            else
            {
                if (vignette.intensity.value > .1f)
                    vignette.intensity.value -= Time.deltaTime * glowSpeed;
                else
                    up = true;
            }
            await UniTask.Yield();
        }
    }

    private async void ResetVignetteAsync()
    {
        while (vignette.intensity.value > 0f)
        {
            vignette.intensity.value -= Time.deltaTime * glowSpeed;
            await UniTask.Yield();
        }

        OnEventEnd?.Invoke();
        TimeBeforeEventAsync();
    }

    public void LeverDown()
    {
        if (lever.IsActive())
        {
            audioManager.Play("lever");
            audioManager.Stop("escape", true);
            cts.Cancel();
            lever.SetDown();
            buttonManager.ResetTimerText();
            choiceManager.LockChoice(false);
            moneyManager.AddMoney(50);
            ResetVignetteAsync();
        }
    }
}
