using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MovementController))]
public class Character : Actor
{
    // INSTANCES
    private MovementController _movementController;
    private Camera mainCamera;
    private Vector3 targetDirection;
    
    private void Start()
    {
        _movementController = GetComponent<MovementController>();
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

    }
}