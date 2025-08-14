using Scellecs.Morpeh;
using Scellecs.Morpeh.Providers;
using System;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[RequireComponent(typeof(Rigidbody2D))]
public sealed class PlayerProvider : MonoProvider<Player>
{
}

[Serializable]
public struct Player : IComponent
{
    public PlayerConfig playerConfig;
    public Rigidbody2D rigidbody;
}
