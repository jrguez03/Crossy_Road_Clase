using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swipe_Controller : MonoBehaviour
{
    Vector3 s_ClickInicial;
    Vector3 s_AlSoltarClick;

    float s_LimitMove;

    [SerializeField] GameObject s_Cubo;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            s_ClickInicial = Input.mousePosition;
            //Debug.Log("Pressed left click");
        }

        if (Input.GetMouseButtonUp(0))
        {
            s_AlSoltarClick = Input.mousePosition;
            Vector3 diferencia = s_AlSoltarClick - s_ClickInicial;
            //Debug.Log(diferencia);
            if ((diferencia.x) < 0)
            {
                MoveTarget(- s_Cubo.GetComponent<Transform>().right);
                Debug.Log("Ha movido a la izq");
            }
            if (diferencia.x > 0)
            {
                MoveTarget(- s_Cubo.GetComponent<Transform>().right);
                Debug.Log("Ha movido a la derch");
            }
            if (diferencia.y < 0)
            {
                Debug.Log("Ha movido hacia abajo");
            }
            if (diferencia.y > 0)
            {
                Debug.Log("Ha movido hacia arriba");
            }
        }
    }

    void MoveTarget(Vector3 direction)
    {
        s_Cubo.transform.position += direction;
    }
}
