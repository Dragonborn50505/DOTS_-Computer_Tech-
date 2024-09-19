using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

[UpdateBefore(typeof(TransformSystemGroup))]
public partial struct EnemyMoveSystem : ISystem
{

    public void OnCreate(ref SystemState state)
    {
       state.RequireForUpdate<PlayerTag>(); 
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        float deltaTime = SystemAPI.Time.DeltaTime;

        var Player = SystemAPI.GetSingletonEntity<PlayerTag>();
        LocalTransform PlayerTransform = state.EntityManager.GetComponentData<LocalTransform>(Player);

        new EnemyMoveJob
        {
            PlayerLocalTransform = PlayerTransform,
            DeltaTime = deltaTime,
        }.Schedule();
    }
}


[BurstCompile]
public partial struct EnemyMoveJob : IJobEntity
{
    public float DeltaTime;

    public LocalTransform PlayerLocalTransform;

    [BurstCompile]

    private void Execute(ref LocalTransform transform, EnemyMoveSpeed speed)
    {
        //float3 tagetPosition = PlayerLocalTransform.Position - transform.Position;
        Vector3 tagetPosition = PlayerLocalTransform.Position - transform.Position;
        tagetPosition.Normalize();
        float3 TargetPos = tagetPosition;

        transform.Position += TargetPos * speed.Value * DeltaTime;
    }
}
