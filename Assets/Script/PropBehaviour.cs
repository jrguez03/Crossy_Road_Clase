using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PropsBehaviour : MonoBehaviour
{
    public Spawn_Props pb_SpawnProps;
    public PlayerBehaviour pb_PlayerBehaviour;
    public Swipe_Controller pb_SwipeController;

    [SerializeField] GameObject pb_Player;
    [SerializeField] GameObject pb_Prop;

    public float pb_Duration = 0.25f;

    
    public void Awake()
    {
        pb_Prop = this.gameObject;
    }

    public void OnEnable()
    {
        pb_SwipeController.OnSeMueve += MoveTarget;
    }

    public void OnDisable()
    {
        pb_SwipeController.OnSeMueve -= MoveTarget;
    }

    public void MoveTarget(Vector3 pb_Direction)
    {
        RaycastHit pb_HitInfo = PlayerBehaviour.p_LastRay;

        if (pb_PlayerBehaviour.p_CanMove)
        {
            if (Physics.Raycast(pb_Player.transform.position + new Vector3(0, 1f, 0), pb_Direction, out pb_HitInfo, 1f))
            {
                Debug.Log("Hit Something, Restricting Movement");
                if (pb_HitInfo.collider.tag != "ProceduralTerrain")
                {
                    if (pb_Direction.z != 0)
                    {
                        pb_Direction.z = 0;
                    }
                }

                Debug.DrawRay(transform.position + new Vector3(0, 1f, 0), transform.forward * pb_HitInfo.distance, Color.red);
            }

            if (pb_Direction != Vector3.zero)
            {
                print("Se mueve");
                LeanTween.move(pb_Prop, pb_Prop.transform.position + new Vector3(0, 0, -pb_Direction.normalized.z), pb_Duration).setEase(LeanTweenType.easeOutQuad);
            }
        }
    }
}