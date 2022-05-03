using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    private Action<EnemyHealth> m_killAction;

    [SerializeField] private int m_maxHealth;

    public int Health { get; set; }

    private void Awake() => ResetHealth();

    public void Init(Action<EnemyHealth> killAction) => m_killAction = killAction;

    public void ResetHealth() => Health = m_maxHealth;

    public void Damage(int damageAmount)
    {
        Health -= damageAmount;
        if(Health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        m_killAction(this);
    }
}
