using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwbomb : MonoBehaviour
{
    public GameObject bomb;


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            Instantiate(bomb, transform.position, transform.rotation);
        }
    }
}
