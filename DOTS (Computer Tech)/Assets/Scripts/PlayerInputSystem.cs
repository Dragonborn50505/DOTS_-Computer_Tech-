using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;
using UnityEngine.InputSystem;

[UpdateInGroup(typeof(InitializationSystemGroup), OrderLast = true)]

public partial class PlayerInputSystem : SystemBase
{
    private GameInputTest InputActions;
    private Entity Player;

    protected override void OnCreate()
    {
        RequireForUpdate<PlayerTag>();
        RequireForUpdate<PlayerMoveInput>();
        
        
        
        
        InputActions = new GameInputTest();

    }

    protected override void OnStartRunning()
    {
        InputActions.Enable();
        InputActions.GamePlay.Shoot.performed += OnShoot;
        Player = SystemAPI.GetSingletonEntity<PlayerTag>();
    }

    private void OnShoot(InputAction.CallbackContext context)
    {
        if (!SystemAPI.Exists(Player)) return;

        SystemAPI.SetComponentEnabled<FireProjectileTag>(Player, true);
    }

    protected override void OnUpdate()
    {
        Vector2 moveInput = InputActions.GamePlay.Move.ReadValue<Vector2>();
        SystemAPI.SetSingleton(new PlayerMoveInput { Value = moveInput });
        
        //Test
        //int rotateInput = InputActions.GamePlay.Rotate.ReadValue<int>();
        //SystemAPI.SetSingleton(new PlayerMoveInput { Value = rotateInput });
    }


    protected override void OnStopRunning() 
    { 
        InputActions.Disable();
        Player = Entity.Null;
    }
}
