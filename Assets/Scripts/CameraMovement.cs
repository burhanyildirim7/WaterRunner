using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    private GameObject Player;

    Vector3 aradakiFark;

    //private bool _baslangicBitti = false;


    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Water");
        aradakiFark = transform.position - Player.transform.position;
    }


    void Update()
    {

        if (GameManager.instance._oyunAktif == true && GameManager.instance._oyunBaslangic == true)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(Player.transform.position.x, Player.transform.position.y + aradakiFark.y, Player.transform.position.z + aradakiFark.z), Time.deltaTime * 5f);
        }
        else if (GameManager.instance._suDamladi == true && GameManager.instance._cameraDondu == false && GameManager.instance._oyunBaslangic == false)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(0, 6, -12), Time.deltaTime * 200f);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(16.699f, 0, 0), 200f * Time.deltaTime);
            //Invoke("CameraOyunBaslangicKonumu", 0.5f);
            CameraOyunBaslangicKonumu();
            //_baslangicBitti = true;
        }



    }

    private void CameraOyunBaslangicKonumu()
    {
        aradakiFark = transform.position - Player.transform.position;
    }

    public void CameraResetle()
    {
        transform.position = new Vector3(9.22f, 9.44f, -11.1f);
        transform.rotation = Quaternion.Euler(23.6f, -43.7f, 0);
    }

}
