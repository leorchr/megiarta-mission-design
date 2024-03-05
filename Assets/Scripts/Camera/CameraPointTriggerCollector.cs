using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPointTriggerCollector : MonoBehaviour
{
    CameraController camController;
    private void Start()
    {
        camController = Camera.main.GetComponent<CameraController>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CameraPoint"))
        {
            camController.AddCamPointTrigger(other.GetComponent<CameraPoint>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("CameraPoint"))
        {
            camController.RemoveCamPointTrigger(other.GetComponent<CameraPoint>());
        }
    }
}
