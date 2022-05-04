using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Destructable : MonoBehaviour, IDamageable, IDroppable
{
    [SerializeField] private IntValue m_maxHealth;
    [SerializeField] private List<Drops> m_possibleDrops = new List<Drops>();
    [SerializeField] private GameEvent m_onBoxBreak;

    private PhotonView m_photonView;

    public int Health { get; set; }


    private void Awake() => m_photonView = GetComponent<PhotonView>();

    private void Start()
    {
        Health = m_maxHealth.value;
    }

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
        Debug.Log("hit, damage = " + damageAmount + ". Health = " + Health);
        if (Health <= 0)
        {
            Drop(RandomItemDrop.GetRandomDrop(m_possibleDrops));
            Destroy(gameObject);
        }
    }

    [PunRPC]
    public void RPC_Damage(int damageAmount, bool isViewMine) 
    {
        Health -= damageAmount;
        Debug.Log("hit, damage = " + damageAmount + ". Health = " + Health);
        if (Health <= 0)
        {
            if (isViewMine)
            {
                m_onBoxBreak.Raise();
                Debug.Log($"BoxBreaked, Event Raised");
            }


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
