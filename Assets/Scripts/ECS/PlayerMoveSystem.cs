using Scellecs.Morpeh;
using Scellecs.Morpeh.Helpers;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public sealed class PlayerMoveSystem : SimpleFixedUpdateSystem<PlayerMove>
{
    protected override void Process(Entity entity, ref PlayerMove player, in float deltaTime)
    {
        var direction = player.rigidbody.velocity;
        direction = new Vector3(player.joystick.Direction.x, 0f, player.joystick.Direction.y);
        direction.Normalize();
        player.rigidbody.velocity = direction * player.moveSpeed;
    }
}