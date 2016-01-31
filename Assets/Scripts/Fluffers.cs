using UnityEngine;
using System.Collections;

public class Fluffers : MonoBehaviour
{
    public GameObject AngryVersion, HappyVersion;
    public AudioSource Audio;
    public AudioClip AngrySound, HappySound;
    public Transform Destination;

    bool isAngry;

    public void GiveCat()
    {
        StopAllCoroutines();

        Audio.PlayOneShot(AngrySound);
        HappyVersion.SetActive(true);
        AngryVersion.SetActive(false);

        StartCoroutine(MoveAway());
    }

    public void AngryStuff()
    {
        if (!isAngry)
        {
            StartCoroutine(AngryCoroutine());
        }
    }

    IEnumerator AngryCoroutine()
    {
        isAngry = true;
        Audio.PlayOneShot(AngrySound);

        HappyVersion.SetActive(false);
        AngryVersion.SetActive(true);

        yield return new WaitForSeconds(3f);

        isAngry = false;

        HappyVersion.SetActive(true);
        AngryVersion.SetActive(false);
    }

    IEnumerator MoveAway()
    {
        for (;;)
        {
            if (Vector3.SqrMagnitude(Destination.position - transform.position) > 0.1f)
                transform.position = transform.position + (Destination.position - transform.position).normalized * Time.deltaTime * 4f;
            else
                break;

            yield return null;
        }
    }
}
