using Zenject;

public class Installer : MonoInstaller
{
    public VariableJoystick VariableJoystick;

    public ChoiceManager ChoiceManager;

    public HpManager HpManager;

    public MoneyManager MoneyManager;

    public AudioManager AudioManager;

    public NpcSpawner NpcSpawner;

    public LampManager LampManager;

    public EventManager EventManager;

    public SaveManager SaveManager;

    public QuestManager QuestManager;

    public ButtonManager ButtonManager;

    public Lever Lever;

    public override void InstallBindings()
    {
        Container.Bind<VariableJoystick>().FromInstance(VariableJoystick).AsSingle().NonLazy();

        Container.Bind<ChoiceManager>().FromInstance(ChoiceManager).AsSingle().NonLazy();

        Container.Bind<HpManager>().FromInstance(HpManager).AsSingle().NonLazy();

        Container.Bind<MoneyManager>().FromInstance(MoneyManager).AsSingle().NonLazy();

        Container.Bind<AudioManager>().FromInstance(AudioManager).AsSingle().NonLazy();

        Container.Bind<NpcSpawner>().FromInstance(NpcSpawner).AsSingle().NonLazy();

        Container.Bind<LampManager>().FromInstance(LampManager).AsSingle().NonLazy();

        Container.Bind<ButtonManager>().FromInstance(ButtonManager).AsSingle().NonLazy();

        Container.Bind<EventManager>().FromInstance(EventManager).AsSingle().NonLazy();

        Container.Bind<SaveManager>().FromInstance(SaveManager).AsSingle().NonLazy();

        Container.Bind<QuestManager>().FromInstance(QuestManager).AsSingle().NonLazy();

        Container.Bind<Lever>().FromInstance(Lever).AsSingle().NonLazy();
    }
}