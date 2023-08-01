using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Transform player;

    // Use LateUpdate instead of Update for smoother camera movement
    void LateUpdate()
    {
        // Check if the player is assigned
        if (player != null)
        {
            // Set the camera's position to follow the player's position
            Vector3 targetPosition = new Vector3(player.position.x, player.position.y, -10f);
            transform.position = targetPosition;
        }
    }
}

