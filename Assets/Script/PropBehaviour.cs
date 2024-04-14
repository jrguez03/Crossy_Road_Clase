using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.EventSystems;
using static Swipe_Controller;

public class PropsBehaviour : MonoBehaviour
{
    public PlayerBehaviour pb_PlayerBehaviour;
    public Swipe_Controller pb_SwipeController;

    [SerializeField] GameObject pb_Player;
    [SerializeField] GameObject pb_Prop;

    public float pb_Duration = 0.25f;
    public float pb_Offset = 100f;

    Vector3 pb_ClickInicial;
    Vector3 pb_AlSoltarClick;
    
    public void Awake()
    {
        pb_Prop = this.gameObject;
    }

    private void Update()
    {
        RaycastHit pb_HitInfo = PlayerBehaviour.p_LastRay;

        if (Input.GetMouseButton(0))
        {
            pb_ClickInicial = Input.mousePosition;
        }

        if (Input.GetMouseButton(0))
        {
            pb_AlSoltarClick = Input.mousePosition;

            Vector3 pb_Diferencia = pb_AlSoltarClick - pb_ClickInicial;

            if (Mathf.Abs(pb_Diferencia.magnitude) > pb_Offset)
            {
                pb_Diferencia = pb_Diferencia.normalized;

                pb_Diferencia.z = pb_Diferencia.y;

                if (Mathf.Abs(pb_Diferencia.x) > Mathf.Abs(pb_Diferencia.z))
                {
                    pb_Diferencia.z = 0.0f;
                }
                else
                {
                    pb_Diferencia.x = 0.0f;
                }

                pb_Diferencia.y = 0.0f;

                if (Physics.Raycast(pb_Player.transform.position + new Vector3(0, 1f, 0), pb_Diferencia, out pb_HitInfo, 1f))
                {
                    Debug.Log("Hit Something, Restricting Movement");
                    if (pb_HitInfo.collider.tag != "ProceduralTerrain")
                    {
                        if (pb_Diferencia.z != 0)
                        {
                            pb_Diferencia.z = 0;
                        }
                    }
                }

                if (pb_Diferencia.z >= 0)
                {
                    LeanTween.move(pb_Prop, pb_Prop.transform.position + new Vector3(0, 0, -pb_Diferencia.z), pb_Duration).setEase(LeanTweenType.easeOutQuad);
                }

                if (pb_Diferencia.z < 0 && PlayerBehaviour.p_Instance.p_StepsBack < 4)
                {
                    LeanTween.move(pb_Prop, pb_Prop.transform.position + new Vector3(0, 0, -pb_Diferencia.z), pb_Duration).setEase(LeanTweenType.easeOutQuad);
                }
            }
        }
    }

    /*public void MoveTarget(Vector3 pb_Direction)
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
    }*/
}