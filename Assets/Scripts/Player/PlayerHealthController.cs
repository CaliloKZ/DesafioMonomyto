using UnityEngine;
using Photon.Pun;

public class PlayerHealthController : MonoBehaviour, IDamageable
{
    [SerializeField] private IntValue m_maxHealth;
    [SerializeField] private IntValue m_currentHealth;
    [SerializeField] private GameEvent m_onPlayerDeath;
    [SerializeField] private GameEvent m_onOtherPlayerDeath;
    [SerializeField] private GameEvent m_onPlayerHealthChange;

    private PhotonView m_photonView;

    public int Health { get; set; }

    private void Awake() => m_photonView = GetComponent<PhotonView>();
    private void OnEnable() => Health = m_maxHealth.value;

    
    public void Damage(int damageAmount)
    {
        m_photonView.RPC("RPC_Damage", RpcTarget.All, damageAmount);
    }

    public void Damage(int damageAmount, PhotonView view)
    {
        m_photonView.RPC("RPC_Damage", RpcTarget.All, damageAmount, view.IsMine);
    }

    [PunRPC]
    private void RPC_Damage(int damageAmount)
    {
        //if (!m_photonView.IsMine)
        //    return;

        Health -= damageAmount;
        m_currentHealth.value = Health;

        if (m_photonView.IsMine)
            m_onPlayerHealthChange.Raise();

        if (Health <= 0)
        {
            Die();
        }
    }

    [PunRPC]
    private void RPC_Damage(int damageAmount, bool isViewMine)
    {
        //if (!m_photonView.IsMine)
        //    return;

        Health -= damageAmount;
        m_currentHealth.value = Health;

        if (m_photonView.IsMine)
            m_onPlayerHealthChange.Raise();

        if (Health <= 0)
        {
            if (isViewMine)
                m_onOtherPlayerDeath.Raise();

            Die();
        }
    }

    private void Die()
    {
        if (m_photonView.IsMine)
            m_onPlayerDeath.Raise();

        gameObject.SetActive(false);
    }
}
