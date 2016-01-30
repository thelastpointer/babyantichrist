using UnityEngine;
using System.Collections;

public class Inventory : MonoBehaviour
{
    public Transform LeftHandContainer, RightHandContainer;
    public AudioSource Audio;
    public AudioClip PickupSound;
    public AudioClip CombineSound;
    public GameObject HandIcon;

    Pickup leftHand, rightHand;
    Pickup lookAt = null;
    Vector3 leftOriginalScale, rightOriginalScale;

    void Update()
    {
        // Raycast every frame for items
        RaycastHit hit;
        lookAt = null;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0)), out hit, 1.5f, 1 << LayerMask.NameToLayer("Item")))
            lookAt = hit.collider.GetComponent<Pickup>();

        HandIcon.SetActive(lookAt != null);

        // Left hand
        if (Input.GetMouseButtonDown(0))
        {
            // Pick up
            if ((leftHand == null) && (lookAt != null))
            {
                Audio.PlayOneShot(PickupSound);
                leftHand = lookAt;
                leftOriginalScale = lookAt.transform.localScale;
                Rigidbody rb = lookAt.GetComponent<Rigidbody>();
                if (rb != null)
                    rb.isKinematic = true;
                lookAt.transform.SetParent(LeftHandContainer);
                lookAt.transform.localPosition = Vector3.zero;
                lookAt.transform.localRotation = Quaternion.identity;
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
                    leftHand.transform.SetParent(null);
                    leftHand.transform.localScale = leftOriginalScale;
                    Rigidbody rb = leftHand.GetComponent<Rigidbody>();
                    if (rb != null)
                    {
                        rb.isKinematic = false;
                        rb.velocity = GetComponentInParent<Rigidbody>().velocity;
                    }

                    leftHand = null;
                }
            }
        }
        // Right hand
        if (Input.GetMouseButtonDown(1))
        {
            // Pick up
            if ((rightHand == null) && (lookAt != null))
            {
                Audio.PlayOneShot(PickupSound);
                rightHand = lookAt;
                rightOriginalScale = lookAt.transform.localScale;
                Rigidbody rb = lookAt.GetComponent<Rigidbody>();
                if (rb != null)
                    rb.isKinematic = true;
                lookAt.transform.SetParent(RightHandContainer);
                lookAt.transform.localPosition = Vector3.zero;
                lookAt.transform.localRotation = Quaternion.identity;
            }
            // Unable to pick up
            else if ((rightHand != null) && (lookAt != null))
            {
                // TODO: Sad sound
            }
            // Throw shit
            else if ((rightHand != null) && (lookAt == null))
            {
                // TODO: Triggers and stuff

                {
                    rightHand.transform.SetParent(null);
                    rightHand.transform.localScale = rightOriginalScale;
                    Rigidbody rb = rightHand.GetComponent<Rigidbody>();
                    if (rb != null)
                    {
                        rb.isKinematic = false;
                        rb.velocity = GetComponentInParent<Rigidbody>().velocity;
                    }

                    rightHand = null;
                }
            }
        }
        // Combine
        if (Input.GetMouseButtonDown(2))
        {
            if ((leftHand != null) && (rightHand != null))
            {
                GameObject result = Crafting.Instance.GetCraftResult(leftHand.Item, rightHand.Item);
                if (result != null)
                {
                    Audio.PlayOneShot(CombineSound);
                    Destroy(leftHand.gameObject);
                    Destroy(rightHand.gameObject);

                    Vector3 pos = Camera.main.transform.TransformPoint(new Vector3(0f, 0f, 3f));
                    GameObject go = (GameObject)Instantiate(result, pos, Quaternion.identity);
                    go.SetActive(true);
                }
                else
                {
                    //...saaaad sound
                }
            }
        }
    }
}
