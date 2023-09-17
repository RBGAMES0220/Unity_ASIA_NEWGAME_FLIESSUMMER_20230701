using UnityEngine;

namespace GLORY
{
    public class PlayerController : MonoBehaviour
    {
        public float moveSpeed = 5f; // 玩家移動速度
        public float jumpForce = 10f; // 跳躍力度
        public int maxJumps = 2; // 最大跳躍次數
        public Collider2D coll;
        public Animator anim;
        public LayerMask ground;
        public Vector2 attackRangeScale = new Vector2(1f, 1f); // 玩家攻擊範圍的缩放
        public Vector2 attackRangeOffset = new Vector2(1f, 0f); // 玩家攻擊範圍的位置偏移
        public int attackDamage = 10; // 玩家攻擊力
        public LayerMask enemyLayer; // 敵人的圖層（用於碰撞檢測）
        public Color attackRangeGizmoColor = Color.red; // 攻擊範圍的Gizmo顏色
        public int integer = 0;
        public GameObject myBag;
        bool isOpen;
        public AudioSource attackSound; // 要播放的攻擊音效的AudioSource組件
        public AudioSource moveSound; // 移動音效的AudioSource組件
        public AudioSource jumpSound; // 要播放的跳躍音效的AudioSource組件
        public GameObject attackEffect; // 攻擊特效的引用
        public Vector3 attackEffectOffset = new Vector3(0f, 1f, 0f); // 上升偏移量



        private Rigidbody2D rb; // Rigidbody2D組件
        private int jumpCount = 0; // 當前跳躍次數
        private bool isGrounded = false; // 玩家是否在地面上
        private bool isDoubleJumping = false; // 玩家是否正在進行第二段跳躍
        private float movement = 0f; // 左右移動輸入
        private bool isAttacking = false;
        private PlayerHealth playerHealth; // 玩家的PlayerHealth組件

        // 初始化
        private void Start()
        {
            rb = GetComponent<Rigidbody2D>(); // 取得Rigidbody2D組件
            playerHealth = GetComponent<PlayerHealth>(); // 取得PlayerHealth組件
            anim = GetComponent<Animator>(); // 取得Animator組件
            integer = 50;
        }

        // 獲取左右移動輸入和跳躍輸入
        private void Update()
        {
            OpenMyBag();

            

            movement = Input.GetAxisRaw("Horizontal"); // 讀取左右移動輸入

            if (Input.GetButtonDown("Jump"))
            {
                if (isGrounded)
                {
                    jumpCount = 1;
                    isDoubleJumping = false;
                    rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                    anim.SetBool("jumping", true);

                    // 播放跳躍音效
                    if (jumpSound != null)
                    {
                        jumpSound.Play();
                    }
                }
                else if (!isDoubleJumping && jumpCount < maxJumps)
                {
                    jumpCount++;
                    isDoubleJumping = true;
                    rb.velocity = new Vector2(rb.velocity.x, 0f);
                    rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
                    anim.SetBool("doubleJumping", true);

                    // 播放跳躍音效
                    if (jumpSound != null)
                    {
                        jumpSound.Play();
                    }
                }
            }

            if (Input.GetButtonDown("Fire1"))
            {
                // 攻擊按鈕的檢測
                Attack();
            }
        }

        // 玩家移動和攻擊
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

            if (movement != 0 && isGrounded && !isAttacking)
            {
                PlayMoveSound();
            }
            else
            {
                StopMoveSound();
            }
        }

        // 重置跳躍次數
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                isGrounded = true;
                jumpCount = 0;
                anim.SetBool("falling", false);
                anim.SetBool("idle", true);
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

            if (rb.velocity.y < 0 && !coll.IsTouchingLayers(ground))
            {
                //anim.SetBool("falling", true);
            }

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
                if (rb.velocity.y < 0) // 下落時切換到下落動畫
                {
                    //anim.SetBool("falling", true);
                    anim.SetBool("doubleJumping", false);
                    anim.SetBool("idle", false);
                }
                else if (rb.velocity.y == 0) // 在地面上待機時切換到待機動畫
                {
                    anim.SetBool("falling", false);
                    anim.SetBool("doubleJumping", false);
                    anim.SetBool("idle", true);
                }
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
            Vector2 facingDirection = (transform.localScale.x > 0) ? Vector2.right : Vector2.left;
            Vector2 attackRangePosition = (Vector2)transform.position + new Vector2(attackRangeOffset.x * facingDirection.x, attackRangeOffset.y);
            Collider2D[] hitEnemies = Physics2D.OverlapBoxAll(attackRangePosition, attackRangeScale, 0f, enemyLayer);

            Debug.Log("Attack method is called!");

            // 在攻擊時播放音效
            attackSound.Play();


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

          

            // 在攻擊時不播放受傷動畫
            anim.ResetTrigger("hurt");

            // 計算生成位置，包括上升偏移量和面朝方向
            Vector3 spawnOffset = new Vector3((transform.localScale.x > 0) ? attackEffectOffset.x : -attackEffectOffset.x, attackEffectOffset.y, attackEffectOffset.z);
            Vector3 spawnPosition = transform.position + spawnOffset;

            // 計算生成特效時的旋轉，根據面朝方向決定
            Quaternion spawnRotation = (transform.localScale.x > 0) ? Quaternion.identity : Quaternion.Euler(0, 180, 0);

            // 啟動攻擊特效
            if (attackEffect != null)
            {
                GameObject effect = Instantiate(attackEffect, spawnPosition, spawnRotation);
                Destroy(effect, 1.0f); // 1.0秒後銷毀特效，你可以根據需要調整時間
            }
        }

        // 移動音效的播放
        private void PlayMoveSound()
        {
            if (!moveSound.isPlaying)
            {
                moveSound.Play();
            }
        }

        // 停止移動音效的播放
        private void StopMoveSound()
        {
            moveSound.Stop();
        }

        // 動畫事件：攻擊動畫結束時觸發
        public void OnAttackAnimationEnd()
        {
            isAttacking = false;
            anim.SetBool("isAttacking", false);
        }

        // 傷害停止時播放待機動畫
        private void LateUpdate()
        {

        }

        // 繪制攻擊範圍的Gizmo
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = attackRangeGizmoColor;
            Vector2 facingDirection = (transform.localScale.x > 0) ? Vector2.right : Vector2.left;
            Vector2 attackRangePosition = (Vector2)transform.position + new Vector2(attackRangeOffset.x * facingDirection.x, attackRangeOffset.y);
            Gizmos.DrawWireCube(attackRangePosition, attackRangeScale);
        }

        void OpenMyBag()
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                isOpen =!isOpen;
                myBag.SetActive(isOpen);
            }
        }
    }

}



























