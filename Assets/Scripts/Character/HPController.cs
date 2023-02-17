using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.TryGetComponent(out DeathZone deathZone)) 
            transform.position = new Vector2(-17.0f, -3.0f);
    }
}
