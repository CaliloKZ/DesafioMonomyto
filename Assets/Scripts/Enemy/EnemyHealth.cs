using System;
using UnityEngine;
using Photon.Pun;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    private Action<EnemyHealth> m_killAction;

    [SerializeField] private IntValue m_maxHealth;
    [SerializeField] private GameEvent m_onEnemyDeath;

    private PhotonView m_photonView;

    private bool m_isKillerViewMine;
    public int Health { get; set; }

    private void Awake()
    {
        m_photonView = GetComponent<PhotonView>();
        ResetHealth();
    }

    public void Init(Action<EnemyHealth> killAction) => m_killAction = killAction;

    public void ResetHealth() => Health = m_maxHealth.value;


    public void Damage(int damageAmount, PhotonView view)
    {
        m_isKillerViewMine = view.IsMine;

        if (view.CompareTag("Enemy"))
            return;

        if (m_isKillerViewMine)
        {
            m_photonView.RPC("RPC_Damage", RpcTarget.All, damageAmount, true);
        }
    }

    [PunRPC]
    public void RPC_Damage(int damageAmount, bool hasView)
    {
        Health -= damageAmount;
        if (Health <= 0)
        {
            Die(hasView);
        }
    }

    private void Die(bool hasView)
    {
        if (hasView && m_isKillerViewMine)
        {
            m_onEnemyDeath.Raise();
        }
        EnemyPool.OnEnemyDeath(this);
    }
}
