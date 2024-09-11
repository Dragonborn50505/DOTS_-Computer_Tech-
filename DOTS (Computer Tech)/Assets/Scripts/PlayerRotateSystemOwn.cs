using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

[UpdateBefore(typeof(TransformSystemGroup))]
public partial struct PlayerRotateSystemOwn : ISystem
{
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        float deltaTime = SystemAPI.Time.DeltaTime;
        new PlayerRotateJob2
        {
            DeltaTime = deltaTime,
            
        }.Schedule();
    }
}

[BurstCompile]

public partial struct PlayerRotateJob2 : IJobEntity
{
    public float DeltaTime;
    
    private Vector3 mousePosition;
    private Vector2 rotationValue;
    

    [BurstCompile]

    private void Execute(ref LocalTransform transform, in PlayerRotateInput input, in PlayerMoveInput input2)
    {
        //transform.Position.xy += input.Value * speed.Value * DeltaTime;
        /*
        mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        rotationValue = new Vector2(mousePosition.x - transform.Position.x, mousePosition.y - transform.Position.y);
        */
        
        float rotation = input2.Value.x * DeltaTime;
        
    }
}
