using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent (typeof(CameraPointTriggerCollector))]
public class PlayerController : MonoBehaviour
{

    public float maxSpeed;
    public float accelerationSpeed;
    public float deccelerationSpeed;

    private Vector2 moveInput = Vector2.zero;

    private Rigidbody rb;
    private Animator animator;

    public float smoothRotValue = 0.2f;
    private float smoothRotVel = 0;

    public AnimationCurve jumpPowerCurveMultiplier = AnimationCurve.EaseInOut(0,1,1,0);
    public float initialJumpPower;
    public float jumpPower;
    public float jumpDuration;

    private float jumpTime;

    private bool isJumping = false;

    [Range(0.01f, 1f)]
    public float turnPower = 0.5f;
    public float angleOffset = 15;

    public float airForceMultiplier = 0.2f;

    public Transform groundCheck;
    public LayerMask groundMask;

    bool isGrounded = false;

    
    
    // Start is called before the first frame update
    void Start()
    {
        rb =GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = checkIsGrounded();
        Vector3 fwd = Camera.main.transform.forward * moveInput.y;
        Vector3 side = Camera.main.transform.right * moveInput.x;
        Vector3 plDir = (fwd + side);
        plDir = new Vector3(plDir.x,0,plDir.z).normalized;

        float gravityVelocity = rb.velocity.y;
        Vector3 velNoGrav = new Vector3(rb.velocity.x,0,rb.velocity.z);

        float angleDiff = Vector3.Angle(plDir, new Vector3(rb.velocity.x, 0, rb.velocity.z).normalized);
        if ( angleDiff > angleOffset)
        {
            Vector3 velocityDiff = Vector3.ClampMagnitude(plDir * maxSpeed, maxSpeed) - Vector3.ClampMagnitude(velNoGrav.normalized * maxSpeed, maxSpeed);
            velocityDiff = velocityDiff * turnPower;
            
            rb.velocity = Vector3.ClampMagnitude(velNoGrav + (velocityDiff), maxSpeed);
        }
        else
        {
            rb.velocity = Vector3.ClampMagnitude(velNoGrav + (plDir * accelerationSpeed * Time.deltaTime), maxSpeed);
        }
        

        if(plDir.magnitude < 0.001)
        {
            float deccel = deccelerationSpeed * Time.deltaTime;
            Vector2 plSpeed = new Vector2(plDir.x, plDir.z);
            plSpeed = Vector2.ClampMagnitude(plSpeed, plSpeed.magnitude - deccel);
            rb.velocity = new Vector3(plSpeed.x,0,plSpeed.y);
        }
        else
        {
            Vector3 rot = Quaternion.LookRotation(rb.velocity.normalized, Vector3.forward).eulerAngles;
            float smoothRot = Mathf.SmoothDampAngle(transform.rotation.eulerAngles.y, rot.y,ref smoothRotVel, smoothRotValue);
            transform.rotation = Quaternion.Euler(new Vector3(0, smoothRot, 0));
        }

        if (!isGrounded)
        {
            Vector3 additiveForce = new Vector3(rb.velocity.x - velNoGrav.x, 0, rb.velocity.z - velNoGrav.z);
            additiveForce = additiveForce * airForceMultiplier;
            rb.velocity = new Vector3(velNoGrav.x + additiveForce.x, gravityVelocity, velNoGrav.z + additiveForce.z);
        }
        else
        {
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y + gravityVelocity, rb.velocity.z);
        }
        

       

        animator.SetFloat("Speed", rb.velocity.magnitude);
        animator.SetFloat("VelY", rb.velocity.y);
        animator.SetBool("IsGrounded", Physics.Raycast(groundCheck.position, Vector3.down, 0.2f, groundMask));
    }

    private bool checkIsGrounded()
    {
        return Physics.Raycast(groundCheck.position, Vector3.down, 0.3f, groundMask);
    }

    public void OnMove(InputAction.CallbackContext callbackContext)
    {
        moveInput = callbackContext.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext callbackContext)
    {
        switch (callbackContext.phase)
        {
            case InputActionPhase.Started:
                if (canJump())
                {
                    isJumping = true;
                    rb.AddForce(Vector3.up * initialJumpPower, ForceMode.VelocityChange);
                    jumpTime = 0;
                }
                break;
            case InputActionPhase.Performed:
                jumpTime += Time.deltaTime;
                if (jumpTime < jumpDuration)
                {
                    rb.AddForce(Vector3.up * (jumpPower * jumpPowerCurveMultiplier.Evaluate(jumpTime/jumpDuration)), ForceMode.Force);
                }
                else
                {
                    isJumping = false;
                }
                break;
            case InputActionPhase.Canceled:
                isJumping = false;
                break;
            default: break;
        }
    }

    private bool canJump()
    {
        return !isJumping && isGrounded;
    }

    



}
