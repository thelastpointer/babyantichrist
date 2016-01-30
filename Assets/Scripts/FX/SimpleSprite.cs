/*
	SimpleSprite.cs
	Sprites rotate towards the camera which tries to render it.
*/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SimpleSprite : MonoBehaviour
{
    public Vector3 AdditionalRotation = new Vector3(90.0f, 0.0f, 0.0f);
    public Vector3 RotationSpeed = Vector3.zero;

    void OnWillRenderObject()
    {
        transform.LookAt(Camera.current.transform.position);
        transform.Rotate(AdditionalRotation, Space.Self);
        transform.Rotate(RotationSpeed * Time.time, Space.Self);
    }
}
