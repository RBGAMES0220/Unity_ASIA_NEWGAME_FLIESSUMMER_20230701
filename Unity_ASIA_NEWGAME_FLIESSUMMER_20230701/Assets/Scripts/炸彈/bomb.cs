using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bomb : MonoBehaviour
{
    public GameObject explosionRange;



    public Vector2 startSpeed;
    public float delayExplodeTime;
    public float destroyBombTime;
    public float hitBoxTime;

    private Rigidbody2D rd2d;
    private Animator anim;



    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        rd2d.velocity = transform.right * startSpeed.x + transform.up * startSpeed.y;

        Invoke("Explode", delayExplodeTime);


    }

    // Update is called once per frame
    void Update()
    {

    }
    void Explode()
    {
        anim.SetTrigger("Explode");
        Invoke("GenExplosionRange", hitBoxTime);
        Invoke("DestoryThisBomb", destroyBombTime);
    }
    void GenExplosionRange()
    {
        Instantiate(explosionRange, transform.position, Quaternion.identity);
    }

    void DestoryThisBomb()
    {
        Destroy(gameObject);
    }
}
