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
			// collectible toplama ve water büyütme iþlemleri...
			float a = transform.localScale.x+growAdd;
			float b = transform.localScale.y+growAdd;
			float c = transform.localScale.z+growAdd;
			transform.localScale = new Vector3(a,b,c);
			other.GetComponent<Collider>().enabled = false;
			other.GetComponent<Renderer>().enabled = false;
		}else if (other.CompareTag("obstacle"))
		{
			//engele çarpma ve water küçültme iþlemleri...
			float a = transform.localScale.x - growAdd;
			float b = transform.localScale.y - growAdd;
			float c = transform.localScale.z - growAdd;
			transform.localScale = new Vector3(a, b, c);
			other.GetComponent<Collider>().enabled = false;
			other.GetComponent<Renderer>().enabled = false;
			if(transform.localScale.x < 80)
			{
				// fazla küçüldüðü için oyunu bitirme iþlemleri...
				Debug.Log("Oyun bitti caným...");
				GameManager.instance.isContinue = false;
				UIController.instance.LoosePanel.SetActive(true);
			}
		}else if (other.CompareTag("finish"))
		{
			// eðer oyun sonuna varabildiyse oyunu bitirme iþlemleri....
			Debug.Log("Oyunu kazandýn bebiþim...");
		}
	}
}
