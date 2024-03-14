using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swipe_Controller : MonoBehaviour
{
    Vector3 s_ClickInicial;
    Vector3 s_AlSoltarClick;

    public float s_LimitMove = 100f;

    public static Swipe_Controller instance;

    bool s_Salta;

    //declarar delegate y events para los movimientos
    public delegate void SeMueve(Vector3 diferencia);
    public event SeMueve OnSeMueve;

    private void Awake()
    {
        if (Swipe_Controller.instance != null)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            s_ClickInicial = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            s_AlSoltarClick = Input.mousePosition;

            Vector3 diferencia = s_AlSoltarClick - s_ClickInicial;

            if (Mathf.Abs(diferencia.magnitude) > s_LimitMove)
            {
                diferencia = diferencia.normalized;
                diferencia.z = diferencia.y;

                if (Mathf.Abs(diferencia.x) > Mathf.Abs(diferencia.z))
                {
                    diferencia.z = 0.0f;
                }
                else
                {
                    diferencia.x = 0.0f;
                }

                diferencia.y = 0.0f;

                if (OnSeMueve != null)
                {
                    OnSeMueve(diferencia);
                }
            }

        }
    }
}
