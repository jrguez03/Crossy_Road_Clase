using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PropGenerator : MonoBehaviour
{
    public GameObject m_PropsGeneratorSpawn;
    public GameObject[] m_Props;
    public int m_PropsInPool = 4;

    private void Start()
    {
        foreach (var m_PropsObjects in m_Props)
        {
            Object_Pool.PreLoad(m_PropsObjects, m_PropsInPool);
        }

    }

    public GameObject GenerateProps()
    {
        int m_RandomIndex = Random.Range(0, m_Props.Length);
        GameObject propObject = m_Props[m_RandomIndex];
        propObject.transform.position = m_PropsGeneratorSpawn.transform.position;
        return Object_Pool.GetObject(m_Props[m_RandomIndex]);
    }

}
