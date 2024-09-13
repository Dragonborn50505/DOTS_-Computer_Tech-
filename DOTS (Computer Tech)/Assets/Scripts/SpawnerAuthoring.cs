using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public class SpawnerAuthoring : MonoBehaviour
{
    public GameObject Prefab;
    public float SpawnRate;
    public float RotationSpeed;
    public float2 SpawnerPos;

    class SpawnBaker : Baker<SpawnerAuthoring>
    {
        public override void Bake(SpawnerAuthoring authoring)
        {
            Entity spawnerEntity = GetEntity(TransformUsageFlags.Dynamic);

            AddComponent<SpawnerTag>(spawnerEntity);



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