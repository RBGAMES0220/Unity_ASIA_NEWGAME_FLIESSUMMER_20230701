using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinuousDamageBomb : MonoBehaviour
{
    public GameObject explosionRange;
    public int damagePerTick;       // 每個傷害週期對敵人造成的傷害量
    public float tickInterval;      // 傷害週期的間隔時間
    public float duration;          // 炸彈持續傷害的總時間

    private float elapsedTime;      // 已經過的時間
    private bool isExploding;       // 是否正在爆炸狀態

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
                DamageEnemiesInRange(); // 對範圍內的敵人造成傷害
            }

            if (elapsedTime >= duration)
            {
                Destroy(gameObject);    // 在達到指定的持續時間後銷毀炸彈
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            isExploding = true;
            Invoke("Explode", tickInterval); // 啟動爆炸的計時過程
        }
    }

    void Explode()
    {
        Instantiate(explosionRange, transform.position, Quaternion.identity);
        DamageEnemiesInRange();                  // 對範圍內的敵人造成傷害
        InvokeRepeating("DamageEnemiesInRange", tickInterval, tickInterval);
        Destroy(gameObject, duration);           // 在指定的持續時間後銷毀炸彈
    }

    void DamageEnemiesInRange()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRange.GetComponent<CircleCollider2D>().radius);

        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject.CompareTag("Enemy"))
            {
                collider.GetComponent<Enemy>().TakeDamage(damagePerTick);
            }
        }
    }
}