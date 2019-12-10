using System;
using UnityEngine;

public class PlayerView : MonoBehaviour
{

    private Rigidbody _playerRigidBody;
    private Collider _collider;

    private Camera _playerCamera;
    public GameObject LvlUpEffect;
    public void Awake()
    {
        _playerCamera = Camera.main;
        _playerRigidBody = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
    }
    public Vector3 PerformMovement(ref  Vector3 playerVelocity, ref Vector3 direction, float rotationSpeed, ref float speed, float walkSpeed, float sprintSpeed)
    {                                               //y
                                //y
        if (!IsGrounded() || playerVelocity.y > 0)
        {       //y
            playerVelocity += Vector3.up * Physics.gravity.y * Time.fixedDeltaTime;
            //_playerAnimation.Jump(_playerVelocityY.y);
        }
        else
        {   //y
            playerVelocity.y = 0;
            //_playerAnimation.Jump(_playerVelocityY.y);
        }
        // reset movement
        direction = Vector3.zero;

        // get vertical and horizontal movement input (controller and WASD/ Arrow Keys)
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        // base movement on camera
        Vector3 combinedInput = vertical * _playerCamera.transform.forward + horizontal * _playerCamera.transform.right;

        // normalize so diagonal movement isnt twice as fast
        direction = new Vector3(combinedInput.normalized.x, 0, combinedInput.normalized.z);

        // make sure the input doesnt go negative or above 1;
        float inputAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));


        // rotate player to movement direction
        if (direction != Vector3.zero)
        {
            Quaternion rot = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, rot, Time.fixedDeltaTime * inputAmount * rotationSpeed);
        }
        if (Input.GetButtonDown("Walk/Run"))
        {
            if (speed == sprintSpeed)
                speed = walkSpeed;
            else if (speed == walkSpeed)
                speed = sprintSpeed;
        }                                                                       //y
        _playerRigidBody.velocity = (direction * speed * inputAmount) + playerVelocity;

        // Debug.Log(PlayerBody.velocity);
        //Animation
        return _playerRigidBody.velocity;
    }

    private Boolean IsGrounded()
    {
        float maxDistance = 0.5f;

        Debug.DrawRay(_collider.bounds.center - (Vector3.up * _collider.bounds.extents.y), -Vector3.up * maxDistance, Color.magenta);
        if (Physics.Raycast(_collider.bounds.center - (Vector3.up * _collider.bounds.extents.y)
            , -Vector3.up, out RaycastHit hit,
            maxDistance))
        {
            if (hit.distance < maxDistance)
                _playerRigidBody.MovePosition(new Vector3(_playerRigidBody.position.x,
                   _playerRigidBody.position.y + (maxDistance - hit.distance)
                    , _playerRigidBody.position.z));

            return true;
        }
        else return false;
    }

    public bool PerformAttack1()
    {
        return Input.GetButtonDown("Fire1") && IsGrounded() ;
    }

    public bool PerformAttack2()
    {
        return Input.GetButtonDown("Fire2") && IsGrounded();
    }


}
