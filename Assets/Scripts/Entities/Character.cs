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
    private void Start()
    {
        _movementController = GetComponent<MovementController>();
        _cmdMoveForward = new CmdMovement(_movementController, Vector3.forward);
        _cmdMoveBack = new CmdMovement(_movementController, -Vector3.forward);
        _cmdMoveLeft = new CmdMovement(_movementController, Vector3.left);
        _cmdMoveRight = new CmdMovement(_movementController, Vector3.right);
    }

    private void Update()
    {
        if (Input.GetKey(_moveForward)) EventQueueManager.instance.AddCommand(_cmdMoveForward);
        if (Input.GetKey(_moveBack)) EventQueueManager.instance.AddCommand(_cmdMoveBack);
        if (Input.GetKey(_moveRight)) EventQueueManager.instance.AddCommand(_cmdMoveRight);
        if (Input.GetKey(_moveLeft)) EventQueueManager.instance.AddCommand(_cmdMoveLeft);
    }
}