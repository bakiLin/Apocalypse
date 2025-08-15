using UnityEngine;
using Zenject;
using Random = System.Random;

public class NpcSpawner : MonoBehaviour
{
    [Inject]
    private ChoiceManager choiceManager;

    private Pooler pooler;

    private Random random = new Random();

    private int prevIndex, currentIndex;

    public Transform spawnPoint, scanPoint, campPoint, rejectPoint;

    private void Awake()
    {
        pooler = GetComponent<Pooler>();
    }

    private void OnEnable()
    {
        choiceManager.OnScan += DelayedSpawn;
    }

    private void OnDisable()
    {
        choiceManager.OnScan -= DelayedSpawn;
    }

    private void Start()
    {
        DelayedSpawn();
    }

    private void DelayedSpawn()
    {
        Invoke("Spawn", 1f);
    }

    private void Spawn()
    {
        do currentIndex = random.Next(0, 3);
        while (currentIndex == prevIndex);
        prevIndex = currentIndex;

        //var npc = pooler.Spawn(currentIndex.ToString(), spawnPoint.position, new Vector3(0f, 180f, 0f));
        //npc.GetComponent<NpcMovement>().MoveToScan();
        //npc.GetComponent<NpcScan>().SetInfection();
    }
}
