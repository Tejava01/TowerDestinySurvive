using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : ObjectPool<MonsterBase>
{
    [SerializeField] protected float minSpawnTime = 2.5f;
    [SerializeField] protected float maxSpawnTime = 5f;

    protected float m_spawnTimer = 0f;
    protected float m_spawnCycleTime = 0f;
    protected int m_poolObjectIndex = 0;

    private void Awake()
    {
        ProtPoolSetting();
        m_spawnCycleTime = Random.Range(minSpawnTime, maxSpawnTime);
    }

    private void Update()
    {
        PrivSpawnTimer();
    }

    //-----------------------------------------------

    private void PrivSpawnTimer()
    {
        m_spawnTimer += Time.deltaTime;

        if (m_spawnTimer >= m_spawnCycleTime)
        {
            m_poolObjectIndex = Random.Range(0, poolObject.Length);
            PrivDoSpawn(m_poolObjectIndex);

            m_spawnTimer -= m_spawnCycleTime; //초과한 deltaTime만큼 손실 방지
            m_spawnCycleTime = Random.Range(minSpawnTime, maxSpawnTime);
        }
    }

    private void PrivDoSpawn(int poolObjectIndex)
    {
        MonsterBase monster = ProtDoPoolNextObject(poolObjectIndex);
        monster.gameObject.SetActive(true);
    }
}
