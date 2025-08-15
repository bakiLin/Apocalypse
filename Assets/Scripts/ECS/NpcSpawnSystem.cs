using Scellecs.Morpeh;
using Scellecs.Morpeh.Helpers;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public sealed class NpcSpawnSystem : SimpleUpdateSystem<NpcSpawn>
{
    protected override void Process(Entity entity, ref NpcSpawn npcSpawn, in float deltaTime)
    {
        throw new System.NotImplementedException();
    }
}