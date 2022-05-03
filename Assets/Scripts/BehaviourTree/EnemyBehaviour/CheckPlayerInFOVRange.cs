using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPlayerInFOVRange : Node
{
    private static int m_playerLayerMask = 1 << 6;
    private EnemyBehaviour m_enemyBehaviour;

    public CheckPlayerInFOVRange(EnemyBehaviour behaviour)
    {
        m_enemyBehaviour = behaviour;
    }

    public override NodeState Evaluate()
    {
        object t = GetData("target");
        if (t == null)
        {
            Collider2D[] _colliders = Physics2D.OverlapCircleAll(m_enemyBehaviour.transform.position, m_enemyBehaviour.fovRange, m_playerLayerMask);
            if (_colliders.Length > 0)
            {
                parent.parent.SetData("target", _colliders[0].transform);

                _state = NodeState.SUCCESS;
                return _state;
            }

            _state = NodeState.FAILURE;
            return _state;
        }

        _state = NodeState.SUCCESS;
        return _state;
    }

}
