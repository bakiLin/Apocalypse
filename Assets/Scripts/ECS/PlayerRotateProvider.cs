using Scellecs.Morpeh;
using Scellecs.Morpeh.Providers;
using System;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public sealed class PlayerRotateProvider : MonoProvider<PlayerRotate> 
{
    
}

[Serializable]
[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public struct PlayerRotate : IComponent
{
    [HideInInspector]
    public Vector2 rotation;

    public float rotateSpeed;

    public float limitAngle;

    public Transform playerTransform;

    public Transform pivotTransform;
}