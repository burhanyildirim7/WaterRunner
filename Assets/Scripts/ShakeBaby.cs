using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeBaby : MonoBehaviour
{
    private SkinnedMeshRenderer skrenderer;
    private float[] lastValues;
    private float[] newValues;
    private float t = 0;
    [SerializeField] private float shakingSpeed = 0.2f;
    [Range(0,200)]
    [SerializeField] private float shakingRange = 200f;

    void Start()
    {
        skrenderer = GetComponent<SkinnedMeshRenderer>();
        lastValues = new float[6];
        newValues = new float[6];
        RandomFirstValues();
        RandomNewValues();
       
    }

    void FixedUpdate()
    {
        Shake();
    }

	public void Shake()
	{
        for (int i = 0; i < lastValues.Length; i++)
        {
            skrenderer.SetBlendShapeWeight(i, Mathf.Lerp(lastValues[i], newValues[i], t));
        }
        t += shakingSpeed * Time.deltaTime;
        if (Mathf.Abs(skrenderer.GetBlendShapeWeight(1) - newValues[1]) == 0f)
        {
            t = 0;
            for (int i = 0; i < lastValues.Length; i++)
            {
                lastValues[i] = newValues[i];
            }
            RandomNewValues();
        }
    }


    public void RandomFirstValues()
	{
		for (int i = 0; i < lastValues.Length; i++)
		{
            lastValues[i] = Random.Range(-shakingRange,shakingRange);
            skrenderer.SetBlendShapeWeight(i,lastValues[i]);
		}
	}

    public void RandomNewValues()
	{
        for (int i = 0; i < lastValues.Length; i++)
        {
            newValues[i] = Random.Range(-shakingRange, shakingRange);
        }
    }
}
