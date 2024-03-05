using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPoint : MonoBehaviour
{
    public CamControlData data;

    private void Awake()
    {
        GetComponent<Renderer>().enabled = false;
    }
}
