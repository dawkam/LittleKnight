using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Rigidbody PlayerBody;
    private Vector3 _direction;
    private Quaternion _startingRotation;

    private Camera _playerCamera;

    private PlayerAnimation _playerAnimation;

    public float sprintSpeed;
    public float walkSpeed;
    public float rotationSpeed;
    public float jumpForce;

    public KeyCode walkKey;

    private float speed;

    private Animator _animator;
    // Start is called before the first frame update

    private Vector3 _playerVelocityY;
    private float _distToGround;
    private Collider _collider;

    void Start()
    {
        _playerCamera = Camera.main;
        _animator = GetComponentInChildren<Animator>();//GetComponent<Animator>();
        _playerAnimation = new PlayerAnimation(_animator);
        PlayerBody = GetComponent<Rigidbody>();

        speed = sprintSpeed;

        _collider= GetComponent<Collider>();
    }

    void FixedUpdate()
    {
        //PerformJump();
        PerformMovement();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    void PerformMovement()
    {

        if (!IsGrounded() || _playerVelocityY.y > 0)
        {
            _playerVelocityY += Vector3.up * Physics.gravity.y * Time.fixedDeltaTime;
            //_playerAnimation.Jump(_playerVelocityY.y);
        }
        else
        {
            _playerVelocityY.y = 0;
            //_playerAnimation.Jump(_playerVelocityY.y);
        }
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

        if (Input.GetKeyDown(walkKey))
        { 
            if (speed == sprintSpeed)
                speed = walkSpeed;
            else if (speed == walkSpeed)
                speed = sprintSpeed;
        }
            PlayerBody.velocity = (_direction * speed * inputAmount) + _playerVelocityY;
        // Debug.Log(PlayerBody.velocity);
        //Animation
        if (_playerVelocityY.y==0)
            _playerAnimation.Walk(Math.Abs(PlayerBody.velocity.x) + Math.Abs(PlayerBody.velocity.z));
        else
            _playerAnimation.Walk(0);

    }

    //void PerformJump()
    //{
    //    Debug.Log(Input.GetAxis("Jump"));
    //    if (Input.GetAxis("Jump") != 0 && IsGrounded())
    //    {
    //       if(_playerAnimation.Jump(_playerVelocityY.y + jumpForce))
    //            _playerVelocityY += Vector3.up * jumpForce;
    //        //_playerAnimation.Jump(_playerVelocityY.y);
    //    }


    //}

    Boolean IsGrounded()
    {
        Debug.DrawRay(_collider.bounds.center - (Vector3.up * _collider.bounds.extents.y), -Vector3.up* 0.5f , Color.magenta);
        return Physics.Raycast(_collider.bounds.center-(Vector3.up* _collider.bounds.extents.y), -Vector3.up,  0.5f);
    }


}
