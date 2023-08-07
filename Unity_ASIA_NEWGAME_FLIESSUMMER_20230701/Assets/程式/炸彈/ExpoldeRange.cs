using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ExpoldeRange : MonoBehaviour
{
    public int damage;
    public float destroyTime;

    private GameObject playerGameObject;
    private PlayerHealth playerHealth;

    void Start()
    {
        playerGameObject = GameObject.FindGameObjectWithTag("Player");
        playerHealth = playerGameObject.GetComponent<PlayerHealth>();
        Destroy(gameObject, destroyTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>()?.TakeDamage(damage);
        }
        else if (other.gameObject.CompareTag("Player") && other is CapsuleCollider2D)
        {
            if (playerHealth != null)
            {
                playerHealth.DamagePlayer(damage);
            }
        }
    }
}
