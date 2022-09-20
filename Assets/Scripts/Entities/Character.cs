using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MovementController))]
public class Character : MonoBehaviour
{
    // MOVEMENT
    [SerializeField] private KeyCode _moveForward = KeyCode.W;
    [SerializeField] private KeyCode _moveBack = KeyCode.S;
    [SerializeField] private KeyCode _moveLeft = KeyCode.A;
    [SerializeField] private KeyCode _moveRight = KeyCode.D;

    // COMANDS
    private CmdMovement _cmdMoveForward;
    private CmdMovement _cmdMoveBack;
    private CmdMovement _cmdMoveRight;
    private CmdMovement _cmdMoveLeft;

    // INSTANCES
    private MovementController _movementController;
    private Camera mainCamera;
    private Vector3 targetDirection;
    
    private void Start()
    {
        _movementController = GetComponent<MovementController>();
        _cmdMoveForward = new CmdMovement(_movementController, Vector3.forward);
        _cmdMoveBack = new CmdMovement(_movementController, -Vector3.forward);
        _cmdMoveLeft = new CmdMovement(_movementController, Vector3.left);
        _cmdMoveRight = new CmdMovement(_movementController, Vector3.right);
	    mainCamera = Camera.main;
    }

    private void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        if(direction.magnitude >= 0.1f){
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + mainCamera.transform.eulerAngles.y;
            Vector3 targetDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward; 

            EventQueueManager.instance.AddCommand(new CmdMovement(_movementController, targetDirection));
            EventQueueManager.instance.AddCommand(new CmdRotation(_movementController, targetAngle));
        }

        // if (Input.GetKey(_moveForward)) EventQueueManager.instance.AddCommand(_cmdMoveForward);
        // if (Input.GetKey(_moveLeft)) EventQueueManager.instance.AddCommand(_cmdMoveLeft);
        // if (Input.GetKey(_moveBack)) EventQueueManager.instance.AddCommand(_cmdMoveBack);
        // if (Input.GetKey(_moveRight)) EventQueueManager.instance.AddCommand(_cmdMoveRight);

        // // Player rotation to camera direction
        // var input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        // var forward = mainCamera.transform.TransformDirection(Vector3.forward);
        // forward.y = 0;
        // //get the right-facing direction of the referenceTransform
        // var right = mainCamera.transform.TransformDirection(Vector3.right);
        // targetDirection = input.x * right + input.y * forward;
        // if (input != Vector2.zero && targetDirection.magnitude > 0.1f)
        //     EventQueueManager.instance.AddCommand(new CmdRotation(_movementController, targetDirection));
    }
}