using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NuevoPropsBehaviour : MonoBehaviour
{
    public Object_Pool m_NuevoPropsBehaviour;
    public PlayerBehaviour m_PlayerBehaviour;

    private bool m_PlayerMoved = false;

    void Start()
    {
        if (m_PlayerBehaviour == null)
        {
            enabled = false;
        }
    }

    void Update()
    {
        if (m_PlayerBehaviour.p_CanMove == false)
        {
            m_PlayerMoved = true;
            RandomPrefab();
        }
        else if (m_PlayerBehaviour.p_CanMove)
        {
            m_PlayerMoved = false;
        }
    }


    void RandomPrefab()
    {
        if (m_NuevoPropsBehaviour != null)
        {
            int m_RandomIndex = Random.Range(0, m_NuevoPropsBehaviour.m_PrefabsToPool.Length);
            GameObject m_RandomPrefab = m_NuevoPropsBehaviour.m_PrefabsToPool[m_RandomIndex].m_Prefab;
            GameObject m_RandomPrefabSpawn = m_NuevoPropsBehaviour.GetObject(m_RandomPrefab);
        }
    }
}