using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskChaseAndShoot : Node
{
    private EnemyBehaviour m_enemyBehaviour;

    public TaskChaseAndShoot(EnemyBehaviour behaviour)
    {
        m_enemyBehaviour = behaviour;
    }

    public override NodeState Evaluate()
    {
        Transform _body = m_enemyBehaviour.transform;
        Transform _target = (Transform)GetData("target");

        var _distanceToTarget = Vector2.Distance(_body.position, _target.position);
        if(_distanceToTarget < m_enemyBehaviour.chaseRange)
        {
            if (_distanceToTarget > m_enemyBehaviour.minRange && _distanceToTarget < m_enemyBehaviour.chaseRange)
            {
                _body.position = Vector2.MoveTowards(_body.position, _target.position, m_enemyBehaviour.moveSpeed * Time.deltaTime);
                _body.right = _target.position - _body.position;
            }
            else if (_distanceToTarget < m_enemyBehaviour.minRange)
            {
                _body.position = Vector2.MoveTowards(_body.position, -_target.position, m_enemyBehaviour.moveSpeed * Time.deltaTime);
                _body.right = _target.position - _body.position;
            }

            m_enemyBehaviour.enemyShoot.Shoot();
        }
        else
        {
            _state = NodeState.FAILURE;
            return _state;
        }



        _state = NodeState.RUNNING;
        return _state;
    }
}
