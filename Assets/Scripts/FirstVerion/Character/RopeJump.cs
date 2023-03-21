using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeJump : MonoBehaviour
{
    // significant types
    public float ResetConnectionColldown = 1.0f;

    // reference types
    private HingeJoint2D _hingleJoint;
    private Rope _lastJoint;

    private void Start()
    {
        // initialize reference varibles 
        _hingleJoint = GetComponent<HingeJoint2D>();
    }

    private void OnEnable()
    {
        // Start calldown corutine 
        StartCoroutine(resetConnection());
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {

        if (collider.gameObject.transform?.parent != null && collider.gameObject.transform.parent.TryGetComponent(out Rope rope))
        {
            // check than the last connected item != current
            if (_lastJoint == rope) return;

            // enable and connect new item to character
            _hingleJoint.enabled = true;
            _hingleJoint.connectedBody = collider.attachedRigidbody;
            _lastJoint = rope;

        }
    }

    private IEnumerator resetConnection()
    {
        /*
         Method - calldown to cling to the same rope
        */

        while (true)
        {
            yield return new WaitForSeconds(ResetConnectionColldown);
            if (!_hingleJoint.enabled) _lastJoint = null;
        }
    }
}
