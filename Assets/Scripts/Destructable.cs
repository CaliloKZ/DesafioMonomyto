using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructable : MonoBehaviour, IDamageable
{
    [SerializeField] private int m_initialHealth;
    [HideInInspector] public int Health { get; set; }

    private void Start()
    {
        Health = m_initialHealth;
    }

    public void Damage(int damageAmount)
    {
        Health -= damageAmount;
        Debug.Log("hit, damage = " + damageAmount + ". Health = " + Health);
        if (Health <= 0)
        {
            Destroy(gameObject);
        }
    }

}
