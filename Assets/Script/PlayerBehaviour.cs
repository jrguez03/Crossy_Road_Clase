using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public Swipe_Controller r_SwipeController;

    public float r_Offset = 100f;
    public float r_Duration = 0.25f;
    public GameObject r_Player;

    public void Awake()
    {
        r_Player = this.gameObject;
    }

    public void Start()
    {
        r_SwipeController.OnSeMueve += MoveTarget;
    }

    public void OnDisable()
    {
        r_SwipeController.OnSeMueve -= MoveTarget;
    }

    void MoveTarget(Vector3 m_Direction)
    {
        if (m_Direction.x > 0)
        {
            transform.eulerAngles = new Vector3(0, -90f, 0);
        }
        else if (m_Direction.x < 0)
        {
            transform.eulerAngles = new Vector3(0, 90f, 0);
        }
        else if (m_Direction.z > 0)
        {
            transform.eulerAngles = new Vector3(0, 180f, 0);
        }
        else if (m_Direction.z < 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }

        LeanTween.move(r_Player, r_Player.transform.position + new Vector3(m_Direction.normalized.x, 0, 0) + Vector3.up, r_Duration / 2).setEase(LeanTweenType.easeOutQuad).setOnComplete(() =>
        {
            LeanTween.move(r_Player, r_Player.transform.position + new Vector3(m_Direction.normalized.x, 0, 0) - Vector3.up, r_Duration / 2).setEase(LeanTweenType.easeOutQuad);
        });
    }
}