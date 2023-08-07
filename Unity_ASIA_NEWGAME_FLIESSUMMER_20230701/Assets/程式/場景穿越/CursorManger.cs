using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManger : MonoBehaviour
{
    private Vector3 mouseWorldPos => Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));

    private bool canClick;

    private void Update()
    {
        canClick = ObjectAtMousePosition();

        if (canClick && Input.GetMouseButtonDown(0))
        {
            ClickAction(ObjectAtMousePosition().gameObject);
        }
    }

    private void ClickAction(GameObject clickObjet)
    {
        switch (clickObjet.tag)
        {
            case "Teleport":
                var teleport = clickObjet.GetComponent<Teleport>();
                teleport?.TeleportToScene();

                break;
        }
    }



    private Collider2D ObjectAtMousePosition()
    {
        return Physics2D.OverlapPoint(mouseWorldPos);
    }
}
