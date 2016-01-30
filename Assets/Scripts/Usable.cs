using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class Usable : MonoBehaviour
{
    public UnityEvent Trigger;
    public Pickup Item;
    public string NeedsItem;
    public bool ConsumesItem = false;
}
