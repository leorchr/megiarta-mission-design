using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{

    public float maxSpeed;
    public float accelerationSpeed;
    public float deccelerationSpeed;

    private Vector2 moveInput = Vector2.zero;

    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb =GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 fwd = Camera.main.transform.forward * moveInput.y;
        Vector3 side = Camera.main.transform.right * moveInput.x;
        Vector3 plDir = (fwd + side);
        plDir = new Vector3(plDir.x,0,plDir.z).normalized;
        rb.velocity = Vector3.ClampMagnitude(rb.velocity + (plDir * accelerationSpeed * Time.deltaTime), maxSpeed);
        if(plDir.magnitude < 0.001)
        {
            float deccel = deccelerationSpeed * Time.deltaTime;
            Vector2 plSpeed = new Vector2(plDir.x, plDir.z);
            plSpeed = Vector2.ClampMagnitude(plSpeed, plSpeed.magnitude - deccel);
            rb.velocity = new Vector3(plSpeed.x,rb.velocity.y,plSpeed.y);
        }
    }

    public void OnMove(InputAction.CallbackContext callbackContext)
    {
        moveInput = callbackContext.ReadValue<Vector2>();
    }
}
