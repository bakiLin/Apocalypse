using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

public class NpcMovement : MonoBehaviour
{
    [Inject]
    private ChoiceManager choiceManager;

    [Inject]
    private NpcSpawner npcSpawner;

    private NavMeshAgent agent;

    private Animator animator;

    private CancellationTokenSource cts;

    private delegate void ReachDestination();

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        cts = new CancellationTokenSource();
    }

    private void OnDestroy()
    {
        cts?.Cancel();
        cts?.Dispose();
    }

    public void MoveToScan()
    {
        agent.SetDestination(npcSpawner.scanPoint.position);

        CheckDestinationAsync(() => {
            animator.SetBool("idle", true);
            choiceManager.ChoiceWindow(this);
            cts?.Cancel();
        }, cts.Token).Forget();
    }

    private async UniTask CheckDestinationAsync(ReachDestination reachDestination, CancellationToken token)
    {
        while (!token.IsCancellationRequested)
        {
            if (transform.position == agent.destination)
                reachDestination();

            await UniTask.Yield(cancellationToken: token);
        }
    }

    public void WalkChoice(bool value)
    {
        animator.SetBool("idle", false);
        if (value) agent.SetDestination(npcSpawner.campPoint.position);
        else agent.SetDestination(npcSpawner.rejectPoint.position);

        cts = new CancellationTokenSource();
        CheckDestinationAsync(() => {
            gameObject.SetActive(false);
            cts?.Cancel();
        }, cts.Token).Forget();
    }
}
