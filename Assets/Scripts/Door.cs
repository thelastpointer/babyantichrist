using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class Door : MonoBehaviour
{
    public GameObject[] ActiveObjects;
    public GameObject[] InactiveObjects;
    public bool ActiveByDefault = false;
    public Transform Player;
    public Transform Destination;
    public UnityEvent OnTransport;

    void Start()
    {
        if (ActiveByDefault)
            Activate();
        else
            Deactivate();
    }

    public void Activate()
    {
        foreach (GameObject go in ActiveObjects)
            go.SetActive(true);
        foreach (GameObject go in InactiveObjects)
            go.SetActive(false);
    }
    public void Deactivate()
    {
        foreach (GameObject go in ActiveObjects)
            go.SetActive(false);
        foreach (GameObject go in InactiveObjects)
            go.SetActive(true);
    }

    public void PlaceCharacter()
    {
        Player.position = Destination.position;
        if (OnTransport != null)
            OnTransport.Invoke();
    }
}
