using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour, IDamageable
{
    [SerializeField] private int m_maxHealth;
    [SerializeField] private int m_startingHealth;

    public int Health { get; set; }

    private void Awake()
    {
        Health = m_startingHealth;
    }

    public void Damage(int damageAmount)
    {
        Debug.Log("PlayerDamaged");
    }
}
