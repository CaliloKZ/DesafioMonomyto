using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Pool;

public abstract class Gun : MonoBehaviour
{
    private PlayerInputActions m_playerInputActions;

    [SerializeField] protected float _damage;
    [SerializeField] protected float _fireRate;
    [SerializeField] protected float _bulletSpeed;
    [SerializeField] protected GameObject _bulletPrefab;

    [SerializeField] protected Transform _firePoint;

    protected ObjectPool<GameObject> _bulletPool;

    [SerializeField] protected int _poolDefaultCapacity = 10;
    [SerializeField] protected int _poolMaxCapacity = 100;

    protected bool _canShoot;

    protected void Awake()
    {
        m_playerInputActions = new PlayerInputActions();
    }

    protected void OnEnable()
    {
        m_playerInputActions.Gun.Enable();
        m_playerInputActions.Gun.Shoot.performed += Shoot;
    }

    protected void OnDisable()
    {
        m_playerInputActions.Gun.Shoot.performed -= Shoot;
        m_playerInputActions.Gun.Disable();
    }

    protected virtual void Start()
    {
        _bulletPool = new ObjectPool<GameObject>(() => {
            return Instantiate(_bulletPrefab, _firePoint.position, _firePoint.rotation);
        }, bullet => {
            bullet.SetActive(true);
        }, bullet => {
            bullet.SetActive(false);
        }, bullet => { Destroy(bullet);
        }, false, _poolDefaultCapacity, _poolMaxCapacity);
    }

    public virtual void Shoot(InputAction.CallbackContext context){}

    protected virtual void KillBullet(Bullet bullet)
    {
        _bulletPool.Release(bullet.gameObject);
    }
}
