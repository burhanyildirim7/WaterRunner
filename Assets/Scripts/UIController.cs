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

	// Tap to start butonuna tıklayınca çalışacak olan fonksiyon...
	public void TapToStart()
	{
		TapToStartPanel.SetActive(false);
		GameManager.instance.isContinue = true;
	}

	// Tap to restart butonuna tıkalyınca çalışacak olan fonksiyon..
	public void TapToRestart()
	{
		LoosePanel.SetActive(false);
		TapToStartPanel.SetActive(true);
	}
}
