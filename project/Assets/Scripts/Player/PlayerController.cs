﻿using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{

    private PlayerModel _playerModel;
    private PlayerView _playerView;
    private PlayerAnimation _playerAnimation;
    private HealthBar _healthBar;

    private float _speed;
    private Vector3 _direction;
    private Vector3 _playerVelocity;
    private Vector3 _playerVelocityY;

    public Collider weapon;


    #region Singleton
    public static PlayerController instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of PlayerController !");
        }

        instance = this;
    }

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        Animator animator = GetComponentInChildren<Animator>();//GetComponent<Animator>();
        _playerAnimation = new PlayerAnimation(animator);
        _playerModel = GetComponent<PlayerModel>();
        _playerView = GetComponent<PlayerView>();
        _healthBar = GetComponent<HealthBar>();
        _speed = _playerModel.sprintSpeed;

    }

    void FixedUpdate()
    {
        //PerformJump();
        if (weapon != null)
            weapon.enabled = _playerAnimation.IsAttacking();

        //if (EventSystem.current.IsPointerOverGameObject())
        //    return;

        if (weapon != null && _playerView.PerformAttack1() && Math.Abs(_playerVelocity.z)  < 1 && Math.Abs( _playerVelocity.x) < 1)
        {

            weapon.enabled = true;
            _playerAnimation.PerformAttack1();
        }
        else if (weapon != null && _playerView.PerformAttack2() && Math.Abs(_playerVelocity.z) < 1 && Math.Abs(_playerVelocity.x) < 1)
        {
            weapon.enabled = true;
            _playerAnimation.PerformAttack2();
        }
        else if(!_playerAnimation.IsAttacking())
        {
            _playerVelocity = _playerView.PerformMovement(ref _playerVelocityY, ref _direction, _playerModel.rotationSpeed, ref _speed, _playerModel.walkSpeed, _playerModel.sprintSpeed);

            if (Math.Abs(_playerVelocityY.y) < 0.3)
                _playerAnimation.Walk(Math.Abs(_playerVelocity.x) + Math.Abs(_playerVelocity.z));
            else if(Math.Abs(_playerVelocity.z) < 1 && Math.Abs(_playerVelocity.x) < 1 )
                _playerAnimation.Walk(0);
        }
    }

    public void ChangeCurrentArmor(Elements armorEq)
    {
        armorEq.Add(_playerModel.baseArmor);
        _playerModel.armor = armorEq;

    }

    public void ChangeCurrentDamage(Elements damageEq)
    {
        damageEq.Add(_playerModel.baseDamage);
        _playerModel.damage = damageEq;
    }

    public string[] GetStats()
    {
        return _playerModel.GetStats();

    }

    public Elements GetBaseDamage()
    {
        return _playerModel.baseDamage;
    }

    public void SetWeaponCollider(Collider collider)
    {
        weapon = collider;
        weapon.enabled = false;
    }
    public void RemoveWeaponCollider()
    {
        weapon = null;
    }

    public void TakeDamage(Elements damage)
    {
        _playerModel.TakeDamage(damage);
        _healthBar.SetSize(_playerModel.CurrentHealth/ _playerModel.baseHealth);

    }

    public void AddExp(float exp)
    {
        if (_playerModel.AddExp(exp))
        {
            Instantiate(_playerView.LvlUpEffect, this.transform);
            _healthBar.SetLvl(_playerModel.level.ToString());
            _healthBar.SetSize(_playerModel.CurrentHealth / _playerModel.baseHealth);
        }
    }
}
