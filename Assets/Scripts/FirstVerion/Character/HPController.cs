using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPController : MonoBehaviour
{
    /*
     * Class to control HP value of character
    */
    [SerializeField] private Interface _interface;

    private HingeJoint2D _hingleJoint;

    private void Start()
    {
        // initialize reference varibles 
        _hingleJoint = GetComponent<HingeJoint2D>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        /*
         Method for control character condishion,
         if he contact with some dangerous or useful objects
        */
        
        if (collider.gameObject.TryGetComponent(out DeathZone deathZone))
        {
            _hingleJoint.enabled = false;
            transform.position = new Vector2(-17.0f, -3.0f);
            _interface.RemoveHP();
        }

        
    }
}
