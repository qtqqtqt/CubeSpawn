using UnityEngine;

public class CubeMover : MonoBehaviour
{
    float speed = 10f;
    public float Speed { set { speed = value; } }
    float distanceToTravel = 5f;
    public float DistanceToTravel { set { distanceToTravel = value; } }

    Vector3 targetPoint;

    private void OnEnable()
    {
        targetPoint = CalculateTargetPoint();
    }

    private void Update()
    {
        if (gameObject.activeInHierarchy)
        {
            Move();
            if (Vector3.Distance(transform.position,targetPoint) < 0.3f)
            {
                gameObject.SetActive(false);
            }
        }
    }

    public Vector3 CalculateTargetPoint()
    {
       return transform.position + new Vector3(0, 0,  Mathf.Abs(distanceToTravel));
    }

    private void Move()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}
