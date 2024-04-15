using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropMov : MonoBehaviour
{
    public Swipe_Controller pm_SwipeController;
    public PlayerBehaviour pm_PlayerBehaviour;

    public float pm_Duration = 0.25f;

    [SerializeField] GameObject pm_PropBus;
    [SerializeField] GameObject pm_Player;

    public bool pm_CanMove = true;

    public void Awake()
    {
        pm_PropBus = this.gameObject;
    }

    public void OnEnable()
    {
        pm_SwipeController.OnSeMueve += MoveTarget;
    }

    public void OnDisable()
    {
        pm_SwipeController.OnSeMueve -= MoveTarget;
    }

    public void MoveTarget(Vector3 t_Direction)
    {
        RaycastHit t_HitInfo = PlayerBehaviour.p_LastRay;

        Vector3 t_DirectionNormalized = t_Direction.normalized;

        if (pm_PlayerBehaviour.p_CanMove && pm_CanMove)
        {
            if (Physics.Raycast(pm_Player.transform.position + new Vector3(0, 1f, 0), t_Direction, out t_HitInfo, 1f))
            {
                Debug.Log("Hit Something, Restricting Movement");
                if (t_HitInfo.collider.tag != "ProceduralTerrain")
                {
                    if (t_DirectionNormalized.z != 0)
                    {
                        t_DirectionNormalized.z = 0;
                    }
                }

                Debug.DrawRay(transform.position + new Vector3(0, 1f, 0), transform.forward * t_HitInfo.distance, Color.red);
            }

            if (t_DirectionNormalized.z >= 0 && pm_PlayerBehaviour.p_StepsBack == 0)
            {
                LeanTween.move(pm_PropBus, pm_PropBus.transform.position + new Vector3(0, 0, -t_DirectionNormalized.z), pm_Duration).setEase(LeanTweenType.easeOutQuad);
            }
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            pm_PlayerBehaviour.p_CanMove = true;
        }
    }
}
