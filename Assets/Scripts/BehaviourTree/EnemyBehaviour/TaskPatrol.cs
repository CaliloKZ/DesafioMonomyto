using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskPatrol : Node
{
    private EnemyBehaviour m_enemyBehaviour;

    private int m_currentWaypointIndex = 0;

    private float m_waitTime = 1f;
    private float m_waitCounter = 0f;
    private bool m_waiting = false;

    public TaskPatrol(EnemyBehaviour behaviour)
    {
        m_enemyBehaviour = behaviour;
    }

    public override NodeState Evaluate()
    {
        var _transform = m_enemyBehaviour.transform;
        var _waypoints = m_enemyBehaviour.waypoints;

        if (m_waiting)
        {
            m_waitCounter += Time.deltaTime;
            if(m_waitCounter >= m_waitTime)
            {
                m_waiting = false;
            }
        }
        else
        {
            Transform _wp = _waypoints[m_currentWaypointIndex];
            if(Vector2.Distance(_transform.position, _wp.position) < 0.01f)
            {
                _transform.position = _wp.position;
                m_waitCounter = 0f;
                m_waiting = true;

                m_currentWaypointIndex = (m_currentWaypointIndex + 1) % _waypoints.Length;
            }
            else
            {
                _transform.position = Vector2.MoveTowards(_transform.position, _wp.position, m_enemyBehaviour.moveSpeed * Time.deltaTime);
                _transform.right = _wp.position - _transform.position;
            }
        }

        _state = NodeState.RUNNING;
        return _state;
    }
}
