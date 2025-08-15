using Scellecs.Morpeh;
using Scellecs.Morpeh.Providers;
using System;
using System.Collections.Generic;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public sealed class NpcSpawnProvider : MonoProvider<NpcSpawn> 
{
    
}

[Serializable]
[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public struct NpcSpawn : IComponent
{
    public bool spawn;

    public Transform spawnPosition;

    public Vector3 spawnRotation;

    public Dictionary<string, Queue<GameObject>> poolDictionary;

    public Pooler pooler;
}