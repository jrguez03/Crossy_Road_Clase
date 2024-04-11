using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public Swipe_Controller p_SwipeController;
    public CoinBehaviour p_CoinBehaviour;
    public Spawn_Props p_SpawnProps;

    public float p_Offset = 100f;
    public float p_Duration = 0.25f;
    [SerializeField] GameObject p_Player;

    public bool p_CanMove = true;

    public static RaycastHit p_LastRay;

    public void Awake()
    {
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

            if (Physics.Raycast(transform.position + new Vector3(0, 1f, 0), p_Direction, out p_HitInfo, 1f))
            {
                Debug.Log("Hit Something, Restricting Movement");

                p_LastRay = p_HitInfo;

                if (p_Direction.x != 0)
                {
                    p_Direction.x = 0;
                }

                Debug.DrawRay(transform.position + new Vector3(0, 1f, 0), transform.forward * p_HitInfo.distance, Color.red);
            }

            if (p_Direction != Vector3.zero)
            {
                if (p_Direction.x > 0)
                {
                    transform.eulerAngles = new Vector3(0, 90f, 0);
                }
                else if (p_Direction.x < 0)
                {
                    transform.eulerAngles = new Vector3(0, -90f, 0);
                }
                else if (p_Direction.z > 0)
                {
                    transform.eulerAngles = new Vector3(0, 0, 0);
                }
                else if (p_Direction.z < 0)
                {
                    transform.eulerAngles = new Vector3(0, 180f, 0);
                }

                LeanTween.move(p_Player, p_Player.transform.position + new Vector3(p_Direction.normalized.x, 0, 0) / 2 + Vector3.up / 2, p_Duration / 2).setEase(LeanTweenType.easeOutQuad).setOnComplete(() =>
                {
                    LeanTween.move(p_Player, p_Player.transform.position + new Vector3(p_Direction.normalized.x, 0, 0) / 2 - Vector3.up / 2, p_Duration / 2).setEase(LeanTweenType.easeOutQuad);
                });

                p_CanMove = false;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("InitialTerrain") || collision.gameObject.CompareTag("ProceduralTerrain"))
        {
            p_CanMove = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            p_CoinBehaviour.c_CoinCount += 1;

            Destroy(other.gameObject);
        }
    }
}
