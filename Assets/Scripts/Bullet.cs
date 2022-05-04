using System;
using UnityEngine;
using Photon.Pun;

public class Bullet : MonoBehaviour
{
    private PhotonView m_playerPhotonView;
    private Action<Bullet> m_killAction;
    private int m_damage;

    public void Init(Action<Bullet> killAction) => m_killAction = killAction;

    public void SetDamage(int damageAmount) => m_damage = damageAmount;

    public void SetPhotonView(PhotonView view) => m_playerPhotonView = view;

    private void OnTriggerEnter2D(Collider2D other)
    {
        IDamageable hit = other.GetComponent<IDamageable>();
        
        if(hit != null)
        {
            if (m_playerPhotonView != null)
            {
                hit.Damage(m_damage, m_playerPhotonView);
            }

            else
                hit.Damage(m_damage);
        }

        m_killAction(this);
    }
}
