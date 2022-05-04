using UnityEngine;
using Photon.Pun;

public class DoubleCannon : Weapon
{
    [SerializeField] private Transform m_rightFirePoint;
    [SerializeField] private Transform m_leftFirePoint;

    protected override void Shoot()
    {
        if (_currentAmmo <= 0)
            return;

        if (_isPhotonViewMine)
        {
            _nextTimeToFire = Time.time + _fireRate;

            _currentAmmo -= 2;
            AmmoCount.OnCurrentAmmoChange(_currentAmmo);
        }

        _photonView.RPC("SpawnRightBullet", RpcTarget.All);
        _photonView.RPC("SpawnLeftBullet", RpcTarget.All);
        //SpawnRightBullet();
        //SpawnLeftBullet();
    }

    [PunRPC]
    private void SpawnRightBullet()
    {
        var bullet = _bulletPool.Get();
        bullet.transform.position = m_rightFirePoint.position;
        bullet.transform.rotation = m_rightFirePoint.rotation;
        bullet.GetComponent<Rigidbody2D>().AddForce(_firePoint.right * _bulletSpeed, ForceMode2D.Impulse);

        Bullet bulletScript = bullet.GetComponent<Bullet>();
        bulletScript.SetDamage(_damage);

        if(_isPhotonViewMine)
            bulletScript.SetPhotonView(_photonView);

        bulletScript.Init(KillBullet);
    }

    [PunRPC]
    private void SpawnLeftBullet()
    {
        var bullet = _bulletPool.Get();
        bullet.transform.position = m_leftFirePoint.position;
        bullet.transform.rotation = m_leftFirePoint.rotation;
        bullet.GetComponent<Rigidbody2D>().AddForce(_firePoint.right * _bulletSpeed, ForceMode2D.Impulse);

        Bullet bulletScript = bullet.GetComponent<Bullet>();
        bulletScript.SetDamage(_damage);

        if (_isPhotonViewMine)
            bulletScript.SetPhotonView(_photonView);

        bulletScript.Init(KillBullet);
    }


}
