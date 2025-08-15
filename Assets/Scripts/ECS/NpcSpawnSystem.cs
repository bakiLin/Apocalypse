using Scellecs.Morpeh;
using Scellecs.Morpeh.Helpers;
using Unity.IL2CPP.CompilerServices;
using Random = System.Random;
using UnityEngine;
using Unity.Burst;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public sealed class NpcSpawnSystem : SimpleUpdateSystem<NpcSpawn>
{
    private float time;

    private int prevIndex, currentIndex;

    private Random random = new Random();
    [BurstCompile]
    protected override void Process(Entity entity, ref NpcSpawn npcSpawn, in float deltaTime)
    {
        if (!npcSpawn.spawn)
            return;

        if (time < 1f)
            time += deltaTime;
        else
        {
            do currentIndex = random.Next(0, 3);
            while (currentIndex == prevIndex);
            prevIndex = currentIndex;

            Spawn(ref npcSpawn, currentIndex.ToString(), npcSpawn.spawnPosition.position, npcSpawn.spawnRotation);

            time = 0f;
            npcSpawn.spawn = false;
        }
    }

    private void Spawn(ref NpcSpawn npcSpawn, string tag, Vector3 position, Vector3 rotation)
    {
        if (npcSpawn.poolDictionary.ContainsKey(tag))
        {
            GameObject obj = npcSpawn.poolDictionary[tag].Dequeue();
            obj.transform.position = position;
            obj.transform.rotation = Quaternion.Euler(rotation);
            obj.SetActive(true);

            npcSpawn.poolDictionary[tag].Enqueue(obj);
        }
    }
}