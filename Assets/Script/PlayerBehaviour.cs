using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public Swipe_Controller r_SwipeController;

    public float r_Offset = 100f;
    public float r_Duration = 0.25f;
    public GameObject r_Player;

    bool p_CanMove = true;

    Vector3 p_PreviousPosition;

    public void Awake()
    {
        r_Player = this.gameObject;
    }

    public void Start()
    {
        r_SwipeController.OnSeMueve += MoveTarget;
    }

    private void Update()
    {
        if (Physics.Raycast (transform.position + new Vector3(0, 1f, 0), transform.forward, out RaycastHit hitinfo, 1f))
        {
            Debug.Log("Hit Something");
            Debug.Log(hitinfo.transform.position.normalized.x);
            Debug.DrawRay(transform.position + new Vector3(0, 1f, 0), transform.forward * hitinfo.distance, Color.red);
        }
        else
        {
            Debug.Log("Hit Nothing");
            Debug.DrawRay(transform.position + new Vector3(0, 1f, 0), transform.forward * 1f, Color.green);
        }
    }
    public void OnDisable()
    {
        r_SwipeController.OnSeMueve -= MoveTarget;
    }

    void MoveTarget(Vector3 m_Direction)
    {
        if (p_CanMove == true)
        {
            if (m_Direction.x > 0)
            {
                transform.eulerAngles = new Vector3(0, 90f, 0);
            }
            else if (m_Direction.x < 0)
            {
                transform.eulerAngles = new Vector3(0, -90f, 0);
            }
            else if (m_Direction.z > 0)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
            else if (m_Direction.z < 0)
            {
                transform.eulerAngles = new Vector3(0, 180f, 0);
            }

            LeanTween.move(r_Player, r_Player.transform.position + new Vector3(m_Direction.normalized.x, 0, 0) / 2 + Vector3.up / 2, r_Duration / 2).setEase(LeanTweenType.easeOutQuad).setOnComplete(() =>
            {
                LeanTween.move(r_Player, r_Player.transform.position + new Vector3(m_Direction.normalized.x, 0, 0) / 2 - Vector3.up / 2, r_Duration / 2).setEase(LeanTweenType.easeOutQuad);
            });
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Suelo"))
        {
            p_PreviousPosition = transform.position;
        }

        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("choca");
            //transform.position = p_PreviousPosition;
            p_CanMove = false;
            Invoke("CanMove", 1f);
        }
    }

    private void CanMove()
    {
        p_CanMove = true;
    }
}
