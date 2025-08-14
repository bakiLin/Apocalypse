using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public sealed class PlayerMovementInitSystem : IInitializer 
{
    public World World { get; set; }

    private Filter _playerWithoutMovement;

    public void OnAwake() 
    {
        _playerWithoutMovement = World.Filter.With<Player>().Without<MoveDirection>().Build();
        foreach (var entity in _playerWithoutMovement)
            entity.AddComponent<MoveDirection>();
    }

    public void Dispose()
    {

    }
}