using System.Collections;
using System.Collections.Generic;
using Commands.Animations;
using Controllers;
using UnityEngine;

[RequireComponent(typeof(MovementController)), RequireComponent(typeof(WeaponController))]
public class Character : Actor
{
    // COMANDS
    private CmdJump _cmdJump;
    private CmdDash _cmdDash;
    private CmdStartRunAnimation _cmdStartRunAnimation;
    private CmdStopRunAnimation _cmdStopRunAnimation;
    private CmdJumpAnimation _cmdJumpAnimation;
    private CmdLandAnimation _cmdLandAnimation;
    private CmdAttack _cmdAttack;
    private CmdAttackAnimation _cmdAttackAnimation;
    // INSTANCES
    private Camera _mainCamera;
    private MovementController _movementController;
    private AnimationController _animationController;
    private WeaponController _weaponController;

    private void Start()
    {
        _movementController = GetComponent<MovementController>();
        _animationController = GetComponentInChildren<AnimationController>();
        _weaponController = GetComponent<WeaponController>();

        _cmdJump = new CmdJump(_movementController);
        _cmdDash = new CmdDash(_movementController);

        _cmdStartRunAnimation = new CmdStartRunAnimation(_animationController);
        _cmdStopRunAnimation = new CmdStopRunAnimation(_animationController);

        _cmdJumpAnimation = new CmdJumpAnimation(_animationController);
        _cmdLandAnimation = new CmdLandAnimation(_animationController);
        
        _cmdAttack = new CmdAttack(_weaponController);
        // _cmdAttackAnimation = new CmdAttackAnimation(_animationController);
        
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        

        // Dash
        bool isDashing = Input.GetButtonDown("Fire3");
        if (isDashing) EventQueueManager.instance.AddCommand(_cmdDash);

        // Jump
        bool isJumping = Input.GetButtonDown("Jump");
        if (isJumping)
        {
            if(_animationController.IsRunning) {
                // shutdown every animation immediately
                _animationController.StopAllAnimations();
                // EventQueueManager.instance.AddCommand(_cmdStopRunAnimation);
            } else if(_animationController.IsJumping) {
                _animationController.Rebind();
            }
                
            EventQueueManager.instance.AddCommand(_cmdJump);
            EventQueueManager.instance.AddCommand(_cmdJumpAnimation);
            return;
        } 

        bool isMoving = direction.magnitude == 1f;
        if (isMoving)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg +
                                _mainCamera.transform.eulerAngles.y;

            Vector3 targetDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            //Movement
            EventQueueManager.instance.AddCommand(new CmdMovement(_movementController, targetDirection));
            EventQueueManager.instance.AddCommand(new CmdRotation(_movementController, targetAngle));

            //Animation
            if(_animationController.IsIdle)
                EventQueueManager.instance.AddCommand(_cmdStartRunAnimation);
            return;
        }
        else
        {
            if (_animationController.IsRunning)
            {
                EventQueueManager.instance.AddCommand(_cmdStopRunAnimation);
            }
        }

        //Attack
        bool isAttacking = Input.GetButtonDown("Fire1");
        if (isAttacking)
        {
            //TODO: add attack Command
           _animationController.Attack();
           EventQueueManager.instance.AddCommand(_cmdAttack);
        //    EventQueueManager.instance.AddCommand(new CmdAttackAnimation(_animationController, _weaponController.CurrentWeapon));
           return;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground")) {
            _movementController.ResetJumpsCounter();
            EventQueueManager.instance.AddCommand(_cmdLandAnimation);
        }
    }

    private bool MovedForward() => Input.GetButtonDown("Vertical") && Input.GetAxis("Vertical") > 0;
}