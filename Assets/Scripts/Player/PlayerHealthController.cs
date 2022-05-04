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
    private bool m_isPlayerPhotonViewMine;

    private bool m_isKillerViewMine;

    public int Health { get; set; }

    private void Awake()
    {
        m_photonView = GetComponent<PhotonView>();
        m_isPlayerPhotonViewMine = m_photonView.IsMine;
    }

    private void OnEnable()
    {
        Health = m_maxHealth.value;
        m_currentHealth.value = Health;
        if (m_isPlayerPhotonViewMine)
        {
            m_onPlayerHealthChange.Raise();
        }
    }

    public void Damage(int damageAmount, PhotonView view)
    {
        m_isKillerViewMine = view.IsMine;

        if (view.CompareTag("Enemy"))
        {
            m_photonView.RPC("RPC_Damage", RpcTarget.All, damageAmount, false);
            return;
        }

        if (!view.CompareTag("Enemy") && m_isKillerViewMine)
        {
            m_photonView.RPC("RPC_Damage", RpcTarget.All, damageAmount, true);
        }

    }


    [PunRPC]
    private void RPC_Damage(int damageAmount, bool hasView)
    {
        Health -= damageAmount;
        m_currentHealth.value = Health;

        if (m_isPlayerPhotonViewMine)
            m_onPlayerHealthChange.Raise();


        if (Health <= 0)
        {
            Die(hasView);
        }
    }

    private void Die(bool hasView)
    {
        if (hasView && m_isKillerViewMine)
            m_onOtherPlayerDeath.Raise();

        if (m_isPlayerPhotonViewMine)
        {
            m_onPlayerDeath.Raise();
            PhotonNetwork.Destroy(gameObject);
        }
    }
}
