using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Rigidbody PlayerBody;
    private Vector3 _direction;
    private Quaternion _startingRotation;
    private bool _rotatingToLeft;
    private bool _rotatingToRight;

    private Camera _playerCamera;

    public float speed;
    public float rotationSpeed;
    // Start is called before the first frame update
    void Start()
    {
        _playerCamera = Camera.main;
        PlayerBody = GetComponent<Rigidbody>();

    }

    void FixedUpdate()
    {
        PerformMovement();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    void PerformMovement()
    {
        // reset movement
        _direction = Vector3.zero;

        // get vertical and horizontal movement input (controller and WASD/ Arrow Keys)
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        // base movement on camera
        Vector3 combinedInput = vertical * _playerCamera.transform.forward + horizontal * _playerCamera.transform.right;

        // normalize so diagonal movement isnt twice as fast
        _direction = new Vector3(combinedInput.normalized.x, 0, combinedInput.normalized.z);

        // make sure the input doesnt go negative or above 1;
        float inputAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));


        // rotate player to movement direction
        Quaternion rot = Quaternion.LookRotation(_direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, rot, Time.fixedDeltaTime * inputAmount * rotationSpeed);

          PlayerBody.velocity = (_direction * speed* inputAmount);

        //_direction = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));


        //// The step size is equal to speed times frame time-> angularSpeed * Time.deltaTime
        //Vector3 newDir = Vector3.RotateTowards(transform.forward, _direction, rotationSpeed * Time.deltaTime, 0.0f);
        //Debug.DrawRay(transform.position, newDir, Color.red);//?

        //// Move our position a step closer to the target.
        //transform.rotation = Quaternion.LookRotation(newDir);

        //transform.Translate(_direction * speed * Time.deltaTime);//,Space.World);



    }

}
