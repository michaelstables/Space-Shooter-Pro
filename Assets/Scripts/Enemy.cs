using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy References")]
    [SerializeField] ShipStatsSO enemyStats;
    [SerializeField] GameObjectBoundsSO enemyBounds;
    [SerializeField] GameObject laser;

    ReferenceManager referenceManager;
    GameObject enemyLaser;

    Animator animator;

    float speed;

    private void Awake()
    {
        GetReferences();
        speed = enemyStats.moveSpeed;
    }

    private void GetReferences()
    {
        animator = GetComponent<Animator>();

        referenceManager = FindObjectOfType<ReferenceManager>();
        if (referenceManager == null) { Debug.LogError("No Reference Manager On Player"); }
    }

    private void Start()
    {
        StartCoroutine("FireLaser");
    }

    void Update()
    {
        CalculateMovement();
        WrapToTop();
    }

    void CalculateMovement()
    {
        transform.Translate(new Vector3(0, -1, 0) * speed * Time.deltaTime);
    }

    void WrapToTop()
    {

        if (transform.position.y < enemyBounds.minYPosition)
        {
            transform.position = new Vector3(Random.Range(-11f, 11f), enemyBounds.maxYPosition, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            HandleEnemyDeath();
            if (referenceManager.player != null)
            {
                referenceManager.player.DamagePlayer();
            }
        }

        if(other.tag == "Laser")
        {
            referenceManager.scoreManager.IncrementScore();
            Destroy(other.gameObject);
            HandleEnemyDeath();
        }
    }

    private void HandleEnemyDeath()
    {
        StopCoroutine("FireLaser");
        Destroy(GetComponent<Collider2D>());
        animator.SetTrigger("OnEnemyDestroyed");
        speed = 0;
        referenceManager.audioManager.PlayExplosionSoundSFX();
        Destroy(gameObject, 2.5f);
    }

    IEnumerator FireLaser()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(2f, 5f));
            enemyLaser = Instantiate(laser, transform.position + new Vector3(0, -1.4f, 0), Quaternion.identity);
            referenceManager.audioManager.PlayLaserSoundSFX();
        }
    }
}