using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = System.Random;

public class NpcMaterial : MonoBehaviour
{
    [System.Serializable]
    public class Infection
    {
        public int id;
        public Material material;
    }

    [SerializeField]
    private SkinnedMeshRenderer meshRenderer, meshRenderer2;

    [SerializeField]
    private List<Infection> infection, infection2;

    private Random random = new Random();

    private Material[] normalMaterials, normalMaterials2;

    public void ResetMaterials()
    {
        if (normalMaterials == null)
        {
            normalMaterials = new Material[meshRenderer.materials.Length];
            for (int i = 0; i < normalMaterials.Length; i++) normalMaterials[i] = meshRenderer.materials[i];

            if (!meshRenderer2.IsUnityNull())
            {
                normalMaterials2 = new Material[meshRenderer2.materials.Length];
                for (int i = 0; i < normalMaterials2.Length; i++) normalMaterials2[i] = meshRenderer2.materials[i];
            }
        }

        Material[] temp = new Material[meshRenderer.materials.Length];
        for (int i = 0; i < meshRenderer.materials.Length; i++) temp[i] = normalMaterials[i];
        meshRenderer.materials = temp;

        if (!meshRenderer2.IsUnityNull())
        {
            temp = new Material[meshRenderer2.materials.Length];
            for (int i = 0; i < meshRenderer2.materials.Length; i++) temp[i] = normalMaterials2[i];
            meshRenderer2.materials = temp;
        }
    }

    public void SetInfection()
    {
        int index = random.Next(0, infection.Count + infection2.Count);

        if (index < infection.Count)
        {
            var temp = meshRenderer.materials;
            temp[infection[index].id] = infection[index].material;
            meshRenderer.materials = temp;
        }
        else
        {
            var temp = meshRenderer2.materials;
            temp[infection2[index - infection.Count].id] = infection2[index - infection.Count].material;
            meshRenderer2.materials = temp;
        }
    }
}
