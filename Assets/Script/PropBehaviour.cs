using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropBehaviour : MonoBehaviour
{
    public Swipe_Controller pr_SwipeController;
    public PlayerBehaviour pr_PlayerBehaviour;
    public PropGenerator pr_PropsGenerator;



    public void OnEnable()
    {
        pr_SwipeController.OnSeMueve += MoveTarget;
    }

    public void OnDisable()
    {
        pr_SwipeController.OnSeMueve -= MoveTarget;
    }

    void MoveTarget(Vector3 m_Direction)
    {
        RaycastHit pr_Hitinfo = PlayerBehaviour.p_LastRay;

        if (pr_PlayerBehaviour.p_CanMove)
        {

            if (Physics.Raycast(pr_PlayerBehaviour.transform.position + new Vector3(0, 1f, 0), m_Direction, out pr_Hitinfo, 1f))
            {
                Debug.Log("Hit Something, Restricting Movement");

                if (m_Direction.z != 0)
                {
                    m_Direction.z = 0;
                }
            }

            if (pr_PlayerBehaviour.p_CanMove)
            {
                GameObject propObject = pr_PropsGenerator.GenerateProps();
                if (propObject != null)
                {
                    LeanTween.move(propObject, propObject.transform.position + new Vector3(0, 0, -m_Direction.normalized.z), pr_PlayerBehaviour.p_Duration).setEase(LeanTweenType.easeOutQuad);
                }
            }
        }
    }
}