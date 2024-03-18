using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prueba1 : MonoBehaviour
{
    public Swipe_Controller m_SwipeController;
    public Prueba m_TerrainGeneratorManager;

    public float m_Offset = 100f;
    public float m_Duration = 1f;
    public GameObject m_Terrain;
    public bool m_IsMoving = false;

    private bool m_IsRecycled = false;

    public void Awake()
    {
        m_Terrain = this.gameObject;
    }

    public void OnEnable()
    {
        m_SwipeController.OnSeMueve += MoveTarget;
    }

    public void OnDisable()
    {
        m_SwipeController.OnSeMueve -= MoveTarget;
        m_IsMoving = false;
    }

    void MoveTarget(Vector3 m_Direction)
    {
        LeanTween.move(m_Terrain, m_Terrain.transform.position + new Vector3(0, 0, -m_Direction.z), m_Duration).setEase(LeanTweenType.easeOutQuad);
        m_IsMoving = true;
    }

    public void OnBecameInvisible()
    {
        if (!m_IsRecycled)
        {
            m_TerrainGeneratorManager.RecycleTerrain(m_Terrain);
            m_IsRecycled = true;

        }

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Entro");
            m_TerrainGeneratorManager.NewLevelZone();

        }

    }
    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }
}
