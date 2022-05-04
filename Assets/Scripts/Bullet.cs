using System;
using UnityEngine;
using Photon.Pun;

public class Bullet : MonoBehaviour
{
    private PhotonView m_shooterPhotonView;
    private Action<Bullet> m_killAction;
    private int m_damage;

    public void Init(Action<Bullet> killAction) => m_killAction = killAction;

    public void SetDamage(int damageAmount) => m_damage = damageAmount;

    public void SetPhotonView(PhotonView view) => m_shooterPhotonView = view;

    

    private void OnTriggerEnter2D(Collider2D other)
    {
        IDamageable hit = other.GetComponent<IDamageable>();
        if(hit != null && m_shooterPhotonView != null)
        {
             hit.Damage(m_damage, m_shooterPhotonView);
        }
        m_killAction(this);
    }
}
