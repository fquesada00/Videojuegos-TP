using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MovementController))]
public class Character : Actor
{
    // COMANDS
    private CmdJump _cmdJump;
    private CmdDash _cmdDash;

    // INSTANCES
    private MovementController _movementController;
    private Camera mainCamera;
    private Vector3 targetDirection;

    // AUXILIAR
    private KeyCode _dashKeyCode = KeyCode.LeftControl;

    private void Start()
    {
        _movementController = GetComponent<MovementController>();
        _cmdJump = new CmdJump(_movementController);
        _cmdDash = new CmdDash(_movementController);
        mainCamera = Camera.main;
    }

    private void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg +
                                mainCamera.transform.eulerAngles.y;
            Vector3 targetDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            EventQueueManager.instance.AddCommand(new CmdMovement(_movementController, targetDirection));
            EventQueueManager.instance.AddCommand(new CmdRotation(_movementController, targetAngle));
        }

        if (Input.GetKeyDown(_dashKeyCode)) EventQueueManager.instance.AddCommand(_cmdDash);

        if (Input.GetButtonDown("Jump")) EventQueueManager.instance.AddCommand(_cmdJump);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground")) _movementController.ResetJumpsCounter();
    }

    private bool MovedForward() => Input.GetButtonDown("Vertical") && Input.GetAxis("Vertical") > 0;
}