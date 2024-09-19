using NUnit.Framework.Internal;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;


public partial struct SpanwerSystem : ISystem
{
    private float distanceFromEachOther;

    public void OnCreate(ref SystemState state) 
    {}

    public void OnDestroy(ref SystemState state) { }

    public void OnUpdate(ref SystemState state) 
    {
        foreach(RefRW<Spawner> spawner in SystemAPI.Query<RefRW<Spawner>>())
        {
            if (spawner.ValueRO.NextSpawnTime < SystemAPI.Time.ElapsedTime)
            {
                Entity Spawner = SystemAPI.GetSingletonEntity<SpawnerTag>();
                float SpawnNr = state.EntityManager.GetComponentData<EnemySpawnNr>(Spawner).Value;

                for (int i = 0; i < SpawnNr; i++) 
                {
                    Entity newEntity = state.EntityManager.Instantiate(spawner.ValueRO.Prefab);
                    LocalTransform SpawnerTransform = state.EntityManager.GetComponentData<LocalTransform>(Spawner);

                    distanceFromEachOther += i;
                    float distance = state.EntityManager.GetComponentData<DistanceFromPlayer>(Spawner).Value;
                    float3 SpawnerLocation = SpawnerTransform.Position + (SpawnerTransform.Up() * distance) + (SpawnerTransform.Right() * distanceFromEachOther);
                    float2 SpawnLoc = SpawnerLocation.xy;
                    spawner.ValueRW.SpawnPosition = SpawnLoc;


                    float3 pos = new float3(spawner.ValueRO.SpawnPosition.x, spawner.ValueRO.SpawnPosition.y, 0);
                    state.EntityManager.SetComponentData(newEntity, LocalTransform.FromPosition(pos));

                    distanceFromEachOther = 0;
                }


                spawner.ValueRW.NextSpawnTime = (float)SystemAPI.Time.ElapsedTime + spawner.ValueRO.SpawnRate;

                if (SpawnNr < 5) 
                {
                    new increaseNrOfEnemies{}.Schedule();
                }
            }
        }
    }
}


[BurstCompile]
public partial struct increaseNrOfEnemies : IJobEntity
{

    [BurstCompile]
    private void Execute(ref EnemySpawnNr nrOfEnemies)
    {
        nrOfEnemies.Value++;
    }
}