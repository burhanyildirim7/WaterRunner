using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaterController : MonoBehaviour
{
    //public static WaterController instance;
    [Header("Buyume Orani")]
    public float growAdd = 10f;

    [Header("Water Material Listesi")]
    [SerializeField] private List<Material> _waterMaterialList = new List<Material>();

    [Header("Water Efekt Listesi")]
    [SerializeField] private List<GameObject> _waterEfektList = new List<GameObject>();

    [Header("Toplanabilir Obje Degerleri")]
    [SerializeField] private int _temizWaterToplanabilir;
    [SerializeField] private int _kirliWaterToplanabilir;


    [Header("Karakter Canvas Objeleri")]
    [SerializeField] private Text _levelText;
    [SerializeField] private Slider _levelSlider;
    [SerializeField] private GameObject _sliderFill;
    [SerializeField] private List<Color> _sliderFillColors = new List<Color>();


    [Header("Animatorler")]
    [SerializeField] private Animator _baslangicAnimator;
    //[SerializeField] private List<Animator> _oyunSonuAnimators = new List<Animator>();


    //private int _sliderDeger;


    private int _temizDuvarToplanabilir;
    private int _kirliDuvarToplanabilir;

    public static int _temizlikSeviyesi;

    private GameObject _player;

    private GameObject _waterPaket;


    private int _oyunSonuXDegeri;




    //private void Awake()
    //{
    //	if (instance == null) instance = this;
    //	else Destroy(this);
    //}

    private void Start()
    {
        LevelStart();
    }

    private void Update()
    {
        if (_levelSlider.value <= 4)
        {
            _sliderFill.GetComponent<Image>().color = _sliderFillColors[0];
        }
        else if (_levelSlider.value > 4 && _levelSlider.value <= 8)
        {
            _sliderFill.GetComponent<Image>().color = _sliderFillColors[1];
        }
        else if (_levelSlider.value > 8 && _levelSlider.value <= 12)
        {
            _sliderFill.GetComponent<Image>().color = _sliderFillColors[2];
        }
        else if (_levelSlider.value > 12 && _levelSlider.value <= 16)
        {
            _sliderFill.GetComponent<Image>().color = _sliderFillColors[3];
        }
        else if (_levelSlider.value > 16 && _levelSlider.value <= 20)
        {
            _sliderFill.GetComponent<Image>().color = _sliderFillColors[4];
        }
        else
        {

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("collectible"))
        {
            _temizlikSeviyesi += _temizWaterToplanabilir;
            _levelSlider.value = _temizlikSeviyesi % 20;

            // collectible toplama ve water b?y?tme i?lemleri...
            if (_temizlikSeviyesi > 100)
            {
                _temizlikSeviyesi = 100;
                _levelSlider.value = 20;
            }
            else
            {

            }

            _levelText.text = "%" + _temizlikSeviyesi.ToString() + " CLEAN";
            float a = transform.localScale.x + growAdd;
            float b = transform.localScale.y + growAdd;
            float c = transform.localScale.z + growAdd;
            transform.localScale = new Vector3(a, b, c);
            Destroy(other.gameObject);

            if (_temizlikSeviyesi < 20)
            {
                GetComponent<Renderer>().material = _waterMaterialList[4];
                _waterEfektList[0].SetActive(false);
                _waterEfektList[1].SetActive(false);
                _waterEfektList[2].SetActive(false);
                _waterEfektList[3].SetActive(false);
                _waterEfektList[4].SetActive(true);
            }
            else if (_temizlikSeviyesi >= 20 && _temizlikSeviyesi < 40)
            {
                GetComponent<Renderer>().material = _waterMaterialList[3];
                _waterEfektList[0].SetActive(false);
                _waterEfektList[1].SetActive(false);
                _waterEfektList[2].SetActive(false);
                _waterEfektList[4].SetActive(false);
                _waterEfektList[3].SetActive(true);
            }
            else if (_temizlikSeviyesi >= 40 && _temizlikSeviyesi < 60)
            {
                GetComponent<Renderer>().material = _waterMaterialList[2];
                _waterEfektList[0].SetActive(false);
                _waterEfektList[1].SetActive(false);
                _waterEfektList[3].SetActive(false);
                _waterEfektList[4].SetActive(false);
                _waterEfektList[2].SetActive(true);
            }
            else if (_temizlikSeviyesi >= 60 && _temizlikSeviyesi < 80)
            {
                GetComponent<Renderer>().material = _waterMaterialList[1];
                _waterEfektList[0].SetActive(false);
                _waterEfektList[2].SetActive(false);
                _waterEfektList[3].SetActive(false);
                _waterEfektList[4].SetActive(false);
                _waterEfektList[1].SetActive(true);
            }
            else if (_temizlikSeviyesi >= 80)
            {
                GetComponent<Renderer>().material = _waterMaterialList[0];
                _waterEfektList[1].SetActive(false);
                _waterEfektList[2].SetActive(false);
                _waterEfektList[3].SetActive(false);
                _waterEfektList[4].SetActive(false);
                _waterEfektList[0].SetActive(true);
            }
            else
            {

            }




        }
        else if (other.CompareTag("obstacle"))
        {
            //engele ?arpma ve water k???ltme i?lemleri...
            _temizlikSeviyesi -= _kirliWaterToplanabilir;
            _levelSlider.value = _temizlikSeviyesi % 20;

            _levelText.text = "%" + _temizlikSeviyesi.ToString() + " CLEAN";
            float a = transform.localScale.x + growAdd;
            float b = transform.localScale.y + growAdd;
            float c = transform.localScale.z + growAdd;
            transform.localScale = new Vector3(a, b, c);
            Destroy(other.gameObject);

            if (_temizlikSeviyesi < 0 && GameManager.instance._finishCizgisiAktif == false)
            {
                // fazla k???ld??? i?in oyunu bitirme i?lemleri...
                Debug.Log("Oyun bitti can?m...");

                _temizlikSeviyesi = 0;
                _levelSlider.value = 0;
                _levelText.text = "%" + _temizlikSeviyesi.ToString() + " CLEAN";
                GameManager.instance._oyunAktif = false;

                //OyunSonuElmasHesaplama();
                UIController.instance.LoseScreenPanelOpen();
            }

            if (_temizlikSeviyesi < 20)
            {
                GetComponent<Renderer>().material = _waterMaterialList[4];
                _waterEfektList[0].SetActive(false);
                _waterEfektList[1].SetActive(false);
                _waterEfektList[2].SetActive(false);
                _waterEfektList[3].SetActive(false);
                _waterEfektList[4].SetActive(true);
            }
            else if (_temizlikSeviyesi >= 20 && _temizlikSeviyesi < 40)
            {
                GetComponent<Renderer>().material = _waterMaterialList[3];
                _waterEfektList[0].SetActive(false);
                _waterEfektList[1].SetActive(false);
                _waterEfektList[2].SetActive(false);
                _waterEfektList[4].SetActive(false);
                _waterEfektList[3].SetActive(true);
            }
            else if (_temizlikSeviyesi >= 40 && _temizlikSeviyesi < 60)
            {
                GetComponent<Renderer>().material = _waterMaterialList[2];
                _waterEfektList[0].SetActive(false);
                _waterEfektList[1].SetActive(false);
                _waterEfektList[3].SetActive(false);
                _waterEfektList[4].SetActive(false);
                _waterEfektList[2].SetActive(true);
            }
            else if (_temizlikSeviyesi >= 60 && _temizlikSeviyesi < 80)
            {
                GetComponent<Renderer>().material = _waterMaterialList[1];
                _waterEfektList[0].SetActive(false);
                _waterEfektList[2].SetActive(false);
                _waterEfektList[3].SetActive(false);
                _waterEfektList[4].SetActive(false);
                _waterEfektList[1].SetActive(true);
            }
            else if (_temizlikSeviyesi >= 80)
            {
                GetComponent<Renderer>().material = _waterMaterialList[0];
                _waterEfektList[1].SetActive(false);
                _waterEfektList[2].SetActive(false);
                _waterEfektList[3].SetActive(false);
                _waterEfektList[4].SetActive(false);
                _waterEfektList[0].SetActive(true);
            }
            else
            {

            }

        }
        else if (other.CompareTag("temizduvar"))
        {

            //engele ?arpma ve water k???ltme i?lemleri...
            _temizDuvarToplanabilir = other.GetComponent<DuvarScript>()._duvarDegeri;
            _temizlikSeviyesi += _temizDuvarToplanabilir;
            _levelSlider.value = _temizlikSeviyesi % 20;

            if (_temizlikSeviyesi > 100)
            {
                _temizlikSeviyesi = 100;
                _levelSlider.value = 20;
            }
            else
            {

            }

            _levelText.text = "%" + _temizlikSeviyesi.ToString() + " CLEAN";
            float a = transform.localScale.x + (growAdd * 3);
            float b = transform.localScale.y + (growAdd * 3);
            float c = transform.localScale.z + (growAdd * 3);
            transform.localScale = new Vector3(a, b, c);
            Destroy(other.gameObject);

            if (_temizlikSeviyesi < 20)
            {
                GetComponent<Renderer>().material = _waterMaterialList[4];
                _waterEfektList[0].SetActive(false);
                _waterEfektList[1].SetActive(false);
                _waterEfektList[2].SetActive(false);
                _waterEfektList[3].SetActive(false);
                _waterEfektList[4].SetActive(true);
            }
            else if (_temizlikSeviyesi >= 20 && _temizlikSeviyesi < 40)
            {
                GetComponent<Renderer>().material = _waterMaterialList[3];
                _waterEfektList[0].SetActive(false);
                _waterEfektList[1].SetActive(false);
                _waterEfektList[2].SetActive(false);
                _waterEfektList[4].SetActive(false);
                _waterEfektList[3].SetActive(true);
            }
            else if (_temizlikSeviyesi >= 40 && _temizlikSeviyesi < 60)
            {
                GetComponent<Renderer>().material = _waterMaterialList[2];
                _waterEfektList[0].SetActive(false);
                _waterEfektList[1].SetActive(false);
                _waterEfektList[3].SetActive(false);
                _waterEfektList[4].SetActive(false);
                _waterEfektList[2].SetActive(true);
            }
            else if (_temizlikSeviyesi >= 60 && _temizlikSeviyesi < 80)
            {
                GetComponent<Renderer>().material = _waterMaterialList[1];
                _waterEfektList[0].SetActive(false);
                _waterEfektList[2].SetActive(false);
                _waterEfektList[3].SetActive(false);
                _waterEfektList[4].SetActive(false);
                _waterEfektList[1].SetActive(true);
            }
            else if (_temizlikSeviyesi >= 80)
            {
                GetComponent<Renderer>().material = _waterMaterialList[0];
                _waterEfektList[1].SetActive(false);
                _waterEfektList[2].SetActive(false);
                _waterEfektList[3].SetActive(false);
                _waterEfektList[4].SetActive(false);
                _waterEfektList[0].SetActive(true);
            }
            else
            {

            }


        }
        else if (other.CompareTag("kirliduvar"))
        {
            //engele ?arpma ve water k???ltme i?lemleri...
            _kirliDuvarToplanabilir = other.GetComponent<DuvarScript>()._duvarDegeri;
            _temizlikSeviyesi -= _kirliDuvarToplanabilir;
            _levelSlider.value = _temizlikSeviyesi % 20;

            _levelText.text = "%" + _temizlikSeviyesi.ToString() + " CLEAN";
            float a = transform.localScale.x + (growAdd * 3);
            float b = transform.localScale.y + (growAdd * 3);
            float c = transform.localScale.z + (growAdd * 3);
            transform.localScale = new Vector3(a, b, c);
            Destroy(other.gameObject);

            if (_temizlikSeviyesi < 0 && GameManager.instance._finishCizgisiAktif == false)
            {
                // fazla k???ld??? i?in oyunu bitirme i?lemleri...
                Debug.Log("Oyun bitti can?m...");

                _temizlikSeviyesi = 0;
                _levelSlider.value = 0;
                _levelText.text = "%" + _temizlikSeviyesi.ToString() + " CLEAN";
                GameManager.instance._oyunAktif = false;

                //OyunSonuElmasHesaplama();
                UIController.instance.LoseScreenPanelOpen();
            }

            if (_temizlikSeviyesi < 20)
            {
                GetComponent<Renderer>().material = _waterMaterialList[4];
                _waterEfektList[0].SetActive(false);
                _waterEfektList[1].SetActive(false);
                _waterEfektList[2].SetActive(false);
                _waterEfektList[3].SetActive(false);
                _waterEfektList[4].SetActive(true);
            }
            else if (_temizlikSeviyesi >= 20 && _temizlikSeviyesi < 40)
            {
                GetComponent<Renderer>().material = _waterMaterialList[3];
                _waterEfektList[0].SetActive(false);
                _waterEfektList[1].SetActive(false);
                _waterEfektList[2].SetActive(false);
                _waterEfektList[4].SetActive(true);
                _waterEfektList[3].SetActive(true);
            }
            else if (_temizlikSeviyesi >= 40 && _temizlikSeviyesi < 60)
            {
                GetComponent<Renderer>().material = _waterMaterialList[2];
                _waterEfektList[0].SetActive(false);
                _waterEfektList[1].SetActive(false);
                _waterEfektList[3].SetActive(false);
                _waterEfektList[4].SetActive(false);
                _waterEfektList[6].SetActive(true);
                _waterEfektList[2].SetActive(true);
            }
            else if (_temizlikSeviyesi >= 60 && _temizlikSeviyesi < 80)
            {
                GetComponent<Renderer>().material = _waterMaterialList[1];
                _waterEfektList[0].SetActive(false);
                _waterEfektList[2].SetActive(false);
                _waterEfektList[3].SetActive(false);
                _waterEfektList[4].SetActive(false);
                _waterEfektList[6].SetActive(true);
                _waterEfektList[1].SetActive(true);
            }
            else if (_temizlikSeviyesi >= 80)
            {
                GetComponent<Renderer>().material = _waterMaterialList[0];
                _waterEfektList[1].SetActive(false);
                _waterEfektList[2].SetActive(false);
                _waterEfektList[3].SetActive(false);
                _waterEfektList[4].SetActive(false);
                _waterEfektList[6].SetActive(true);
                _waterEfektList[0].SetActive(true);
            }
            else
            {

            }


        }
        else if (other.CompareTag("X1"))
        {
            //float a = transform.localScale.x - (growAdd * 5);
            //float b = transform.localScale.y - (growAdd * 5);
            //float c = transform.localScale.z - (growAdd * 5);
            //transform.localScale = new Vector3(a, b, c);

            GameObject humanx = GameObject.FindGameObjectWithTag("HumanX1");

            if (_temizlikSeviyesi <= 40)
            {
                for (int i = 0; i < humanx.transform.childCount; i++)
                {
                    humanx.gameObject.transform.GetChild(i).gameObject.GetComponent<Animator>().SetBool("isDefeat", true);
                }
            }
            else
            {
                for (int i = 0; i < humanx.transform.childCount; i++)
                {
                    humanx.gameObject.transform.GetChild(i).gameObject.GetComponent<Animator>().SetBool("isVictory", true);

                }
            }

            if (transform.localScale.x <= 50)
            {
                // fazla k???ld??? i?in oyunu bitirme i?lemleri...
                Debug.Log("Oyun bitti can?m...");

                //GameManager.instance._oyunAktif = false;
                //UIController.instance.WinScreenPanelOpen();

                StartCoroutine(LevelWin());
            }
            else
            {
                StartCoroutine(OyunSonuAnimator(1));
            }

            _oyunSonuXDegeri = 1;
        }
        else if (other.CompareTag("X2"))
        {
            //float a = transform.localScale.x - (growAdd * 5);
            //float b = transform.localScale.y - (growAdd * 5);
            //float c = transform.localScale.z - (growAdd * 5);
            //transform.localScale = new Vector3(a, b, c);

            GameObject humanx = GameObject.FindGameObjectWithTag("HumanX2");

            if (_temizlikSeviyesi <= 40)
            {
                for (int i = 0; i < humanx.transform.childCount; i++)
                {
                    humanx.gameObject.transform.GetChild(i).gameObject.GetComponent<Animator>().SetBool("isDefeat", true);
                }
            }
            else
            {
                for (int i = 0; i < humanx.transform.childCount; i++)
                {
                    humanx.gameObject.transform.GetChild(i).gameObject.GetComponent<Animator>().SetBool("isVictory", true);

                }
            }

            if (transform.localScale.x <= 50)
            {
                // fazla k???ld??? i?in oyunu bitirme i?lemleri...
                Debug.Log("Oyun bitti can?m...");

                //GameManager.instance._oyunAktif = false;
                //UIController.instance.WinScreenPanelOpen();

                StartCoroutine(LevelWin());
            }
            else
            {
                StartCoroutine(OyunSonuAnimator(2));
            }

            _oyunSonuXDegeri = 2;
        }
        else if (other.CompareTag("X3"))
        {
            //float a = transform.localScale.x - (growAdd * 5);
            //float b = transform.localScale.y - (growAdd * 5);
            //float c = transform.localScale.z - (growAdd * 5);
            //transform.localScale = new Vector3(a, b, c);

            GameObject humanx = GameObject.FindGameObjectWithTag("HumanX3");

            if (_temizlikSeviyesi <= 40)
            {
                for (int i = 0; i < humanx.transform.childCount; i++)
                {
                    humanx.gameObject.transform.GetChild(i).gameObject.GetComponent<Animator>().SetBool("isDefeat", true);
                }
            }
            else
            {
                for (int i = 0; i < humanx.transform.childCount; i++)
                {
                    humanx.gameObject.transform.GetChild(i).gameObject.GetComponent<Animator>().SetBool("isVictory", true);

                }
            }

            if (transform.localScale.x <= 50)
            {
                // fazla k???ld??? i?in oyunu bitirme i?lemleri...
                Debug.Log("Oyun bitti can?m...");

                //GameManager.instance._oyunAktif = false;
                //UIController.instance.WinScreenPanelOpen();

                StartCoroutine(LevelWin());
            }
            else
            {
                StartCoroutine(OyunSonuAnimator(3));
            }

            _oyunSonuXDegeri = 3;
        }
        else if (other.CompareTag("X4"))
        {
            //float a = transform.localScale.x - (growAdd * 5);
            //float b = transform.localScale.y - (growAdd * 5);
            //float c = transform.localScale.z - (growAdd * 5);
            //transform.localScale = new Vector3(a, b, c);

            GameObject humanx = GameObject.FindGameObjectWithTag("HumanX4");

            if (_temizlikSeviyesi <= 40)
            {
                for (int i = 0; i < humanx.transform.childCount; i++)
                {
                    humanx.gameObject.transform.GetChild(i).gameObject.GetComponent<Animator>().SetBool("isDefeat", true);
                }
            }
            else
            {
                for (int i = 0; i < humanx.transform.childCount; i++)
                {
                    humanx.gameObject.transform.GetChild(i).gameObject.GetComponent<Animator>().SetBool("isVictory", true);

                }
            }

            if (transform.localScale.x <= 50)
            {
                // fazla k???ld??? i?in oyunu bitirme i?lemleri...
                Debug.Log("Oyun bitti can?m...");

                //GameManager.instance._oyunAktif = false;
                //UIController.instance.WinScreenPanelOpen();

                StartCoroutine(LevelWin());
            }
            else
            {
                StartCoroutine(OyunSonuAnimator(4));
            }

            _oyunSonuXDegeri = 4;
        }
        else if (other.CompareTag("X5"))
        {
            //float a = transform.localScale.x - (growAdd * 5);
            //float b = transform.localScale.y - (growAdd * 5);
            //float c = transform.localScale.z - (growAdd * 5);
            //transform.localScale = new Vector3(a, b, c);

            GameObject humanx = GameObject.FindGameObjectWithTag("HumanX5");

            if (_temizlikSeviyesi <= 40)
            {
                for (int i = 0; i < humanx.transform.childCount; i++)
                {
                    humanx.gameObject.transform.GetChild(i).gameObject.GetComponent<Animator>().SetBool("isDefeat", true);
                }
            }
            else
            {
                for (int i = 0; i < humanx.transform.childCount; i++)
                {
                    humanx.gameObject.transform.GetChild(i).gameObject.GetComponent<Animator>().SetBool("isVictory", true);

                }
            }

            if (transform.localScale.x <= 50)
            {
                // fazla k???ld??? i?in oyunu bitirme i?lemleri...
                Debug.Log("Oyun bitti can?m...");

                //GameManager.instance._oyunAktif = false;
                //UIController.instance.WinScreenPanelOpen();

                StartCoroutine(LevelWin());
            }
            else
            {
                StartCoroutine(OyunSonuAnimator(5));
            }

            _oyunSonuXDegeri = 5;
        }
        else if (other.CompareTag("X6"))
        {
            //float a = transform.localScale.x - (growAdd * 5);
            //float b = transform.localScale.y - (growAdd * 5);
            //float c = transform.localScale.z - (growAdd * 5);
            //transform.localScale = new Vector3(a, b, c);

            GameObject humanx = GameObject.FindGameObjectWithTag("HumanX6");

            if (_temizlikSeviyesi <= 40)
            {
                for (int i = 0; i < humanx.transform.childCount; i++)
                {
                    humanx.gameObject.transform.GetChild(i).gameObject.GetComponent<Animator>().SetBool("isDefeat", true);
                }
            }
            else
            {
                for (int i = 0; i < humanx.transform.childCount; i++)
                {
                    humanx.gameObject.transform.GetChild(i).gameObject.GetComponent<Animator>().SetBool("isVictory", true);

                }
            }

            if (transform.localScale.x <= 50)
            {
                // fazla k???ld??? i?in oyunu bitirme i?lemleri...
                Debug.Log("Oyun bitti can?m...");

                //GameManager.instance._oyunAktif = false;
                //UIController.instance.WinScreenPanelOpen();

                StartCoroutine(LevelWin());
            }
            else
            {
                StartCoroutine(OyunSonuAnimator(6));
            }

            _oyunSonuXDegeri = 6;
        }
        else if (other.CompareTag("X7"))
        {
            //float a = transform.localScale.x - (growAdd * 5);
            //float b = transform.localScale.y - (growAdd * 5);
            //float c = transform.localScale.z - (growAdd * 5);
            //transform.localScale = new Vector3(a, b, c);

            GameObject humanx = GameObject.FindGameObjectWithTag("HumanX7");

            if (_temizlikSeviyesi <= 40)
            {
                for (int i = 0; i < humanx.transform.childCount; i++)
                {
                    humanx.gameObject.transform.GetChild(i).gameObject.GetComponent<Animator>().SetBool("isDefeat", true);
                }
            }
            else
            {
                for (int i = 0; i < humanx.transform.childCount; i++)
                {
                    humanx.gameObject.transform.GetChild(i).gameObject.GetComponent<Animator>().SetBool("isVictory", true);

                }
            }

            if (transform.localScale.x <= 50)
            {
                // fazla k???ld??? i?in oyunu bitirme i?lemleri...
                Debug.Log("Oyun bitti can?m...");

                //GameManager.instance._oyunAktif = false;
                //UIController.instance.WinScreenPanelOpen();

                StartCoroutine(LevelWin());
            }
            else
            {
                StartCoroutine(OyunSonuAnimator(7));
            }

            _oyunSonuXDegeri = 7;
        }
        else if (other.CompareTag("X8"))
        {
            //float a = transform.localScale.x - (growAdd * 5);
            //float b = transform.localScale.y - (growAdd * 5);
            //float c = transform.localScale.z - (growAdd * 5);
            //transform.localScale = new Vector3(a, b, c);

            GameObject humanx = GameObject.FindGameObjectWithTag("HumanX8");

            if (_temizlikSeviyesi <= 40)
            {
                for (int i = 0; i < humanx.transform.childCount; i++)
                {
                    humanx.gameObject.transform.GetChild(i).gameObject.GetComponent<Animator>().SetBool("isDefeat", true);
                }
            }
            else
            {
                for (int i = 0; i < humanx.transform.childCount; i++)
                {
                    humanx.gameObject.transform.GetChild(i).gameObject.GetComponent<Animator>().SetBool("isVictory", true);

                }
            }

            if (transform.localScale.x <= 50)
            {
                // fazla k???ld??? i?in oyunu bitirme i?lemleri...
                Debug.Log("Oyun bitti can?m...");

                //GameManager.instance._oyunAktif = false;
                //UIController.instance.WinScreenPanelOpen();

                StartCoroutine(LevelWin());
            }
            else
            {
                StartCoroutine(OyunSonuAnimator(8));
            }

            _oyunSonuXDegeri = 8;
        }
        else if (other.CompareTag("X9"))
        {
            //float a = transform.localScale.x - (growAdd * 5);
            //float b = transform.localScale.y - (growAdd * 5);
            //float c = transform.localScale.z - (growAdd * 5);
            //transform.localScale = new Vector3(a, b, c);

            GameObject humanx = GameObject.FindGameObjectWithTag("HumanX9");

            if (_temizlikSeviyesi <= 40)
            {
                for (int i = 0; i < humanx.transform.childCount; i++)
                {
                    humanx.gameObject.transform.GetChild(i).gameObject.GetComponent<Animator>().SetBool("isDefeat", true);
                }
            }
            else
            {
                for (int i = 0; i < humanx.transform.childCount; i++)
                {
                    humanx.gameObject.transform.GetChild(i).gameObject.GetComponent<Animator>().SetBool("isVictory", true);

                }
            }

            if (transform.localScale.x <= 50)
            {
                // fazla k???ld??? i?in oyunu bitirme i?lemleri...
                Debug.Log("Oyun bitti can?m...");

                //GameManager.instance._oyunAktif = false;
                //UIController.instance.WinScreenPanelOpen();

                StartCoroutine(LevelWin());
            }
            else
            {
                StartCoroutine(OyunSonuAnimator(9));
            }

            _oyunSonuXDegeri = 9;
        }
        else if (other.CompareTag("X10"))
        {
            //float a = transform.localScale.x - (growAdd * 5);
            //float b = transform.localScale.y - (growAdd * 5);
            //float c = transform.localScale.z - (growAdd * 5);
            //transform.localScale = new Vector3(a, b, c);

            GameObject humanx = GameObject.FindGameObjectWithTag("HumanX10");

            if (_temizlikSeviyesi <= 40)
            {
                for (int i = 0; i < humanx.transform.childCount; i++)
                {
                    humanx.gameObject.transform.GetChild(i).gameObject.GetComponent<Animator>().SetBool("isDefeat", true);
                }
            }
            else
            {
                for (int i = 0; i < humanx.transform.childCount; i++)
                {
                    humanx.gameObject.transform.GetChild(i).gameObject.GetComponent<Animator>().SetBool("isVictory", true);

                }
            }

            if (transform.localScale.x <= 50)
            {
                // fazla k???ld??? i?in oyunu bitirme i?lemleri...
                Debug.Log("Oyun bitti can?m...");

                //GameManager.instance._oyunAktif = false;
                //UIController.instance.WinScreenPanelOpen();

                StartCoroutine(LevelWin());
            }
            else
            {
                StartCoroutine(OyunSonuAnimator(10));
            }

            _oyunSonuXDegeri = 10;
        }
        else if (other.gameObject.tag == "FinishCizgisi")
        {
            _waterPaket.transform.localPosition = new Vector3(0, 0, 0);
            GameManager.instance._finishCizgisiAktif = true;
            _waterEfektList[1].SetActive(false);
            _waterEfektList[2].SetActive(false);
            _waterEfektList[3].SetActive(false);
            _waterEfektList[4].SetActive(false);
            _waterEfektList[0].SetActive(false);
            _waterEfektList[5].SetActive(true);


        }
        else if (other.gameObject.tag == "BitirmeCizgisi")
        {
            GameManager.instance._oyunAktif = false;
            transform.localScale = new Vector3(50, 50, 50);
            UIController.instance.WinScreenPanelOpen();

            _waterEfektList[1].SetActive(false);
            _waterEfektList[2].SetActive(false);
            _waterEfektList[3].SetActive(false);
            _waterEfektList[4].SetActive(false);
            _waterEfektList[0].SetActive(false);
            _waterEfektList[5].SetActive(false);
            _waterEfektList[6].SetActive(false);
            _waterEfektList[6].SetActive(false);


        }
        else
        {

        }


    }

    private void OyunSonuElmasHesaplama()
    {
        int _elmasDeger = 0;

        _elmasDeger = (_oyunSonuXDegeri * _temizlikSeviyesi);

        UIController.instance.LevelSonuElmasSayisi(_elmasDeger);
    }

    private IEnumerator LevelWin()
    {
        yield return new WaitForSeconds(0.5f);

        _waterEfektList[1].SetActive(false);
        _waterEfektList[2].SetActive(false);
        _waterEfektList[3].SetActive(false);
        _waterEfektList[4].SetActive(false);
        _waterEfektList[0].SetActive(false);
        _waterEfektList[5].SetActive(false);
        _waterEfektList[6].SetActive(false);

        OyunSonuElmasHesaplama();

        GameManager.instance._oyunAktif = false;
        UIController.instance.WinScreenPanelOpen();
    }

    private IEnumerator OyunSonuAnimator(int deger)
    {
        yield return new WaitForSeconds(0.5f);

        GameManager.instance._oyunuBeklet = true;

        float a = transform.localScale.x - (growAdd * 5);
        float b = transform.localScale.y - (growAdd * 5);
        float c = transform.localScale.z - (growAdd * 5);
        transform.localScale = new Vector3(a, b, c);

        //_oyunSonuAnimators[deger].SetBool("isOpen", true);
        StartCoroutine(OyunaDevamEt());
    }

    private IEnumerator OyunaDevamEt()
    {
        yield return new WaitForSeconds(1f);

        GameManager.instance._oyunuBeklet = false;
        //_oyunSonuAnimators[deger].SetBool("isOpen", true);
    }

    public void LevelStart()
    {
        GameManager.instance._finishCizgisiAktif = false;
        GameManager.instance._oyunuBeklet = false;
        _player = GameObject.FindGameObjectWithTag("Player");
        _waterPaket = GameObject.FindGameObjectWithTag("WaterPaket");
        _player.transform.position = new Vector3(0, 2, 0);
        _waterPaket.transform.localPosition = new Vector3(0, 0, 0);
        _temizlikSeviyesi = 0;
        _oyunSonuXDegeri = 0;
        _levelSlider.value = 0;
        _levelText.text = "%" + _temizlikSeviyesi.ToString() + " CLEAN";
        GetComponent<Renderer>().material = _waterMaterialList[4];
        _waterEfektList[0].SetActive(false);
        _waterEfektList[1].SetActive(false);
        _waterEfektList[2].SetActive(false);
        _waterEfektList[3].SetActive(false);
        _waterEfektList[5].SetActive(false);
        _waterEfektList[6].SetActive(false);
        _waterEfektList[4].SetActive(true);


    }
}
