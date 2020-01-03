using System;
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

    private bool alive;

    public Collider weapon;
    public GameObject GameOverSreen;


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
        _playerView.SetSpeeds(_playerModel.walkSpeed, _playerModel.sprintSpeed, _playerModel.rotationSpeed);
        _healthBar = GetComponent<HealthBar>();
        _speed = _playerModel.sprintSpeed;
        alive = true;
        _healthBar.SetMaxProgress(_playerModel.expToNextLvl.ToString());
    }

    void FixedUpdate()
    {
        if (_playerModel.CurrentHealth > 0)
        {
            bool isAttacking = _playerAnimation.IsAttacking();
            //PerformJump();
            if (weapon != null)
                weapon.enabled = _playerAnimation.IsAttacking();

            //if (EventSystem.current.IsPointerOverGameObject())
            //    return;

            if (weapon != null && _playerView.PerformAttack1() )//&& Math.Abs(_playerVelocity.z) < 3 && Math.Abs(_playerVelocity.x) < 3)
            {
                
                weapon.enabled = true;
                _playerAnimation.PerformAttack1();
            }
            else if (weapon != null && _playerView.PerformAttack2() )//&& Math.Abs(_playerVelocity.z) < 3 && Math.Abs(_playerVelocity.x) < 3)
            {
                
                weapon.enabled = true;
                _playerAnimation.PerformAttack2();
            }

            _playerVelocity = _playerView.PerformMovement(ref _playerVelocityY, ref _direction,  ref _speed, isAttacking );

            if (!isAttacking)
            {
                if (Math.Abs(_playerVelocityY.y) < 0.3)
                    _playerAnimation.Walk(Math.Abs(_playerVelocity.x) + Math.Abs(_playerVelocity.z));
                else if (Math.Abs(_playerVelocity.z) < 1 && Math.Abs(_playerVelocity.x) < 1)
                    _playerAnimation.Walk(0);
            }
        }
        else if(alive)
        {
            alive = false;
            _playerAnimation.Die();
            
        }else if (_playerAnimation.DieAnimationEnd())
        {
            GameOverSreen.SetActive(true);
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
            Instantiate(_playerModel.LvlUpEffect, this.transform);
            _healthBar.SetLvl(_playerModel.level.ToString());
            _healthBar.SetSize(_playerModel.CurrentHealth / _playerModel.baseHealth);
            _healthBar.SetMaxProgress(_playerModel.expToNextLvl.ToString());
        }
        _healthBar.SetProgress(_playerModel.exp.ToString());
    }


}
