using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.EventSystems;
using static Swipe_Controller;

public class PropsBehaviour : MonoBehaviour
{
    Vector3 pb_ClickInicial;
    Vector3 pb_AlSoltarClick;

    public PlayerBehaviour pb_PlayerBehaviour;
    public Swipe_Controller pb_SwipeController;

    [SerializeField] GameObject pb_Player;
    [SerializeField] GameObject pb_Prop;

    public float pb_Offset = 100f;
    public float pb_Duration = 0.25f;
    public int pb_StepsBack = 0;

    public delegate void SeMueve(Vector3 diferencia);
    public event SeMueve OnSeMueve;

    public void Awake()
    {
        pb_Prop = this.gameObject;
    }
    private void Update()
    {
        RaycastHit pb_Hitinfo = PlayerBehaviour.p_LastRay;
        if (Input.GetMouseButtonDown(0) && pb_PlayerBehaviour.p_MoveLevel)
        {
            pb_ClickInicial = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0) && pb_PlayerBehaviour.p_MoveLevel)
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

                if (OnSeMueve != null)
                {
                    OnSeMueve(pb_Diferencia);
                }

                //Pararlo si el eprsonaje choca
                if (Physics.Raycast(PlayerBehaviour.p_Instance.transform.position + new Vector3(0, 1f, 0), pb_Diferencia, out pb_Hitinfo, 1f))
                {
                    if (pb_Hitinfo.collider.tag != "ProceduralTerrain")
                    {
                        if (pb_Diferencia.z != 0)
                        {
                            pb_Diferencia.z = 0;
                        }
                    }
                }

                //Movimiento hacia adelante
                if (pb_Diferencia.normalized.z >= 0 && pb_PlayerBehaviour.p_StepsBack == 0)
                {
                    LeanTween.move(pb_Prop, pb_Prop.transform.position + new Vector3(0, 0, -pb_Diferencia.normalized.z), 0.25f).setEase(LeanTweenType.easeOutQuad);
                }

                //Solo 4 pasos atras.
                if (pb_StepsBack < 4 && pb_Diferencia.normalized.z <= 0)
                {
                    pb_StepsBack++;
                    LeanTween.move(pb_Player, pb_Player.transform.position + new Vector3(pb_Diferencia.x / 2, 0, pb_Diferencia.z / 2) + Vector3.up / 2, pb_Duration / 2).setEase(LeanTweenType.easeOutQuad).setOnComplete(() =>
                    {
                        LeanTween.move(pb_Player, pb_Player.transform.position + new Vector3(pb_Diferencia.x / 2, 0, pb_Diferencia.z / 2) - Vector3.up / 2, pb_Duration / 2).setEase(LeanTweenType.easeOutQuad);
                    });
                }
                if (pb_StepsBack != 0 && pb_Diferencia.normalized.z >= 0)
                {
                    pb_StepsBack--;
                    LeanTween.move(pb_Player, pb_Player.transform.position + new Vector3(pb_Diferencia.x / 2, 0, pb_Diferencia.z / 2) + Vector3.up / 2, pb_Duration / 2).setEase(LeanTweenType.easeOutQuad).setOnComplete(() =>
                    {
                        LeanTween.move(pb_Player, pb_Player.transform.position + new Vector3(pb_Diferencia.x / 2, 0, pb_Diferencia.z / 2) - Vector3.up / 2, pb_Duration / 2).setEase(LeanTweenType.easeOutQuad);
                    });
                }
            }
            else if (Mathf.Abs(pb_Diferencia.magnitude) < pb_Offset)
            {
                Vector3 pb_Clicka = pb_Prop.transform.forward;

                if (OnSeMueve != null)
                {
                    OnSeMueve(pb_Clicka);
                }

                //Pararlo si el eprsonaje choca
                if (Physics.Raycast(PlayerBehaviour.p_Instance.transform.position + new Vector3(0, 1f, 0), pb_Clicka, out pb_Hitinfo, 1f))
                {
                    if (pb_Hitinfo.collider.tag != "ProceduralTerrain")
                    {
                        if (pb_Clicka.z != 0)
                        {
                            pb_Clicka.z = 0;
                        }
                    }
                }

                //Movimiento hacia adelante
                if (pb_Clicka.normalized.z >= 0 && pb_PlayerBehaviour.p_StepsBack == 0)
                {
                    LeanTween.move(pb_Prop, pb_Prop.transform.position + new Vector3(0, 0, -pb_Clicka.normalized.z), 0.25f).setEase(LeanTweenType.easeOutQuad);
                }

                //Solo 4 pasos atras.
                if (pb_StepsBack < 4 && pb_Clicka.normalized.z <= 0)
                {
                    pb_StepsBack++;
                    LeanTween.move(pb_Player, pb_Player.transform.position + new Vector3(pb_Clicka.x / 2, 0, pb_Clicka.z / 2) + Vector3.up / 2, pb_Duration / 2).setEase(LeanTweenType.easeOutQuad).setOnComplete(() =>
                    {
                        LeanTween.move(pb_Player, pb_Player.transform.position + new Vector3(pb_Clicka.x / 2, 0, pb_Clicka.z / 2) - Vector3.up / 2, pb_Duration / 2).setEase(LeanTweenType.easeOutQuad);
                    });
                }
                if (pb_StepsBack != 0 && pb_Clicka.normalized.z >= 0)
                {
                    pb_StepsBack--;
                    LeanTween.move(pb_Player, pb_Player.transform.position + new Vector3(pb_Clicka.x / 2, 0, pb_Clicka.z / 2) + Vector3.up / 2, pb_Duration / 2).setEase(LeanTweenType.easeOutQuad).setOnComplete(() =>
                    {
                        LeanTween.move(pb_Player, pb_Player.transform.position + new Vector3(pb_Clicka.x / 2, 0, pb_Clicka.z / 2) - Vector3.up / 2, pb_Duration / 2).setEase(LeanTweenType.easeOutQuad);
                    });
                }
            }
        }
    }
}