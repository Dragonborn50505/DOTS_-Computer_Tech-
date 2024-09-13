using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

//PlayerRotateSystem



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
    /*
    private Vector3 mousePosition;
    private Vector2 rotationValue;
    */

    [BurstCompile]

    private void Execute(ref LocalTransform transform, in PlayerRotateInput input)
    {
        //transform.Position.xy += input.Value * speed.Value * DeltaTime;
        /*
        mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        rotationValue = new Vector2(mousePosition.x - transform.Position.x, mousePosition.y - transform.Position.y);
        */

        //float rotation = input.Value * DeltaTime;
        //transform.RotateZ(rotation);
        //Debug.Log("RotateZ");

        quaternion rotation = transform.Rotation;
        rotation = math.mul(rotation, quaternion.AxisAngle(new float3(0, 0, 1), input.Value * 5 * DeltaTime));
        transform.Rotation = rotation;

    }
}