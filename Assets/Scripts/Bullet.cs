using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Action<Bullet> m_killAction;

    public void Init(Action<Bullet> killAction)
    {
        m_killAction = killAction;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {

        }
        else if (collision.collider.CompareTag("Object"))
        {

        }
        else
        {
            m_killAction(this);
        }
    }
}
