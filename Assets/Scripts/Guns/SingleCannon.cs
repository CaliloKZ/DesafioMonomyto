using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Pool;

public class SingleCannon : Gun
{
    public override void Shoot(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            var bullet = _bulletPool.Get();
            bullet.transform.position = _firePoint.position;
            bullet.transform.rotation = _firePoint.rotation;
            bullet.GetComponent<Rigidbody2D>().AddForce(_firePoint.right * _bulletSpeed, ForceMode2D.Impulse);
            bullet.GetComponent<Bullet>().Init(KillBullet);
        }
    }

}
