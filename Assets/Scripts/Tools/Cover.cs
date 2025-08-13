using UnityEngine;
using Zenject;

public class Cover : MonoBehaviour
{
    [Inject]
    private EventManager eventManager;

    private void OnTriggerEnter(Collider other)
    {
        eventManager.IsCovered = true;
    }

    private void OnTriggerExit(Collider other)
    {
        eventManager.IsCovered = false;
    }
}
