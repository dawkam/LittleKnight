using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyView : MonoBehaviour
{

    protected NavMeshAgent _agent;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
    }
    public void FaceTarget(Vector3 _target)
    {
        Vector3 direction = (_target - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    public float GetStoppingDistance()
    {
        return _agent.stoppingDistance;
    }

    public bool GetIsStopped()
    {
        return _agent.isStopped;
    }

    public void SetIsStopped(bool value)
    {
        _agent.isStopped = value;
    }

    public void SetDestination(Vector3 value)
    {
        _agent.SetDestination(value);
    }
}
