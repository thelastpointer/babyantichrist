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
    Usable lookAt = null;
    Vector3 leftOriginalScale, rightOriginalScale;

    void Update()
    {
        // Raycast every frame for items
        RaycastHit hit;
        lookAt = null;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0)), out hit, 1.5f))
            lookAt = hit.collider.GetComponent<Usable>();

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
                    leftOriginalScale = lookAt.transform.localScale;
                    Rigidbody rb = lookAt.GetComponent<Rigidbody>();
                    if (rb != null)
                        rb.isKinematic = true;
                    lookAt.transform.SetParent(LeftHandContainer);
                    lookAt.transform.localPosition = Vector3.zero;
                    lookAt.transform.localRotation = Quaternion.identity;
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
