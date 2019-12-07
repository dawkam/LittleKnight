using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{

    private PlayerModel _playerModel;
    private PlayerView _playerView;
    private PlayerAnimation _playerAnimation;

    private float _speed;
    private Vector3 _direction;
    private Vector3 _playerVelocity;
    private Vector3 _playerVelocityY;

    public Collider weapon;

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
        if (weapon != null)
            weapon.enabled = _playerAnimation.IsAttacking();

        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (weapon != null && _playerView.PerformAttack1())
        {
            weapon.enabled = true;
            _playerAnimation.PerformAttack1();
        }
        else if (weapon != null && _playerView.PerformAttack2())
        {
            weapon.enabled = true;
            _playerAnimation.PerformAttack2();
        }
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
        return _playerModel.damage;
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
}
