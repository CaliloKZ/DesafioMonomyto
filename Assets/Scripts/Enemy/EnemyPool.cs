using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using Photon.Pun;

public class EnemyPool : MonoBehaviour
{
    private static ObjectPool<GameObject> m_enemyPool;

    private PhotonView m_photonView;

    [SerializeField] private List<Transform> m_enemySpawnLocations = new List<Transform>();
    private int m_enemySpawnIndex;

    [SerializeField] private GameObject m_enemyPrefab;
    private string m_prefabPath = "PhotonPrefabs/";

    [SerializeField] private int _poolDefaultCapacity = 10;
    [SerializeField] private int _poolMaxCapacity = 100;

    private void Awake()
    {
        m_photonView = GetComponent<PhotonView>();
    }

    private void Start()
    {
        m_enemyPool = new ObjectPool<GameObject>(() => {
            return PoolOnCreate();
        }, enemy => {
            PoolOnGet(enemy);
        }, enemy => {
            PoolOnRelease(enemy);
        }, enemy => {
            PoolOnDestroy(enemy);
        }, false, _poolDefaultCapacity, _poolMaxCapacity);

        if (m_photonView.IsMine)
            SpawnEnemies(m_enemySpawnLocations.Count);
    }

    #region PoolMethods

    private GameObject PoolOnCreate()
    {
        Debug.Log($"EnemySpawnIndex: {m_enemySpawnIndex}");
        var enemy = PhotonNetwork.Instantiate((m_prefabPath + m_enemyPrefab.name), m_enemySpawnLocations[m_enemySpawnIndex].position, Quaternion.identity);
        enemy.transform.SetParent(transform);
        return enemy;
    }

    private void PoolOnGet(GameObject obj)
    {
        obj.GetComponentInChildren<EnemyHealth>().ResetHealth();
        obj.SetActive(true);
    }

    private void PoolOnRelease(GameObject obj)
    {
        obj.SetActive(false);
    }

    private void PoolOnDestroy(GameObject obj)
    {
        Destroy(obj);
    }

    #endregion

    public void SpawnEnemies(int amount)
    {
        for(int i = 0; i < amount; i++)
        {
            m_enemyPool.Get();
            m_enemySpawnIndex++;

            if(m_enemySpawnIndex > m_enemySpawnLocations.Count)
                m_enemySpawnIndex = 0;
        }
    }

    public static void OnEnemyDeath(EnemyHealth enemy)
    {
        Debug.Log($"m_enemyPool = {m_enemyPool}");
        Debug.Log($"m_enemy = {enemy}");
        Debug.Log($"m_enemyGameObject = {enemy.gameObject}");
        m_enemyPool.Release(enemy.gameObject);
    }

}
