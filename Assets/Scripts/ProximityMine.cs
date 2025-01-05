using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProximityMine : MonoBehaviour
{
    [SerializeField] private float activationRange = 2f;
    [SerializeField] private float explosionRange = 3f;
    [SerializeField] private float explosionDamage = 20f;
    [SerializeField] private float explosionDelay = 3f;
    [SerializeField] private GameObject explosionEffectPrefab;
    [SerializeField] private Color alternateColor1 = new Color(124,43,4); // Color 1 for alternation
    [SerializeField] private Color alternateColor2 = Color.yellow; // Color 2 for alternation
    [SerializeField] private float colorChangeInterval = 0.5f; // Time between color changes

    private bool isTriggered = false;
    private float countdown = 0f;
    private float colorChangeTimer = 0f;
    private bool useFirstColor = true;
    private SpriteRenderer spriteRenderer;

    private void CheckProximity()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            float distance = Vector2.Distance(transform.position, player.transform.position);
            if (distance <= activationRange)
            {
                isTriggered = true;
                countdown = explosionDelay;
            }
        }
    }

    private void Explode()
    {

        if (explosionEffectPrefab != null)
        {
            Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity);
        }

        Collider2D[] objectsInRange = Physics2D.OverlapCircleAll(transform.position, explosionRange);
        foreach (Collider2D obj in objectsInRange)
        {
            // Trigger explosions in other crates
            if (obj.CompareTag("Trap"))
            {
                Crate crate = obj.GetComponent<Crate>();
                if (crate != null)
                {
                    crate.InteractWithCrate();
                }
            }

            if (obj.CompareTag("Player"))
            {
                PlayerStats playerStats = obj.GetComponent<PlayerStats>();
                if (playerStats != null && !playerStats.isTrapImmune())
                {
                    playerStats.TakeDamage(explosionDamage);
                }
            }

            if (obj.CompareTag("Enemy"))
            {
                EnemyHealth enemy = obj.GetComponent<EnemyHealth>();
                if (enemy != null)
                {
                    enemy.TakeDamage(explosionDamage);
                }
            }
        }

        Destroy(gameObject);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRange);
    }

    private void ChangeColor()
    {
        useFirstColor = !useFirstColor;
        spriteRenderer.color = useFirstColor ? alternateColor1 : alternateColor2;
        colorChangeTimer = colorChangeInterval;
    }
    private void Start()
    {
        explosionDamage = explosionDamage*GameObject.Find("RoomManager").GetComponent<RoomManager>().GetDifficulty();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        if (isTriggered)
        {
            colorChangeTimer -= Time.deltaTime;
            if (colorChangeTimer <= 0f)
            {
                ChangeColor();
            }

            countdown -= Time.deltaTime;
            if (countdown <= 0f)
            {
                Explode();
            }
        }
        else
        {
            CheckProximity();
        }
    }

}
