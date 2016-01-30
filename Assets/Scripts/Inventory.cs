using UnityEngine;
using System.Collections;

public class Inventory : MonoBehaviour
{
    public Transform LeftHandContainer, RightHandContainer;

    Pickup leftHand, rightHand;
    Pickup lookAt = null;

    void Update()
    {
        // Raycast every frame for items
        RaycastHit hit;
        lookAt = null;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0)), out hit, 3f, LayerMask.NameToLayer("Item"), QueryTriggerInteraction.Collide))
            lookAt = hit.collider.GetComponent<Pickup>();

        // Left hand
        if (Input.GetMouseButtonDown(0))
        {
            // Pick up
            if ((leftHand == null) && (lookAt != null))
            {
                leftHand = lookAt;
                Rigidbody rb = lookAt.GetComponent<Rigidbody>();
                if (rb != null)
                    rb.isKinematic = true;
                lookAt.transform.SetParent(LeftHandContainer, false);
            }
            // Unable to pick up
            else if ((leftHand != null) && (lookAt != null))
            {
                // TODO: Sad sound
            }
            // Throw shit
            else if ((leftHand != null) && (lookAt == null))
            {
                // TODO: Triggers and stuff

                {
                    leftHand.transform.SetParent(null, false);
                    Rigidbody rb = leftHand.GetComponent<Rigidbody>();
                    if (rb != null)
                        rb.isKinematic = false;
                }
            }
        }
        // Right hand
        if (Input.GetMouseButtonDown(1))
        {
            //...
        }
        // Combine
        if (Input.GetMouseButtonDown(2))
        {
            if ((leftHand != null) && (rightHand != null))
            {

            }
        }
    }
}
