using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] protected T[] poolObject;
    [SerializeField] protected int poolSize;

    protected List<Stack<T>> m_listObjectPool = new List<Stack<T>>();
    protected Transform m_spawnPoint;

    //-----------------------------------------

    public void DoReturnPoolObject(int poolObjectIndex, T obj)
    {
        obj.gameObject.SetActive(false);
        m_listObjectPool[poolObjectIndex].Push(obj);
    }

    //-----------------------------------------

    protected void ProtPoolSetting()
    {
        m_spawnPoint = GetComponent<Transform>();

        for (int i = 0; i < poolObject.Length; i++)
        {
            Stack<T> pool = new Stack<T>();
            m_listObjectPool.Add(pool);

            for (int j = 0; j < poolSize; j++)
            {
                T obj = Instantiate(poolObject[i], m_spawnPoint);
                PrivbChangeLayer(obj.transform, gameObject.layer);
                obj.gameObject.SetActive(false);
                m_listObjectPool[i].Push(obj);
            }
        }
    }

    protected T ProtDoPoolNextObject(int poolObjectIndex)
    {
        T nextObj = null;
        if (m_listObjectPool[poolObjectIndex].Count == 0)
        {
            nextObj = Instantiate(poolObject[poolObjectIndex], m_spawnPoint);
        }
        else
        {
            nextObj = m_listObjectPool[poolObjectIndex].Pop();
        }

        return nextObj;
    }

    //-----------------------------------------

    private void PrivbChangeLayer(Transform obj, int layer)
    {
        obj.gameObject.layer = layer;
        if (obj.GetComponent<SpriteRenderer>() != null)
        {
            obj.GetComponent<SpriteRenderer>().sortingLayerName = LayerMask.LayerToName(layer);
        }

        foreach (Transform child in obj.transform)
        {
            PrivbChangeLayer(child, layer);
        }
    }
}
