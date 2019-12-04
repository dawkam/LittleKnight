using UnityEngine;
using UnityEngine.AI;

public class GhostController : EnemyController
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        _enemyAnimation = GetComponent<GhostAnimation>();
        _enemyModel = GetComponent<EnemyModel>();
        _enemyView = GetComponent<EnemyView>();
        if(_enemyModel != null)
        _enemyModel.characterName = "Ghost";
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

    }



    //public override void Die()
    //{
    //    _enemyAnimation.DieAnimation(ref _anim);
    //    Debug.Log(transform.name + " died");
    //    // Destroy(gameObject);
    //}
}

