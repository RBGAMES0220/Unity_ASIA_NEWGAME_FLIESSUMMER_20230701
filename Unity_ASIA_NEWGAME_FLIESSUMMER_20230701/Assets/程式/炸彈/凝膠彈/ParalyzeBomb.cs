using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParalyzeBomb : MonoBehaviour
{
    public GameObject explosionRange; // 爆炸範圍預製物
    public int damagePerTick;         // 每次傷害量
    public float tickInterval;        // 傷害間隔時間
    public float duration;            // 爆炸持續時間
    public float paralyzeDuration;    // 麻痺狀態持續時間

    private float elapsedTime;        // 已過時間
    private bool isExploding;         // 是否正在爆炸狀態

    void Start()
    {
        elapsedTime = 0f;
        isExploding = false;
    }

    void Update()
    {
        if (isExploding)
        {
            elapsedTime += Time.deltaTime;

            if (elapsedTime >= tickInterval)
            {
                elapsedTime = 0f;
                DamageAndParalyzeEnemies(); // 造成傷害和施加麻痺狀態
            }

            if (elapsedTime >= duration)
            {
                Destroy(gameObject);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            isExploding = true;
            Invoke("Explode", tickInterval);
        }
    }

    void Explode()
    {
        Instantiate(explosionRange, transform.position, Quaternion.identity);
        DamageAndParalyzeEnemies();
        InvokeRepeating("DamageAndParalyzeEnemies", tickInterval, tickInterval);
        Destroy(gameObject, duration);
    }

    void DamageAndParalyzeEnemies()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRange.GetComponent<CircleCollider2D>().radius);

        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject.CompareTag("Enemy"))
            {
                Paralyzetheenemy enemy = collider.GetComponent<Paralyzetheenemy>();
                if (enemy != null)
                {
                    enemy.ApplyParalyze(paralyzeDuration); // 對敵人施加麻痺狀態
                    enemy.TakeDamage(damagePerTick);       // 對敵人造成傷害
                }
            }
        }
    }
}
