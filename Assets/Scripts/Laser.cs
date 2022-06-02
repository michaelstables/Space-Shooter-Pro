using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] GameObjectBoundsSO laserBounds;

    [Header ("Laser Tuning")]
    [SerializeField] float laserSpeed = 10f;

    private void Update()
    {
        CalculateMovement();
        DestroyLaser();
    }

    private void CalculateMovement()
    {
        transform.Translate(new Vector3(0, 1, 0) * laserSpeed * Time.deltaTime);
    }

    private void DestroyLaser()
    {
        if (transform.position.y > laserBounds.maxYPosition || transform.position.y < laserBounds.minYPosition)
        {
            Destroy(gameObject);
        }
    }
}