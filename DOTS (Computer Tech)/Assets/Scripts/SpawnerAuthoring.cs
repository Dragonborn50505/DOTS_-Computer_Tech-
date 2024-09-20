using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public class SpawnerAuthoring : MonoBehaviour
{
    public GameObject Prefab;
    public float SpawnRate = 1;
    public float RotationSpeed;
    public float SpawnerDistance;
    public float SpawnerStartingCount;


    private float2 SpawnerPos;

    class SpawnBaker : Baker<SpawnerAuthoring>
    {
        public override void Bake(SpawnerAuthoring authoring)
        {
            Entity spawnerEntity = GetEntity(TransformUsageFlags.Dynamic);

            AddComponent<SpawnerTag>(spawnerEntity);

            AddComponent(spawnerEntity, new DistanceFromPlayer
            {
                Value = authoring.SpawnerDistance,
            });

            AddComponent(spawnerEntity, new EnemySpawnNr
            {
                Value = authoring.SpawnerStartingCount,
            });



            AddComponent(spawnerEntity, new Spawner
            {
                Prefab = GetEntity(authoring.Prefab, TransformUsageFlags.Dynamic),
                //SpawnPosition = float2.zero,
                SpawnPosition = authoring.SpawnerPos,
                NextSpawnTime = 0,
                SpawnRate = authoring.SpawnRate
            });



            AddComponent(spawnerEntity, new SpawnerRotationSpeedPosition
            {
                Value = authoring.RotationSpeed,
            });
        }
    }
}


public struct SpawnerRotationSpeedPosition : IComponentData
{
    public float Value;
}


public struct SpawnerTag : IComponentData
{

}

public struct SpawnedPos : IComponentData
{
    public float2 Value;
}

public struct DistanceFromPlayer : IComponentData
{
    public float Value;
}

public struct EnemySpawnNr : IComponentData
{
    public float Value;
}