using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Action<Bullet> m_killAction;
    private int m_damage;

    public void Init(Action<Bullet> killAction) => m_killAction = killAction;

    public void SetDamage(int damageAmount) => m_damage = damageAmount;

    private void OnTriggerEnter2D(Collider2D other)
    {
        IDamageable hit = other.GetComponent<IDamageable>();
        
        if(hit != null)
        {
            hit.Damage(m_damage);
            Debug.Log($"hit: {hit}, damage: {m_damage}");
        }
        else
        {
            Debug.Log("IDamageable component not found.");
        }

        m_killAction(this);
    }
}
