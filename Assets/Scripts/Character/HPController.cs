using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider)
    {
        switch (collider.gameObject.tag)
        {
            case "deathZone":
                // Move character to start Position
                transform.position = new Vector2(-17.0f, -3.0f); break;
        }
    }
}
