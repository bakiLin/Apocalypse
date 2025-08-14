using Scellecs.Morpeh;
using UnityEngine;

public class Startup : MonoBehaviour
{
    private World world;

    void Start()
    {
        world = World.Default;

        var systemsGroup = world.CreateSystemsGroup();
        systemsGroup.AddSystem(new HealthSystem());

        world.AddSystemsGroup(order: 0, systemsGroup);
    }
}
