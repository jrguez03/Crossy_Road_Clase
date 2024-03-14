using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terrain_Pool : MonoBehaviour
{
    [SerializeField] int d_MaxElements = 3;
    [SerializeField] GameObject t_TerrenoCreate;

    private Stack<GameObject> t_Stack;
    public static Terrain_Pool instance;

    // Start is called before the first frame update
    void Start()
    {
        SetUpPool();
    }

    void SetUpPool()
    {
        t_Stack = new Stack<GameObject>();
        GameObject terrenoCreado = null;

        for (int i = 0; i < d_MaxElements; i++)
        {
            terrenoCreado = Instantiate(t_TerrenoCreate);
            terrenoCreado.SetActive(false);
            t_Stack.Push(terrenoCreado);
        }
    }

    public GameObject ObtenerObjeto()
    {
        GameObject terreno = null;

        if (t_Stack.Count == 0)
        {
            GameObject terrenoCreado = Instantiate(t_TerrenoCreate);
            t_Stack.Push(terrenoCreado);
            return terrenoCreado;
        }
        else
        {
            terreno = t_Stack.Pop();
            terreno.SetActive(true);
        }

        return terreno;
    }

    public void DevolverObjeto(GameObject terrenoDevuelto)
    {
        t_Stack.Push(terrenoDevuelto);
        terrenoDevuelto.SetActive(false);
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
}
