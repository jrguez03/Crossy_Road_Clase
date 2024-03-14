using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_Subscriber : MonoBehaviour
{
    public float s_Duration = 0.25f;

    [SerializeField] GameObject s_Player;

    private void Awake()
    {
        s_Player = this.gameObject;
    }
    public void Start()
    {
        //Suscribirse al evento.
        Swipe_Controller.instance.OnSeMueve += MoveTarget;
    }

    public void OnDisable()
    {
        //Eliminar suscripción.
        Swipe_Controller.instance.OnSeMueve -= MoveTarget;
    }
   
    void MoveTarget(Vector3 direction)
    {
        LeanTween.move(s_Player, s_Player.transform.position + direction / 2 + Vector3.up / 2, s_Duration / 2).setEase(LeanTweenType.easeOutQuad).setOnComplete(() =>
        {
            LeanTween.move(s_Player, s_Player.transform.position + direction / 2 - Vector3.up / 2, s_Duration / 2).setEase(LeanTweenType.easeOutQuad);
        });
    }
}
