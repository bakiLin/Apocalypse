using Scellecs.Morpeh;
using Scellecs.Morpeh.Helpers;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public sealed class PlayerRotateSystem : SimpleUpdateSystem<PlayerRotate>
{
    protected override void Process(Entity entity, ref PlayerRotate player, in float deltaTime)
    {
        var rotation = player.rotation * deltaTime * player.rotateSpeed;

        var playerRotation = player.playerTransform.rotation.eulerAngles;
        playerRotation.y += rotation.x;
        player.playerTransform.rotation = Quaternion.Euler(playerRotation);

        var pivotRotation = player.pivotTransform.rotation.eulerAngles;
        if (pivotRotation.x > player.limitAngle && pivotRotation.x < 180f && rotation.y < 0f 
            || pivotRotation.x < (360f - player.limitAngle) && pivotRotation.x > 180f && rotation.y > 0f) 
            rotation.y = 0f;
        pivotRotation.x -= rotation.y;
        player.pivotTransform.rotation = Quaternion.Euler(pivotRotation);
    }
}
