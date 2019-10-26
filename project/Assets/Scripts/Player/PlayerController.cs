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

    public float speed;
    public float rotationSpeed;
    public float jumpForce;

    private Animator _animator;
    // Start is called before the first frame update

    Vector3 gravity;
    Vector3 raycastFloorPos;
    void Start()
    {
        _playerCamera = Camera.main;
        _animator = GetComponentInChildren<Animator>();//GetComponent<Animator>();
        _playerAnimation = new PlayerAnimation(_animator);
        PlayerBody = GetComponent<Rigidbody>();

    }

    void FixedUpdate()
    {
        PerformJump();
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

        //Animation
        _playerAnimation.Walk( Math.Abs(PlayerBody.velocity.x)+ Math.Abs(PlayerBody.velocity.y));
    }

    void PerformJump()
    {
        //if (FloorRaycasts(0, 0, 0.6f) == Vector3.zero)
        //{
        //    gravity += Vector3.up * Physics.gravity.y * Time.fixedDeltaTime;
        //}


        }

        //Vector3 FloorRaycasts(float offsetx, float offsetz, float raycastLength)
        //{
        //    RaycastHit hit;
        //    // move raycast
        //    raycastFloorPos = transform.TransformPoint(0 + offsetx, 0 + 0.5f, 0 + offsetz);

        //    Debug.DrawRay(raycastFloorPos, Vector3.down, Color.magenta);
        //    if (Physics.Raycast(raycastFloorPos, -Vector3.up, out hit, raycastLength))
        //    {
        //        return hit.point;
        //    }
        //    else return Vector3.zero;
        //}

    }
