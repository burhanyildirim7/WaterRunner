using UnityEngine;

public class SwerveMovement : MonoBehaviour
{
    private SwerveInputSystem _swerveInputSystem;
    [SerializeField] private float swerveSpeed = 0.5f;
    [SerializeField] private float playerZSpeed = 2f;
    [SerializeField] private GameObject water;
    [SerializeField] private float _radius;

    Vector3 centerPosition;
    //[SerializeField] private float maxSwerveAmount = 1f;

    private void Awake()
    {
        _swerveInputSystem = GetComponent<SwerveInputSystem>();
    }

    private void Update()
    {
		if (GameManager.instance.isContinue)
		{
            centerPosition = transform.position;
            float swerveAmount = Time.deltaTime * swerveSpeed * _swerveInputSystem.MoveFactorX;
            water.transform.Translate(swerveAmount, 0, 0);
            transform.Translate(Vector3.forward * Time.deltaTime * playerZSpeed);
            float distance = Vector3.Distance(water.transform.position, centerPosition);
            if (distance > _radius)
            {
                Vector3 fromOriginToObject = water.transform.position - centerPosition;
                fromOriginToObject *= _radius / distance;
                water.transform.position = centerPosition + fromOriginToObject;
            }
        }   
    }
}
