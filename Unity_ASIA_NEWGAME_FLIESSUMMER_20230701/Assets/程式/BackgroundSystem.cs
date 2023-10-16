using UnityEngine;

public class BackgroundSystem : MonoBehaviour
{
    [SerializeField, Header("背景流動速度"), Range(0, 10)]
    private float speed = 3;

    private Rigidbody2D rigPlayer;

    private void Awake()
    {
        rigPlayer = GameObject.Find("玩家").GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.Translate(rigPlayer.velocity.x * -speed * Time.deltaTime, 0, 0);
    }
}