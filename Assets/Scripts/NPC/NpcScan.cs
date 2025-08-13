using UnityEngine;
using Zenject;
using Random = System.Random;

public class NpcScan : MonoBehaviour
{
    [Inject]
    private HpManager hpManager;

    private Random random = new Random();

    private NpcMaterial material;

    private void Awake()
    {
        material = GetComponent<NpcMaterial>();
    }

    public void SetInfection()
    {
        bool isInfected = random.Next(0, 2) == 0 ? false : true;
        hpManager.IsInfected = isInfected;
        material.ResetMaterials();
        if (isInfected) material.SetInfection();
    }
}
