using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OyunSonuKovaScript : MonoBehaviour
{

    void Start()
    {
        GetComponent<Animator>().enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Water")
        {
            GetComponent<Animator>().enabled = true;
            Debug.Log("Kova Animasyonu Calisti");
        }
    }
}
