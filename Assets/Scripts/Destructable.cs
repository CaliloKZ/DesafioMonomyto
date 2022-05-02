using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructable : MonoBehaviour, IDamageable, IDroppable
{
    [SerializeField] private int m_initialHealth;
    [SerializeField] private List<Drops> m_possibleDrops = new List<Drops>();
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
            Drop(RandomItemDrop.GetRandomDrop(m_possibleDrops));
            Destroy(gameObject);
        }
    }

    public void Drop(GameObject drop)
    {
        if(drop != null)
            Instantiate(drop, transform.position, Quaternion.identity);
    }
}
