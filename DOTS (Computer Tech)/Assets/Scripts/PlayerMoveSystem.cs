using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

[UpdateBefore(typeof(TransformSystemGroup))]

public partial struct PlayerMoveSystem : ISystem
{
    [BurstCompile]

    public void OnUpdate(ref SystemState state)
    {
        float deltaTime = SystemAPI.Time.DeltaTime;

        new PlayerMoveJob
        {
            DeltaTime = deltaTime,
        }.Schedule();
    }
}

[BurstCompile]

public partial struct PlayerMoveJob : IJobEntity
{
    public float DeltaTime;

    [BurstCompile]

    private void Execute(ref LocalTransform transform, in PlayerMoveInput input, PlayerMoveSpeed speed)
    {
        transform.Position.xy += input.Value * speed.Value * DeltaTime;
        
        // float rotation = transform.Rotation.value.z * input.Value.x * Mathf.Deg2Rad;
        // quaternion quan = quaternion.Euler(0, 0, rotation);
        // transform.Rotation = quan;

        //if (input.Value.x == 0 && input.Value.y == 0) return;
        
        Vector3 direction = new Vector3(input.Value.x, input.Value.y, 0);
        float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
        angle -= 90;
        Quaternion directionalRotation = Quaternion.Euler(0, 0, angle);
        transform.Rotation = directionalRotation;
    }
}
