using UnityEngine;
using Zenject;

public class Lever : MonoBehaviour
{
    [Inject]
    private ButtonManager buttonManager;

    [SerializeField]
    private GameObject scanButton, leverButton;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        scanButton.SetActive(false);
        leverButton.SetActive(true);
        buttonManager.IsLeverActive = true;
    }

    private void OnTriggerExit(Collider other)
    {
        scanButton.SetActive(true);
        leverButton.SetActive(false);
        buttonManager.IsLeverActive = false;
    }

    public void SetUp() => animator.SetBool("up", true);

    public void SetDown() => animator.SetBool("up", false);

    public bool IsActive() => animator.GetBool("up");
}
