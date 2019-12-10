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
        _healthBar.SetName(_enemyModel.characterName);
    }

}

