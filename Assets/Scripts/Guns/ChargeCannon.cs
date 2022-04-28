using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Pool;

public class ChargeCannon : Gun
{
    [SerializeField] private float _minDamage;
    [SerializeField] private float _medDamage;

    [SerializeField] private float _minBulletSpeed;
    [SerializeField] private float _medBulletSpeed;

    public override void Shoot(InputAction.CallbackContext context)
    {

    }
}
