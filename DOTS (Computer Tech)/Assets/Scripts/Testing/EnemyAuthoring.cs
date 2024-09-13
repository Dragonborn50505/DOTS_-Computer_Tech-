using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class EnemyAuthoring : MonoBehaviour
{
    public float MoveSpeed;

    class EnemyAuthoringBaker : Baker<EnemyAuthoring>
    {
        public override void Bake(EnemyAuthoring authoring)
        {
            Entity enemyEntity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent<EnemyTag>(enemyEntity);

            AddComponent(enemyEntity, new EnemyMoveSpeed
            {
                Value = authoring.MoveSpeed,
            });

            AddComponent<EnemyRotateInput>(enemyEntity);
        }
    }
 }

public struct EnemyMoveSpeed : IComponentData
{
    public float Value;
}

public struct EnemyTag : IComponentData
{

}

public struct EnemyRotateInput : IComponentData
{
    public float Value;
}