using UnityEngine;

public class SingleCannon : Weapon
{
    protected override void Shoot()
    {
        if (_currentAmmo <= 0)
            return;

        _nextTimeToFire = Time.time + _fireRate;

        _currentAmmo--;
        AmmoCount.OnCurrentAmmoChange(_currentAmmo);

        var bullet = _bulletPool.Get();
        bullet.transform.position = _firePoint.position;
        bullet.transform.rotation = _firePoint.rotation;
        bullet.GetComponent<Rigidbody2D>().AddForce(_firePoint.right * _bulletSpeed, ForceMode2D.Impulse);

        Bullet bulletScript = bullet.GetComponent<Bullet>();
        bulletScript.SetDamage(_damage);
        bulletScript.Init(KillBullet);
    }
}
