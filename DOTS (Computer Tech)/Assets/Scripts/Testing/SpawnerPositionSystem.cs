using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
using UnityEngine.Windows;

[UpdateBefore(typeof(TransformSystemGroup))]
public partial struct SpawnerPositionSystem : ISystem
{

    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<PlayerTag>();
    }


    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        float deltaTime = SystemAPI.Time.DeltaTime;

        Entity Player = SystemAPI.GetSingletonEntity<PlayerTag>();
        LocalTransform PlayerTransform = state.EntityManager.GetComponentData<LocalTransform>(Player);

        new SpawnerMoveJob
        {
            PlayerLocalTransform = PlayerTransform,
            DeltaTime = deltaTime,
        }.Schedule();
    }
}

[BurstCompile]
public partial struct SpawnerMoveJob : IJobEntity
{
    public float DeltaTime;
    public LocalTransform PlayerLocalTransform;

    [BurstCompile]
    private void Execute(ref LocalTransform transform, SpawnerRotationSpeedPosition speed)
    {
        transform.Position = PlayerLocalTransform.Position;
        quaternion rotation = transform.Rotation;
        rotation = math.mul(rotation, quaternion.AxisAngle(new float3(0, 0, 1), speed.Value * DeltaTime));
        transform.Rotation = rotation;
    }
}