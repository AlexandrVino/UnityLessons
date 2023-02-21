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
        _hingleJoint = GetComponent<HingeJoint2D>();
    }

    private void OnEnable()
    {
        StartCoroutine(resetConnection());
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {

        if (collider.gameObject.transform.parent.TryGetComponent(out Rope rope))
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
        while (true)
        {
            yield return new WaitForSeconds(ResetConnectionColldown);
            if (!_hingleJoint.enabled) _lastJoint = null;
        }
    }
}
