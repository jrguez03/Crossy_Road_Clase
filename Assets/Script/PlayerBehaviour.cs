using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerBehaviour : MonoBehaviour
{
    public Swipe_Controller p_SwipeController;
    public CoinBehaviour p_CoinBehaviour;

    [SerializeField] GameObject p_Player;
    [SerializeField] GameObject p_DieScreen;
    [SerializeField] GameObject p_ScorePlayer;
    [SerializeField] GameObject p_CoinsPlayer;
    [SerializeField] GameObject p_CanvasCoin;

    public float p_Offset = 100f;
    public float p_Duration = 0.25f;
    public float p_fade = 100f;
    public float p_fadetime = 1f;
    public int p_StepsBack = 0;

    public bool p_CanMove = true;
    public bool p_MoveLevel = true;

    public static PlayerBehaviour p_Instance;
    public static RaycastHit p_LastRay;

    public void Awake()
    {
        if (p_Instance == null)
        {
            p_Instance = this;
        }
        else
        {
            Destroy(this);
        }

        p_Player = this.gameObject;
    }

    public void Start()
    {
        p_SwipeController.OnSeMueve += MoveTarget;
    }

    public void OnDisable()
    {
        p_SwipeController.OnSeMueve -= MoveTarget;
    }

    void MoveTarget(Vector3 p_Direction)
    {
        if (p_CanMove)
        {
            RaycastHit p_HitInfo;

            Vector3 p_MoveDirection = p_Direction.normalized;

            if (Physics.Raycast(transform.position + new Vector3(0, 1f, 0), p_Direction, out p_HitInfo, 1f))
            {
                Debug.Log("Hit Something, Restricting Movement");

                p_LastRay = p_HitInfo;

                if (p_MoveDirection.x != 0)
                {
                    p_MoveDirection.x = 0;
                }

                Debug.DrawRay(transform.position + new Vector3(0, 1f, 0), transform.forward * p_HitInfo.distance, Color.red);
            }

            if (p_MoveDirection != Vector3.zero)
            {
                if (p_MoveDirection.x > 0)
                {
                    transform.eulerAngles = new Vector3(0, 90f, 0);
                }
                else if (p_MoveDirection.x < 0)
                {
                    transform.eulerAngles = new Vector3(0, -90f, 0);
                }
                else if (p_MoveDirection.z > 0)
                {
                    transform.eulerAngles = new Vector3(0, 0, 0);
                }
                else if (p_MoveDirection.z < 0)
                {
                    transform.eulerAngles = new Vector3(0, 180f, 0);
                }

                LeanTween.move(p_Player, p_Player.transform.position + new Vector3(p_MoveDirection.x, 0, 0) / 2 + Vector3.up / 2, p_Duration / 2).setEase(LeanTweenType.easeOutQuad).setOnComplete(() =>
                {
                    LeanTween.move(p_Player, p_Player.transform.position + new Vector3(p_MoveDirection.x, 0, 0) / 2 - Vector3.up / 2, p_Duration / 2).setEase(LeanTweenType.easeOutQuad);
                });

                //Solo 4 pasos atras.
                if (p_StepsBack < 4 && p_Direction.normalized.z <= 0)
                {
                    p_StepsBack++;
                    LeanTween.move(p_Player, p_Player.transform.position + new Vector3(p_MoveDirection.x / 2, 0, p_MoveDirection.z / 2) + Vector3.up / 2, p_Duration / 2).setEase(LeanTweenType.easeOutQuad).setOnComplete(() =>
                    {
                        LeanTween.move(p_Player, p_Player.transform.position + new Vector3(p_MoveDirection.x / 2, 0, p_MoveDirection.z / 2) - Vector3.up / 2, p_Duration / 2).setEase(LeanTweenType.easeOutQuad);
                    });
                }
                if (p_StepsBack != 0 && p_Direction.normalized.z >= 0)
                {
                    p_StepsBack--;
                    LeanTween.move(p_Player, p_Player.transform.position + new Vector3(p_MoveDirection.x / 2, 0, p_MoveDirection.z / 2) + Vector3.up / 2, p_Duration / 2).setEase(LeanTweenType.easeOutQuad).setOnComplete(() =>
                    {
                        LeanTween.move(p_Player, p_Player.transform.position + new Vector3(p_MoveDirection.x / 2, 0, p_MoveDirection.z / 2) - Vector3.up / 2, p_Duration / 2).setEase(LeanTweenType.easeOutQuad);
                    });
                }
                p_CanMove = false;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("InitialTerrain") || collision.gameObject.CompareTag("ProceduralTerrain") || collision.gameObject.CompareTag("Log"))
        {
            p_CanMove = true;
            p_MoveLevel = true;
        }

        if (collision.gameObject.CompareTag("Car"))
        {
            p_Player.SetActive(false);
            p_DieScreen.SetActive(true);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("InitialTerrain") || collision.gameObject.CompareTag("ProceduralTerrain") || collision.gameObject.CompareTag("Log"))
        {
            p_CanMove = false;
            p_MoveLevel = false;
        }
        if (collision.gameObject.CompareTag("Car"))
        {
            p_DieScreen.SetActive(true);
            p_ScorePlayer.SetActive(false);
            p_CoinsPlayer.SetActive(false);
            p_Player.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            p_CoinBehaviour.c_CoinCount += 1;

            other.gameObject.SetActive(false);

            LeanTween.alpha(p_CanvasCoin, p_fade, p_fadetime).setEase(LeanTweenType.easeInQuad).setOnComplete(() =>
            {
                LeanTween.alpha(p_CanvasCoin, p_fade, p_fadetime).setEase(LeanTweenType.easeOutQuad);
            });
        }

        if (other.gameObject.CompareTag("Die") || other.gameObject.CompareTag("Water"))
        {
            p_DieScreen.SetActive(true);
            p_ScorePlayer.SetActive(false);
            p_CoinsPlayer.SetActive(false);
            p_Player.SetActive(false);
        }
    }
}
