using UnityEngine;
using Photon.Pun;

public class EnemyShoot : MonoBehaviour
{
    private PhotonView m_photonView;

    [SerializeField] private Transform m_firePoint;
    [SerializeField] private GameObject m_bulletPrefab;
    [SerializeField] private float m_bulletSpeed;
    [SerializeField] private int m_damage;

    [SerializeField] private float _fireRate;
    private float _nextTimeToFire = 0f;


    private void Awake()
    {
        m_photonView = GetComponent<PhotonView>();
    }

    public void Shoot()
    {
        if (!m_photonView.IsMine)
            return;

        if (Time.time > _nextTimeToFire)
        {
            _nextTimeToFire = Time.time + _fireRate;
            m_photonView.RPC("RPC_EnemyShoot", RpcTarget.All);
        }
    }

    [PunRPC]
    private void RPC_EnemyShoot()
    {
        var _bullet = Instantiate(m_bulletPrefab, m_firePoint.position, m_firePoint.rotation);
        _bullet.GetComponent<Rigidbody2D>().AddForce(m_firePoint.right * m_bulletSpeed, ForceMode2D.Impulse);
        Bullet bulletScript = _bullet.GetComponent<Bullet>();
        bulletScript.SetDamage(m_damage);
        bulletScript.Init(KillBullet);
    }

    private void KillBullet(Bullet bullet)
    {
        Destroy(bullet.gameObject);
    }
}
