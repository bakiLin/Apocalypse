using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public sealed class NpcSpawnInitializer : ISystem
{
    public World World { get; set; }

    private Filter filter;

    private Stash<NpcSpawn> stash;

    public void OnAwake()
    {
        filter = World.Default.Filter.With<NpcSpawn>().Build();
        stash = World.Default.GetStash<NpcSpawn>();

        foreach (var entity in filter)
        {
            ref var spawnComponent = ref stash.Get(entity);
            spawnComponent.poolDictionary = spawnComponent.pooler.GetDictionary();
        }
    }

    public void OnUpdate(float deltaTime)
    {

    }

    public void Dispose()
    {

    }
}