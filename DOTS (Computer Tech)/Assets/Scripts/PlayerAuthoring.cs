using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class PlayerAuthoring : MonoBehaviour
{
    public float MoveSpeed;
    public float ProjectileLifeTime;

    public GameObject ProjectilePrefab;



    class PlayerAuthoringBaker : Baker<PlayerAuthoring>
    {
        public override void Bake(PlayerAuthoring authoring)
        {
            Entity playerEntity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent<PlayerTag>(playerEntity);
            AddComponent<PlayerMoveInput>(playerEntity);

            AddComponent(playerEntity, new PlayerMoveSpeed
            {
                Value = authoring.MoveSpeed,
            });

            AddComponent<FireProjectileTag>(playerEntity);
            SetComponentEnabled<FireProjectileTag>(playerEntity, false);

            AddComponent(playerEntity, new ProjectilePrefab
            {
                Value = GetEntity(authoring.ProjectilePrefab, TransformUsageFlags.Dynamic),
            });
            AddComponent(playerEntity, new ProjectileLifeTime
            {
                Value = authoring.ProjectileLifeTime
            });
            
            
            
            //Test
            AddComponent<PlayerRotateInput>(playerEntity);
        }
    }
}


public struct PlayerMoveInput : IComponentData
{
    public float2 Value;
}

public struct PlayerMoveSpeed : IComponentData
{
    public float Value;
}

public struct PlayerTag: IComponentData
{

}

public struct ProjectilePrefab : IComponentData
{
    public Entity Value;
}

public struct ProjectileMoveSpeed : IComponentData
{
    public float Value;
}

public struct ProjectileLifeTime : IComponentData
{
    public float Value;
}

public struct LifeTime : IComponentData
{
    public float Value;
}

public struct IsDestroying : IComponentData
{
    
}

public struct FireProjectileTag : IComponentData, IEnableableComponent
{

}



//Test 
public struct PlayerRotateInput : IComponentData
{
    public float Value;
}