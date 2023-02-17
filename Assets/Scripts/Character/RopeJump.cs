using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeJump : MonoBehaviour
{

    // significant types
    public float resetConnectionColldown = 1.0f;
    public string lastJoint = "";

    // reference types
    private HingeJoint2D hingleJoint;

    private void Start()
    {
        hingleJoint = GetComponent<HingeJoint2D>();
        StartCoroutine(resetConnection());
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        switch (collider.gameObject.tag)
        {
            case "rope":
                // check than the last connected item != current
                if (lastJoint == collider.gameObject.transform.parent.name) return;

                // enable and connect new item to character
                hingleJoint.enabled = true;
                hingleJoint.connectedBody = collider.gameObject.GetComponent<Rigidbody2D>();
                lastJoint = collider.gameObject.transform.parent.name;
                break;
        }
    }

    private IEnumerator resetConnection()
    {
        while (true)
        {
            yield return new WaitForSeconds(resetConnectionColldown);
            if (!hingleJoint.enabled) lastJoint = "";
        }
    }


}
