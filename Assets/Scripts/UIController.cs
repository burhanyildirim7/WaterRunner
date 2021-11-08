using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public static UIController instance;

	public GameObject TapToStartPanel,LoosePanel;

	private void Awake()
	{
		if (instance == null) instance = this;
		else Destroy(this);
	}

	// Tap to start butonuna t�klay�nca �al��acak olan fonksiyon...
	public void TapToStart()
	{
		TapToStartPanel.SetActive(false);
		GameManager.instance.isContinue = true;
	}

	// Tap to restart butonuna t�kaly�nca �al��acak olan fonksiyon..
	public void TapToRestart()
	{
		LoosePanel.SetActive(false);
		TapToStartPanel.SetActive(true);
	}
}
