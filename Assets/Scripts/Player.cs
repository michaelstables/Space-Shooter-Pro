using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [Header("Player Object References")]
    [SerializeField] ShipStatsSO playerStats;
    [SerializeField] GameObjectBoundsSO playerBounds;
    [SerializeField] GameObject playerShield;
    [SerializeField] GameObject playerLeftEngine;
    [SerializeField] GameObject playerRightEngine;
    [SerializeField] GameObject explosion;

    [Header("Player Position Tuning")]
    [SerializeField] Vector3 startPosition = new Vector3(0, -3f, 0);

    [Header("Player Laser Position Tuning")]
    [SerializeField] Vector3 laserOffset = new Vector3(0, .8f, 0);
    [SerializeField] Vector3 tripleshotLaserOffset = new Vector3(0, 1.05f, 0);

    [Header("Player Powerup Tuning")]
    [SerializeField] float powerupDuration = 5f;

    [Header("Serialized For Testing")]
    [SerializeField] Vector3 moveVector;
    [SerializeField] int playerLives;
    [SerializeField] float playerSpeed;
    [SerializeField] bool isTripleshotActive = false;
    [SerializeField] bool isSpeedBoostActive = false;
    [SerializeField] bool isShieldActive = false;

    SpriteRenderer shield;
    SpriteRenderer leftEngine;
    SpriteRenderer rightEngine;

    PlayerInput playerInput;
    InputAction moveAction;
    InputAction fireAction;

    ReferenceManager referenceManager;

    float nextFire = -1f;

    private void Awake()
    {
        GetReferances();
        SubscribeToPlayerInputEvents();
        SetPlayerStats();
        SetPlayerObjects();
    }

    private void GetReferances()
    {
        referenceManager = FindObjectOfType<ReferenceManager>();
        if (referenceManager == null) { Debug.LogError("No Reference Manager On Player"); }

        shield = playerShield.GetComponent<SpriteRenderer>();
        if (shield == null) { Debug.LogError("No Player Shield Referance Referance"); }

        leftEngine = playerLeftEngine.GetComponent<SpriteRenderer>();
        if (leftEngine == null) { Debug.LogError("No Left Engine Referance Referance"); }

        rightEngine = playerRightEngine.GetComponent<SpriteRenderer>();
        if (rightEngine == null) { Debug.LogError("No Right Engine Referance Referance"); }

        playerInput = GetComponent<PlayerInput>();
        if (playerInput == null) { Debug.LogError("No Player Input Referance"); }

        moveAction = playerInput.actions["Move"];
        if (moveAction == null) { Debug.LogError("No Player Movement Action Referance"); }

        fireAction = playerInput.actions["Fire"];
        if (fireAction == null) { Debug.LogError("No Player Fire Action Referance"); }
   
    }

    private void SetPlayerStats()
    {
        playerLives = playerStats.lives;
        playerSpeed = playerStats.moveSpeed;
        transform.position = startPosition;
    }

    private void SetPlayerObjects()
    {
        shield.enabled = false;
        leftEngine.enabled = false;
        rightEngine.enabled = false;
    }

    private void SubscribeToPlayerInputEvents()
    {
        fireAction.performed += OnFireAction;
    }

    private void OnDestroy()
    {
        fireAction.performed -= OnFireAction;
    }

    private void Update()
    {
        ReadPlayerInput();
        OnMove();
    }

    private void ReadPlayerInput()
    {
        moveVector.x = moveAction.ReadValue<Vector2>().x;
        moveVector.y = moveAction.ReadValue<Vector2>().y;
    }

    private void OnMove()
    {
        transform.Translate(moveVector * playerSpeed * Time.deltaTime);
        LockPlayerToVerticalBounds();
        WrapPlayerOnHorizontalBunds();
    }

    private void LockPlayerToVerticalBounds()
    {
        float yPosition = Mathf.Clamp(transform.position.y, playerBounds.minYPosition, playerBounds.maxYPosition);
        transform.position = new Vector3(transform.position.x, yPosition, transform.position.z);
    }

    private void WrapPlayerOnHorizontalBunds()
    {
        if (transform.position.x > playerBounds.maxXPosition)
        {
            transform.position = new Vector3(playerBounds.minXPosition, transform.position.y, transform.position.z);
        }
        else if (transform.position.x < playerBounds.minXPosition)
        {
            transform.position = new Vector3(playerBounds.maxXPosition, transform.position.y, transform.position.z);
        }
    }

    private void OnFireAction(InputAction.CallbackContext context)
    {
        if (Time.time > nextFire)
        {
            FireLaser();
        }
    }

    private void FireLaser()
    {
        if (isTripleshotActive)
        {
            nextFire = Time.time + playerStats.fireRate;
            Instantiate(playerStats.tripleshotLaser, transform.position + tripleshotLaserOffset, Quaternion.identity);
        }
        else
        {
            nextFire = Time.time + playerStats.fireRate;
            Instantiate(playerStats.laser, transform.position + laserOffset, Quaternion.identity);
        }

        referenceManager.audioManager.PlayLaserSoundSFX();
    }

    public void HandleTripleShotPowerup()
    {
        isTripleshotActive = true;
        StartCoroutine("TripleShotPowerupCoroutine");
            
    }

    public void HandleSpeedBoostPowerup()
    {
        isSpeedBoostActive = true;
        StartCoroutine("SpeedPowerupCoroutine");

    }

    public void HandleShieldPowerup()
    {
        isShieldActive = true;
        shield.enabled = true;
        
    }

    IEnumerator TripleShotPowerupCoroutine()
    {
        while (isTripleshotActive)
        {
            yield return new WaitForSecondsRealtime(powerupDuration);
            isTripleshotActive = false;
        }
    }

    IEnumerator SpeedPowerupCoroutine()
    {
        while (isSpeedBoostActive)
        {
            playerSpeed = playerStats.boostedMoveSpeed;
            yield return new WaitForSecondsRealtime(powerupDuration);
            playerSpeed = playerStats.moveSpeed;
            isSpeedBoostActive = false;
        }
    }

    public void DamagePlayer()
    {
        if (isShieldActive)
        {
            isShieldActive = false;
            shield.enabled = false;
            return;
        }

        playerLives--;
        referenceManager.uiManager.UpdateLivesImage(playerLives);

        if (playerLives == 2) { leftEngine.enabled = true; }
        else if (playerLives == 1) { rightEngine.enabled = true; }
        else if (playerLives <= 0)
        {
            explosion = Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(explosion, 3f);
            referenceManager.spawnManager.OnPlayerDeath();
            referenceManager.gameManager.GameOver();
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy Laser")
        {
            Destroy(other.gameObject);
            DamagePlayer();
            referenceManager.audioManager.PlayExplosionSoundSFX();
        }
    } 
}