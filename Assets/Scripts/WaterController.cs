using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterController : MonoBehaviour
{
	//public static WaterController instance;
	public float growAdd = 10f;

	//private void Awake()
	//{
	//	if (instance == null) instance = this;
	//	else Destroy(this);
	//}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("collectible"))
		{
			// collectible toplama ve water b�y�tme i�lemleri...
			float a = transform.localScale.x+growAdd;
			float b = transform.localScale.y+growAdd;
			float c = transform.localScale.z+growAdd;
			transform.localScale = new Vector3(a,b,c);
			other.GetComponent<Collider>().enabled = false;
			other.GetComponent<Renderer>().enabled = false;
		}else if (other.CompareTag("obstacle"))
		{
			//engele �arpma ve water k���ltme i�lemleri...
			float a = transform.localScale.x - growAdd;
			float b = transform.localScale.y - growAdd;
			float c = transform.localScale.z - growAdd;
			transform.localScale = new Vector3(a, b, c);
			other.GetComponent<Collider>().enabled = false;
			other.GetComponent<Renderer>().enabled = false;
			if(transform.localScale.x < 80)
			{
				// fazla k���ld��� i�in oyunu bitirme i�lemleri...
				Debug.Log("Oyun bitti can�m...");
				GameManager.instance.isContinue = false;
				UIController.instance.LoosePanel.SetActive(true);
			}
		}else if (other.CompareTag("finish"))
		{
			// e�er oyun sonuna varabildiyse oyunu bitirme i�lemleri....
			Debug.Log("Oyunu kazand�n bebi�im...");
		}
	}
}
