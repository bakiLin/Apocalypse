using UnityEngine;
using DG.Tweening;
using Zenject;

public class Scanner : MonoBehaviour
{
    [Inject]
    private AudioManager audioManager;

    [SerializeField]
    private Transform stencil, armTransform;

    [SerializeField]
    private LayerMask seeThroughLayer;

    [SerializeField]
    private float distance, endRotationX;

    private Vector3 startRotation;

    private RaycastHit hit;

    private bool isScanning;

    private Tween tween;

    private void Start()
    {
        startRotation = armTransform.localRotation.eulerAngles;
    }

    private void Update()
    {
        Scan();
    }

    private void Scan()
    {
        if (isScanning)
        {
            Physics.Raycast(Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)),
                out hit, distance, seeThroughLayer);

            if (hit.point != Vector3.zero)
            {
                stencil.position = hit.point;
                stencil.transform.LookAt(Camera.main.transform);
                if (!stencil.gameObject.activeSelf)
                    stencil.gameObject.SetActive(true);
            }
            else if (stencil.gameObject.activeSelf)
                stencil.gameObject.SetActive(false);
        }
    }

    public void SwitchScan()
    {
        tween.Kill();

        if (!isScanning)
        {
            tween = armTransform.DOLocalRotate(new Vector3(endRotationX, 0f, 0f), 1f)
                .OnComplete(() => {
                    isScanning = true;
                    audioManager.Play("scan");
                });
        }
        else
        {
            isScanning = false;
            stencil.gameObject.SetActive(false);
            audioManager.Stop("scan");
            tween = armTransform.DOLocalRotate(startRotation, 1f);
        }
    }
}
