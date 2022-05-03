using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Pool;
using Photon.Pun;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected Weapons _weaponType;
    public Weapons GetWeaponType()
    {
        return _weaponType;
    }

    private PlayerInputActions m_playerInputActions;

    [SerializeField] protected int _damage;
    [SerializeField] protected float _bulletSpeed;
    [SerializeField] protected GameObject _bulletPrefab;
    [SerializeField] protected string _prefabPath;

    [SerializeField] protected Transform _firePoint;
    [SerializeField] protected float _fireRate;
    protected float _nextTimeToFire = 0f;

    protected ObjectPool<GameObject> _bulletPool;

    [SerializeField] protected int _poolDefaultCapacity = 10;
    [SerializeField] protected int _poolMaxCapacity = 100;

    [SerializeField] protected int _maxAmmo;
    protected int _currentAmmo;

    protected PhotonView _photonView;

    protected bool _isActive;

    public int[] GetAmmo()
    {
        var _ammo = new int[] { _currentAmmo, _maxAmmo };
        return _ammo;
    }

    protected bool _isShooting = false;

    protected virtual void Awake()
    {
        _photonView = GetComponent<PhotonView>();
        m_playerInputActions = new PlayerInputActions();
        _currentAmmo = _maxAmmo;
    }

    protected virtual void OnEnable()
    {
        _isActive = true;
    }

    protected virtual void OnDisable()
    {
        _isActive = false;
        _isShooting = false;
        _bulletPool.Dispose();
    }

    protected virtual void Start()
    {
        _bulletPool = new ObjectPool<GameObject>(() => {
            return Instantiate(_bulletPrefab);
        }, bullet => {
            PoolOnGet(bullet);
        }, bullet => {
            PoolOnRelease(bullet);
        }, bullet => {
            PoolOnDestroy(bullet);
        }, false, _poolDefaultCapacity, _poolMaxCapacity);
    }

    #region PoolMethods

    protected virtual void PoolOnGet(GameObject obj)
    {
        obj.SetActive(true);
    }

    protected virtual void PoolOnRelease(GameObject obj)
    {
        obj.SetActive(false);
    }

    protected virtual void PoolOnDestroy(GameObject obj)
    {
        Destroy(obj);
    }

    #endregion

    protected virtual void Update()
    {
        if (!_photonView.IsMine)
            return;

        if(_isShooting && Time.time >= _nextTimeToFire)
        {
            Shoot();
        }
    }

    public virtual void ShootCalled(bool isShooting)
    {
        _isShooting = isShooting;
    }


    protected virtual void Shoot() { }

    protected virtual void KillBullet(Bullet bullet)
    {
        _bulletPool.Release(bullet.gameObject);
    }

    public virtual void AmmoPickup(int amount)
    {
        _currentAmmo += amount;
        if(_currentAmmo > _maxAmmo)
            _currentAmmo = _maxAmmo;

        if(_isActive)
            AmmoCount.OnCurrentAmmoChange(_currentAmmo);
    }
   
}
