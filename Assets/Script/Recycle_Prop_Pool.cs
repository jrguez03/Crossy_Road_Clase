using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recycle_Prop_Pool : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Prop"))
        {
            other.gameObject.SetActive(false);
        }
    }
}
