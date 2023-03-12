using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPController : MonoBehaviour
{
    /*
     * Class to control HP value of character
    */

    private void OnTriggerEnter2D(Collider2D collider)
    {
        /*
         Method for control character condishion,
         if he contact with some dangerous or useful objects
        */
        if (collider.gameObject.TryGetComponent(out DeathZone deathZone)) 
            transform.position = new Vector2(-17.0f, -3.0f);
    }
}
