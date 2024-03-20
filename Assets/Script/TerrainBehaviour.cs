using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainBehaviour : MonoBehaviour
{
    public Swipe_Controller t_SwipeController;
    public Input_Terrain t_InputTerrain;
    public PlayerBehaviour t_PlayerBehaviour;

    public float t_Offset = 100f;
    public float t_Duration = 0.25f;

    [SerializeField] GameObject t_Terrain;
    [SerializeField] GameObject t_Player;

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

    public void MoveTarget(Vector3 t_Direction)
    {
        RaycastHit t_HitInfo = PlayerBehaviour.p_LastRay;

        if (t_PlayerBehaviour.p_CanMove)
        {
            if (Physics.Raycast(t_Player.transform.position + new Vector3(0, 1f, 0), t_Direction, out t_HitInfo, 1f))
            {
                Debug.Log("Hit Something, Restricting Movement");

                if (t_Direction.z != 0)
                {
                    t_Direction.z = 0;
                }

                Debug.DrawRay(transform.position + new Vector3(0, 1f, 0), transform.forward * t_HitInfo.distance, Color.red);
            }

            if (t_Direction != Vector3.zero)
            {
                LeanTween.move(t_Terrain, t_Terrain.transform.position + new Vector3(0, 0, -t_Direction.normalized.z), t_Duration).setEase(LeanTweenType.easeOutQuad);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            t_PlayerBehaviour.p_CanMove = true;
        }
    }
}
