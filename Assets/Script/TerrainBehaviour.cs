using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainBehaviour : MonoBehaviour
{
    public Swipe_Controller t_SwipeController;
    public Input_Terrain t_InputTerrain;

    public float t_Offset = 100f;
    public float t_Duration = 0.25f;

    [SerializeField] GameObject t_Terrain;

    private bool m_IsRecycled = false;

    public void Awake()
    {
        t_Terrain = this.gameObject;
    }

    public void OnEnable()
    {
        t_SwipeController.OnSeMueve += MoveTarget;
    }

    public void OnDisable()
    {
        t_SwipeController.OnSeMueve -= MoveTarget;
    }

    void MoveTarget(Vector3 m_Direction)
    {
        LeanTween.move(t_Terrain, t_Terrain.transform.position + new Vector3(0, 0, -m_Direction.normalized.z), t_Duration).setEase(LeanTweenType.easeOutQuad);
    }

    public void OnBecameInvisible()
    {
        if (!m_IsRecycled)
        {
            t_InputTerrain.RecycleTerrain(t_Terrain);
            m_IsRecycled = true;
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Entro");
            t_InputTerrain.NewLevelZone();
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
