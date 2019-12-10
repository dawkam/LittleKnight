using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    //protected Animator _anim;
    // protected NavMeshAgent _agent;
    protected Transform _target;
    protected EnemyModel _enemyModel;
    protected EnemyView _enemyView;
    protected EnemyAnimation _enemyAnimation;
    protected QuestLogController _questLogController;
    protected bool alive;
    protected HealthBar _healthBar;
    protected PlayerController _player;

    protected virtual void Start()
    {
        _target = GameObject.FindGameObjectWithTag("Player").transform;
        _questLogController = QuestLogController.instance;
        alive = true;
        _healthBar = GetComponent<HealthBar>();
        _player = PlayerController.instance;

    }

    protected virtual void Update()
    {
        RunUpdate();
        _enemyModel.DecrementAttackCoolDown();
    }

    public void RunUpdate()
    {
        float distance = Vector3.Distance(_target.position, transform.position);
        if (_enemyModel.CurrentHealth > 0) //(!_anim.GetBool("isDie"))
        {
            if (distance <= _enemyView.GetStoppingDistance())
            {
                _enemyView.FaceTarget(_target.position);
                _enemyView.SetIsStopped(true);
                if (_enemyAnimation.AttackAnimation())
                {
                    _enemyModel.Fire(_target.position + (Vector3.up * 2f));
                }
                //_enemyAnimation.AttackAnimation(ref _anim, _enemyModel.firePoint, _target, );

            }
            else if (distance <= _enemyModel.lookRadius)
            {
                _enemyView.SetDestination(_target.position);
                _enemyView.SetIsStopped(false);
                _enemyAnimation.WalkAnimation();
            }
            else if (_enemyAnimation.IsIdleAniamtion())
            {
                _enemyView.SetIsStopped(true);
                _enemyAnimation.IdleAnimation();
            }
        }
        else if (_enemyAnimation.DieAnimationEnd())
        {
            GameObject loot = Resources.Load("Prefabs/Loot/Loot", typeof(GameObject)) as GameObject;
            Instantiate(loot, this.transform.position, this.transform.rotation, null);
            Destroy(gameObject);

        }
        else if (alive)
        {
            Die();
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (_enemyView != null && _enemyModel)
        {
            Vector3 armaturePosition;
            Transform armature = transform.Find("Armature");
            if (armature == null)
                armaturePosition = transform.GetChild(0).Find("Armature").position;
            else
                armaturePosition = armature.position;
            Vector3 center = new Vector3(armaturePosition.x, 0.0f, armaturePosition.z);
            Gizmos.color = Color.red;

            Gizmos.DrawWireSphere(center, _enemyModel.lookRadius);
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(center, _enemyView.GetStoppingDistance());
        }
    }

    public void TakeDamage(Elements damage)
    {
        _enemyModel.TakeDamage(damage);
        _healthBar.SetSize(_enemyModel.CurrentHealth / _enemyModel.baseHealth);
    }

    public virtual void Die()
    {
        alive = false;
        _enemyAnimation.DieAnimation();
        _questLogController.CheckGoal(_enemyModel.characterName);
        _player.AddExp(_enemyModel.exp);
        //Debug.Log(transform.name + " died");
        //Destroy(gameObject);
    }
}
