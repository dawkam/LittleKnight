using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    private PlayerModel _playerModel;
    private PlayerView _playerView;

    private float _speed;
    private Vector3 _direction;
    private Vector3 _playerVelocity;
    private Vector3 _playerVelocityY;

    private PlayerAnimation _playerAnimation;
    private Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponentInChildren<Animator>();//GetComponent<Animator>();
        _playerAnimation = new PlayerAnimation(_animator);
        _playerModel = GetComponent<PlayerModel>();
        _playerView= GetComponent<PlayerView>();
        _speed = _playerModel.sprintSpeed;
        
    }

    void FixedUpdate()
    {
        //PerformJump();
        _playerVelocity= _playerView.PerformMovement(ref _playerVelocityY,ref _direction, _playerModel.rotationSpeed, ref _speed, _playerModel.walkSpeed, _playerModel.sprintSpeed);

        if (_playerVelocityY.y == 0)
            _playerAnimation.Walk(Math.Abs(_playerVelocity.x) + Math.Abs(_playerVelocity.z));
        else
            _playerAnimation.Walk(0);
    }
    // Update is called once per frame
    void Update()
    {

    }

       
}
