using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public partial struct SpanwerSystem : ISystem
{
    public void OnCreate(ref SystemState state) { }

    public void OnDestroy(ref SystemState state) { }

    public void OnUpdate(ref SystemState state) 
    {
        foreach(RefRW<Spawner> spawner in SystemAPI.Query<RefRW<Spawner>>())
        {
            if (spawner.ValueRO.NextSpawnTime < SystemAPI.Time.ElapsedTime)
            {
                Entity newEntity = state.EntityManager.Instantiate(spawner.ValueRO.Prefab);
                Entity Spawner = SystemAPI.GetSingletonEntity<SpawnerTag>();
                LocalTransform SpawnerTransform = state.EntityManager.GetComponentData<LocalTransform>(Spawner);

                float3 test = SpawnerTransform.Position + (SpawnerTransform.Up() * 5);
                float3 SpawnerLocation = test;
                float2 SpawnLoc= SpawnerLocation.xy;
                spawner.ValueRW.SpawnPosition = SpawnLoc;

                float3 pos = new float3(spawner.ValueRO.SpawnPosition.x, spawner.ValueRO.SpawnPosition.y, 0);
                state.EntityManager.SetComponentData(newEntity, LocalTransform.FromPosition(pos));
                spawner.ValueRW.NextSpawnTime = (float)SystemAPI.Time.ElapsedTime + spawner.ValueRO.SpawnRate;
            }
        }
    }
}