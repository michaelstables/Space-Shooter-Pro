using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [Header("Reference Objects")]
    [SerializeField] GameObject explosion;

    [Header ("Astroid Movement Tuning")]
    [SerializeField] float rotationSpeed = 5f;

    ReferenceManager referenceManager;

    private void Awake()
    {
        GetReferences();
    }

    private void GetReferences()
    {
        referenceManager = FindObjectOfType<ReferenceManager>();
        if (referenceManager == null) { Debug.LogError("No Reference Manager On Player"); }
    }

    void Update()
    {
        transform.Rotate(new Vector3(0, 0, 1) * rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Laser")
        {
            explosion = Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(explosion, 2.633f);

            Destroy(other.gameObject);

            referenceManager.audioManager.PlayExplosionSoundSFX();
            referenceManager.spawnManager.StartSpawning();

            Destroy(this.gameObject, 0.25f);
        }
    }
}