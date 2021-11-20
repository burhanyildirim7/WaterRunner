using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public bool _oyunAktif = false;

    public bool _oyunuBeklet = false;

    public bool _finishCizgisiAktif;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this);
    }




}
