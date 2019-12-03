using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    
    private PlayerModel _playerModel;
    private PlayerView _playerView;

    private float _speed;
    private Vector3 _direction;
    private Vector3 _playerVelocity;
    private Vector3 _playerVelocityY;

    private PlayerAnimation _playerAnimation;

    // Start is called before the first frame update
    void Start()
    {
         Animator animator = GetComponentInChildren<Animator>();//GetComponent<Animator>();
        _playerAnimation = new PlayerAnimation(animator);
        _playerModel = GetComponent<PlayerModel>();
        _playerView = GetComponent<PlayerView>();
        _speed = _playerModel.sprintSpeed;
        
    }

    void FixedUpdate()
    {
        //PerformJump();
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (_playerView.PerformAttack1())
            _playerAnimation.PerformAttack1();
        else if (_playerView.PerformAttack2())
            _playerAnimation.PerformAttack2();
        else
        {
            _playerVelocity = _playerView.PerformMovement(ref _playerVelocityY, ref _direction, _playerModel.rotationSpeed, ref _speed, _playerModel.walkSpeed, _playerModel.sprintSpeed);

            if (_playerVelocityY.y < 0.3 && _playerVelocityY.y > -0.3)
                _playerAnimation.Walk(Math.Abs(_playerVelocity.x) + Math.Abs(_playerVelocity.z));
            else
                _playerAnimation.Walk(0);
        }
    }
    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeCurrentArmor(DamageType damageType, double armor)
    {
        switch (damageType)
        {
            case DamageType.Physical:
                _playerModel.CurrentPhysicalArmor = armor + _playerModel.basePhysicalArmor ;
                break;
            case DamageType.Air:
                _playerModel.CurrentAirArmor = armor + _playerModel.baseAirArmor;
                break;
            case DamageType.Water:
                _playerModel.CurrentWaterArmor = armor + _playerModel.baseWaterArmor;
                break;
            case DamageType.Fire:
                _playerModel.CurrentFireArmor = armor + _playerModel.baseFireArmor;
                break;
            case DamageType.Earth:
                _playerModel.CurrentEarthArmor += armor + _playerModel.baseEarthArmor;
                break;
        }
    }

    public void ChangeCurrentDamage(DamageType damageType, double damage)
    {
        switch (damageType)
        {
            case DamageType.Physical:
                _playerModel.CurrentPhysicalDamage = damage + _playerModel.basePhysicalDamage;
                break;
            case DamageType.Air:
                _playerModel.CurrentAirDamage = damage + _playerModel.baseAirDamage;
                break;
            case DamageType.Water:
                _playerModel.CurrentWaterDamage = damage + _playerModel.baseWaterDamage;
                break;
            case DamageType.Fire:
                _playerModel.CurrentFireDamage = damage + _playerModel.baseFireDamage;
                break;
            case DamageType.Earth:
                _playerModel.CurrentEarthDamage += damage + _playerModel.baseEarthDamage;
                break;
        }
    }

    public string[] GetStats()
    {
        return _playerModel.GetStats();

    }
}
