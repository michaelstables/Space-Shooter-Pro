using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField] PowerupsSO powerup;
    ReferenceManager referenceManager;

    private void Awake()
    {
        referenceManager = FindObjectOfType<ReferenceManager>();
        if (referenceManager == null) { Debug.LogError("Reference manager is Null"); }
    }

    private void Update()
    {
        CalculateMovement();
        OnLeaveScreen();
    }

    private void CalculateMovement()
    {
        transform.Translate(new Vector3(0, -1, 0) * powerup.moveSpeed * Time.deltaTime);
    }

    private void OnLeaveScreen()
    {
        if (transform.position.y <= powerup.minYPosition)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.tag == "Player")
        {
            HandlePowerup();
            referenceManager.audioManager.PlayPowerupSFX();
            Destroy(gameObject);
        }
    }

    private void HandlePowerup()
    {
        switch (powerup.powerupID)
        {
            case 0:
                referenceManager.player.HandleTripleShotPowerup();
                break;
            case 1:
                referenceManager.player.HandleSpeedBoostPowerup();
                break;
            case 2:
                referenceManager.player.HandleShieldPowerup();
                break;
            default:
                Debug.Log("No Powerup Enabled");
                break;
        }
    }
}