using Scellecs.Morpeh;
using UnityEngine;

public class Init : MonoBehaviour
{
    private World world;

    private void Start()
    {
        world = World.Default;

        var systemsGroup = world.CreateSystemsGroup();
        systemsGroup.AddSystem(ScriptableObject.CreateInstance<PlayerMoveSystem>());
        systemsGroup.AddSystem(ScriptableObject.CreateInstance<PlayerRotateSystem>());

        world.AddSystemsGroup(order: 0, systemsGroup);
    }
}
