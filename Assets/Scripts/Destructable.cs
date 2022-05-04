using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Destructable : MonoBehaviour, IDamageable, IDroppable
{
    [SerializeField] private IntValue m_maxHealth;
    [SerializeField] private List<Drops> m_possibleDrops = new List<Drops>();
    [SerializeField] private GameEvent m_onBoxBreak;

    private PhotonView m_photonView;

    private bool m_isKillerViewMine;

    public int Health { get; set; }


    private void Awake() => m_photonView = GetComponent<PhotonView>();

    private void Start()
    {
        Health = m_maxHealth.value;
    }

    public void Damage(int damageAmount, PhotonView view)
    {
        m_isKillerViewMine = view.IsMine;

        if (view.CompareTag("Enemy"))
        {
            m_photonView.RPC("RPC_Damage", RpcTarget.All, damageAmount, false);
            return;
        }

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

    public void Drop(GameObject drop)
    {
        if(drop != null)
            Instantiate(drop, transform.position, Quaternion.identity);
    }

    private void Die(bool hasView)
    {
        if (hasView && m_isKillerViewMine)
        {
            m_onBoxBreak.Raise();
        }
        Drop(RandomItemDrop.GetRandomDrop(m_possibleDrops));
        Destroy(gameObject);
    }
}
