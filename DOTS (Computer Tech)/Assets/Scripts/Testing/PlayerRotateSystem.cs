using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;



[UpdateBefore(typeof(TransformSystemGroup))]


public partial struct PlayerRotateSystem : ISystem
{


    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        float deltaTime = SystemAPI.Time.DeltaTime;

        new PlayerRotateJob
        {
            DeltaTime = deltaTime,
        }.Schedule();
    }
}

[BurstCompile]

public partial struct PlayerRotateJob : IJobEntity
{
    public float DeltaTime;
    private Vector3 mousePosition;
    private Vector2 rotationValue;


    [BurstCompile]

    private void Execute(ref LocalTransform transform)
    {
        //transform.Position.xy += input.Value * speed.Value * DeltaTime;

        mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        rotationValue.Value = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);
        transform.up = rotationValue.Value;

    }
}