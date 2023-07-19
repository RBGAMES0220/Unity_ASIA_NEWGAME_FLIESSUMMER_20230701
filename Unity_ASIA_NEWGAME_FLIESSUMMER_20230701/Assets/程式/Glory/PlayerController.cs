using Spine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed = 5f; // 玩家移動速度
    public float jumpForce = 10f; // 跳躍力度
    public int maxJumps = 2; // 最大跳躍次數
    public Collider2D coll;
    public Animator anim;
    public LayerMask ground;
    public float attackRange = 1f; // 玩家攻擊範圍
    public int attackDamage = 10; // 玩家攻擊力
    public LayerMask enemyLayer; // 敵人的圖層（用於碰撞檢測）

    private Rigidbody2D rb; // Rigidbody2D組件
    private int jumpCount = 0; // 當前跳躍次數
    private bool isGrounded = false; // 玩家是否在地面上
    private bool isDoubleJumping = false; // 玩家是否正在進行第二段跳躍
    private float movement = 0f; // 左右移動輸入
    private bool isAttacking = false;

    // 初始化
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // 取得Rigidbody2D組件
    }

    // 獲取左右移動輸入和跳躍輸入
    private void Update()
    {
        movement = Input.GetAxisRaw("Horizontal"); // 讀取左右移動輸入

        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded)
            {
                jumpCount = 1;
                isDoubleJumping = false;
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                anim.SetBool("jumping", true);
            }
            else if (!isDoubleJumping && jumpCount < maxJumps)
            {
                jumpCount++;
                isDoubleJumping = true;
                rb.velocity = new Vector2(rb.velocity.x, 0f);
                rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
                anim.SetBool("doubleJumping", true);
                
            }
        }

        if (Input.GetButtonDown("Fire1"))
        {
            // 攻擊按鈕的檢測
            Attack();
        }

    }

    // 玩家移動
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(movement * moveSpeed * Time.deltaTime, rb.velocity.y);

        if (movement > 0)
        {
            transform.localScale = new Vector3(1f, 1f, 1f); // 面向右
        }
        else if (movement < 0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f); // 面向左
        }

        anim.SetFloat("running", Mathf.Abs(movement));
        SwitchAnim();
    }

    // 重置跳躍次數
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            isGrounded = true;
            jumpCount = 0;
            anim.SetBool("falling", false);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            isGrounded = false;
        }
    }

    private void SwitchAnim()
    {
        anim.SetBool("idle", false);

        if (anim.GetBool("jumping"))
        {
            if (rb.velocity.y < 0)
            {
                anim.SetBool("jumping", false);
                anim.SetBool("falling", true);
            }
            else if (rb.velocity.y > 0)
            {
                anim.SetBool("jumping", true);
                anim.SetBool("falling", false);
            }
        }
        else if (coll.IsTouchingLayers(ground))
        {
            anim.SetBool("falling", false);
            anim.SetBool("jumping", false);
            anim.SetBool("doubleJumping", false);
            anim.SetBool("idle", true);
        }

        if (isDoubleJumping)
        {
            if (rb.velocity.y < 0)
            {
                anim.SetBool("doubleJumping", false);
                anim.SetBool("falling", true);
            }
            else if (rb.velocity.y > 0)
            {
                anim.SetBool("doubleJumping", true);
                anim.SetBool("falling", false);
            }
        }
    }

    private void Attack()
    {
        // 在攻擊時進行碰撞檢測，檢測是否有敵人在攻擊範圍內
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, attackRange, enemyLayer);

        Debug.Log("Attack method is called!");

        // 對檢測到的敵人進行攻擊
        foreach (Collider2D enemy in hitEnemies)
        {
            // 獲取敵人的腳本（如果有的話）並給予傷害
            EnemyHealth enemyScript = enemy.GetComponent<EnemyHealth>();
            if (enemyScript != null)
            {
                enemyScript.TakeDamage(attackDamage);
            }
        }
        if (!isAttacking)
        {
            isAttacking = true;
            anim.SetBool("isAttacking", true);
        }
        else if (isAttacking) 
        {
            isAttacking = false;
            anim.SetBool("idle", true);
        }
    }

    
}









