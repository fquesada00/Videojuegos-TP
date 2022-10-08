using System.Collections;
using System.Collections.Generic;
using Commands.Animations;
using Controllers;
using UnityEngine;

[RequireComponent(typeof(MovementController))]
public class Character : Actor
{
    // COMANDS
    private CmdJump _cmdJump;
    private CmdDash _cmdDash;
    private CmdStartRunAnimation _cmdStartRunAnimation;
    private CmdStopRunAnimation _cmdStopRunAnimation;
    private CmdStartJumpAnimation _cmdStartJumpAnimation;
    private CmdStopJumpAnimation _cmdStopJumpAnimation;

    // INSTANCES
    private MovementController _movementController;
    private Camera _mainCamera;
    private AnimationController _animationController;

    private void Start()
    {
        _movementController = GetComponent<MovementController>();
        _animationController = GetComponentInChildren<AnimationController>();
        
        _cmdJump = new CmdJump(_movementController);
        _cmdDash = new CmdDash(_movementController);

        _cmdStartRunAnimation = new CmdStartRunAnimation(_animationController);
        _cmdStopRunAnimation = new CmdStopRunAnimation(_animationController);

        _cmdStartJumpAnimation = new CmdStartJumpAnimation(_animationController);
        _cmdStopJumpAnimation = new CmdStopJumpAnimation(_animationController);
        
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        
        

        bool isDashing = Input.GetButtonDown("Fire3");


        // Dash
        if (Input.GetButtonDown("Fire3")) EventQueueManager.instance.AddCommand(_cmdDash);

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
            EventQueueManager.instance.AddCommand(_cmdStartJumpAnimation);
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
        }
        else
        {
            if (_animationController.IsRunning)
            {
                EventQueueManager.instance.AddCommand(_cmdStopRunAnimation);
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground")) {
            _movementController.ResetJumpsCounter();
            EventQueueManager.instance.AddCommand(_cmdStopJumpAnimation);
        }
    }

    private bool MovedForward() => Input.GetButtonDown("Vertical") && Input.GetAxis("Vertical") > 0;
}