using System;
using UnityEngine;
using Photon.Pun;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    private Action<EnemyHealth> m_killAction;

    [SerializeField] private IntValue m_maxHealth;
    [SerializeField] private GameEvent m_onEnemyDeath;

    private PhotonView m_photonView;

    public int Health { get; set; }

    private void Awake()
    {
        m_photonView = GetComponent<PhotonView>();
        ResetHealth();
    }

    public void Init(Action<EnemyHealth> killAction) => m_killAction = killAction;

    public void ResetHealth() => Health = m_maxHealth.value;



    public void Damage(int damageAmount)
    {
        m_photonView.RPC("RPC_Damage", RpcTarget.All, damageAmount);
    }

    public void Damage(int damageAmount, PhotonView view)
    {
        m_photonView.RPC("RPC_Damage", RpcTarget.All, damageAmount, view.IsMine);
    }

    [PunRPC]
    public void RPC_Damage(int damageAmount)
    {
        Health -= damageAmount;
        if (Health <= 0)
        {
            //m_photonView.RPC("Die", RpcTarget.All);
            Die();
        }
    }

    [PunRPC]
    public void RPC_Damage(int damageAmount, bool isViewMine)
    {
        Health -= damageAmount;
        if (Health <= 0)
        {
            //m_photonView.RPC("Die", RpcTarget.All);
            if (isViewMine)
            {
                m_onEnemyDeath.Raise();
                Debug.Log($"EnemyDied, Event Raised");
            }
 
            Die();
        }
    }

    //[PunRPC]
    private void Die()
    {
        EnemyPool.OnEnemyDeath(this);
    }
}
