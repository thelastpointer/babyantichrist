using UnityEngine;
using System.Collections;

public class Inventory : MonoBehaviour
{
    public Transform LeftHandContainer, RightHandContainer;
    public AudioSource Audio;
    public AudioClip PickupSound;
    public AudioClip CombineSound;
    public GameObject HandIcon;
    public Camera Cam;

    Pickup leftHand, rightHand;
    Usable lookAt = null;
    Vector3 leftOriginalScale, rightOriginalScale;

    void Update()
    {
        // Raycast every frame for items
        lookAt = null;
        Ray ray = new Ray(Cam.transform.position, Cam.transform.TransformDirection(0f, 0f, 1f));
        Debug.DrawRay(ray.origin, ray.direction, Color.yellow, 1.5f);
        RaycastHit[] hits = Physics.RaycastAll(ray, 1.5f);
        foreach (RaycastHit hit in hits)
        {
            Usable u = hit.collider.GetComponent<Usable>();
            if (u != null)
            {
                lookAt = u;
                break;
            }
        }
        //if (Physics.Raycast(ray, out hit, 1.5f)) lookAt = hit.collider.GetComponent<Usable>();

        // UI icon
        HandIcon.SetActive(lookAt != null);

        // Left hand
        if (Input.GetMouseButtonDown(0))
        {
            // Pick up stuff
            if ((lookAt != null) && (lookAt.Item != null))
            {
                if ((leftHand == null) && (lookAt != null) && (lookAt.Item != null))
                {
                    Audio.PlayOneShot(PickupSound);
                    leftHand = lookAt.Item;
                    leftOriginalScale = lookAt.Item.transform.localScale;
                    Rigidbody rb = lookAt.Item.GetComponent<Rigidbody>();
                    if (rb != null)
                        rb.isKinematic = true;
                    lookAt.Item.transform.SetParent(LeftHandContainer);
                    lookAt.Item.transform.localPosition = Vector3.zero;
                    lookAt.Item.transform.localRotation = Quaternion.identity;
                }
            }

            // Activate trigger
            if ((lookAt != null) && (lookAt.Trigger != null))
            {
                if (string.IsNullOrEmpty(lookAt.NeedsItem) || ((leftHand != null) && (string.Compare(leftHand.Item, lookAt.NeedsItem) == 0)))
                {
                    lookAt.Trigger.Invoke();
                    if (lookAt.ConsumesItem)
                        Destroy(leftHand.gameObject);

                    if (lookAt.OneTime)
                    {
                        Destroy(lookAt);
                        lookAt = null;
                    }
                }
            }
            
            // Throw what's in out hands
            if ((leftHand != null) && (lookAt == null))
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
        // Right hand
        if (Input.GetMouseButtonDown(1))
        {
            // Pick up stuff
            if ((lookAt != null) && (lookAt.Item != null))
            {
                if ((rightHand == null) && (lookAt != null) && (lookAt.Item != null))
                {
                    Audio.PlayOneShot(PickupSound);
                    rightHand = lookAt.Item;
                    rightOriginalScale = lookAt.Item.transform.localScale;
                    Rigidbody rb = lookAt.Item.GetComponent<Rigidbody>();
                    if (rb != null)
                        rb.isKinematic = true;
                    lookAt.Item.transform.SetParent(RightHandContainer);
                    lookAt.Item.transform.localPosition = Vector3.zero;
                    lookAt.Item.transform.localRotation = Quaternion.identity;
                }
            }

            // Activate trigger
            if ((lookAt != null) && (lookAt.Trigger != null))
            {
                if (string.IsNullOrEmpty(lookAt.NeedsItem) || ((rightHand != null) && (string.Compare(rightHand.Item, lookAt.NeedsItem) == 0)))
                {
                    lookAt.Trigger.Invoke();
                    if (lookAt.ConsumesItem)
                        Destroy(rightHand.gameObject);

                    if (lookAt.OneTime)
                    {
                        Destroy(lookAt);
                        lookAt = null;
                    }
                }
            }

            // Throw what's in out hands
            if ((rightHand != null) && (lookAt == null))
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
            }
        }
    }
}
