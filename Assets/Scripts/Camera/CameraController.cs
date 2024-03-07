using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor.Overlays;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private GameObject target;

    [SerializeField]
    private int currentCameraID = 0;

    [SerializeField]
    [Min(0f)]
    private float sensitivity = 1;

    public bool useCPoints;
    private CameraPoint CurrentCamPoint;

    Camera cam;

    [SerializeField] private CamControlData[] CamDatas = new CamControlData[0];
    private CamControlData currentCCD;
    private CameraData cData;

    [SerializeField]
    private float transitionTime = 0.2f;

    private float tTime = 1;

    private Vector3 vel;

    private Vector3 lastPos;

    private PlayerInput pInput;

    private Vector2 lookVectorInput;

    private Vector3 lookRotation = new Vector3();

    private CameraData oldCData;

    public List<CameraPoint> cpList;

    public LayerMask ignoreCamPlacement;

    [SerializeField]
    private AnimationCurve posTransitionCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);




    void Start()
    {
        cam = GetComponent<Camera>();
        swapCamViaID(currentCameraID);
        cam.fieldOfView = cData.FOV;
        lookRotation = transform.rotation.eulerAngles;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        

    }

    private void Update()
    {
        if (useCPoints) { swapCam(getCurrentData()); }
        CamUpdate();
        if (tTime != 1) { transitionUpdate(); }
    }

    private CameraData getCurrentData()
    {
        if (cpList.Count > 0)
        {
            CameraPoint currentCP = null;
            foreach (CameraPoint cp in cpList)
            {
                if (currentCP == null || Vector3.Distance(cp.transform.position, target.transform.position) < Vector3.Distance(currentCP.transform.position, target.transform.position))
                {
                    currentCP = cp;
                }
            }
            CurrentCamPoint = currentCP;
            return currentCP.data.getcData();
        }
        else
        {
            return currentCCD.getcData();
        }
    }

    public void onLook(InputAction.CallbackContext callbackContext)
    {
        lookVectorInput = callbackContext.ReadValue<Vector2>();
        lookVectorInput = cData.invertedLook ? lookVectorInput * -1 : lookVectorInput; //Invert input
    }

    public void CameraChange(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            currentCameraID = (currentCameraID + 1) % CamDatas.Length;
            swapCamViaID(currentCameraID);

        }
    }

    void swapCamViaID(int id)
    {
        oldCData = cData;
        currentCCD = CamDatas[id];
        swapCam(currentCCD.getcData());
    }

    void swapCam(CameraData cDataT)
    {
        if (cDataT != cData)
        {
            oldCData = cData;
            cData = cDataT;

            cam.cullingMask = cData.cullingMask;
            cam.orthographic = cData.isOrthographic;

            if (cData.typeOfRotation == RotationType.UseMouse)
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
            tTime = 0;
            lastPos = transform.position;
        }
    }

    void CamUpdate()
    {
        Vector3 baseRot = transform.rotation.eulerAngles;
        Vector3 basePos = transform.position;
        //Rotation
        switch (cData.typeOfRotation)
        {
            case RotationType.Static:
                lookRotation = cData.staticRotation;
                break;
            case RotationType.UseTransformRotation:
                lookRotation = cData.transformToCopyRotation.rotation.eulerAngles;
                break;
            case RotationType.LookTarget:
                Transform trgt = target.transform;
                switch (cData.targetToLookAt)
                {
                    case TargetType.Custom:
                        trgt = cData.lookTarget;
                        break;
                    default: break;
                }
                lookRotation = Quaternion.LookRotation((trgt.position - transform.position).normalized).eulerAngles;
                //transform.LookAt(trgt);
                break;
            case RotationType.UseMouse:
                Vector3 cRotation = lookRotation;
                Vector2 lvi = lookVectorInput * Time.deltaTime;
               
                cRotation = new Vector3(cRotation.x + (-lvi.y * (sensitivity * cData.sensitivityMultiplier)), cRotation.y + (lvi.x * (sensitivity * cData.sensitivityMultiplier)), cRotation.z);
                //cRotation *= Time.deltaTime;
                lookRotation = cRotation;
                break;
            default: break;
        }

        //Clamp Rotation
        lookRotation.x = cData.clampXRotation.clamp ? Mathf.Clamp(lookRotation.x, cData.clampXRotation.min, cData.clampXRotation.max) : lookRotation.x;
        lookRotation.y = cData.clampYRotation.clamp ? Mathf.Clamp(lookRotation.y, cData.clampYRotation.min, cData.clampYRotation.max) : lookRotation.y;
        lookRotation.z = cData.clampZRotation.clamp ? Mathf.Clamp(lookRotation.z, cData.clampZRotation.min, cData.clampZRotation.max) : lookRotation.z;

        //Lock rotation
        lookRotation.x = cData.lockRotation.x ? baseRot.x : lookRotation.x;
        lookRotation.y = cData.lockRotation.y ? baseRot.y : lookRotation.y;
        lookRotation.z = cData.lockRotation.z ? baseRot.z : lookRotation.z;

        //nextRot = Quaternion.Euler(nextRotEuler);
        transform.rotation = Quaternion.Euler(lookRotation);

        Vector3 posOffset = Vector3.zero;
        //Position offset calculating
        switch (cData.positionOffsetType)
        {
            case PositionOffsetType.Static:
                posOffset = cData.positionOffset; break;
            case PositionOffsetType.CamRotBased:
                posOffset = cam.transform.rotation * cData.positionOffset; break;
            case PositionOffsetType.TargetRotBased:
                posOffset = target.transform.rotation * cData.positionOffset; break;
            default: break;
        }

        //Position
        switch (cData.typeOfPosition)
        {
            case PositionType.Static:
                transform.position = cData.staticPosition + posOffset;
                break;
            case PositionType.FollowTransform:
                Transform trgt = target.transform;
                if (cData.targetToBeAt == TargetType.Custom)
                {
                    trgt = cData.transformToCopyPosition;
                }
                transform.position = Vector3.SmoothDamp(transform.position, trgt.transform.position +posOffset, ref vel, cData.smoothTimePosition);
                break;
            case PositionType.RotateAroundTarget:
                Vector3 tpos = target.transform.position + posOffset;
                float newDist = cData.CamDistance;
                RaycastHit hit;
                if (Physics.Raycast(tpos, (transform.rotation * Vector3.back), out hit,newDist, ignoreCamPlacement))
                {
                    newDist = hit.distance - 0.1f;
                }
                Vector3 rPos = (transform.rotation * Vector3.back) * newDist;
                transform.position = Vector3.SmoothDamp(transform.position, tpos + rPos, ref vel, cData.smoothTimePosition);
                break;
            default: break;

        }



        Vector3 position = transform.position;
        //Clamp position
        position.x = cData.clampXPosition.clamp ? Mathf.Clamp(position.x, cData.clampXPosition.min, cData.clampXPosition.max) : position.x;
        position.y = cData.clampYPosition.clamp ? Mathf.Clamp(position.y, cData.clampYPosition.min, cData.clampYPosition.max) : position.y;
        position.z = cData.clampZPosition.clamp ? Mathf.Clamp(position.z, cData.clampZPosition.min, cData.clampZPosition.max) : position.z;

        //Lock position
        position.x = cData.lockPosition.x ? basePos.x : position.x;
        position.y = cData.lockPosition.y ? basePos.y : position.y;
        position.z = cData.lockPosition.z ? basePos.z : position.z;

        transform.position = position;
    }

    private void transitionUpdate()
    {
        if (oldCData != null)
        {
            tTime = Mathf.Clamp(tTime + Time.deltaTime / transitionTime, 0, 1);
            cam.fieldOfView = Mathf.Lerp(oldCData.FOV, cData.FOV, tTime);
            cam.orthographicSize = Mathf.Lerp(oldCData.FOV, cData.FOV, tTime);
            transform.position = Vector3.Lerp(lastPos, transform.position, posTransitionCurve.Evaluate(tTime));
        }
        else
        {
            tTime = 1;
        }
    }

    public void AddCamPointTrigger(CameraPoint cps)
    {
        cpList.Add(cps);
    }

    public void RemoveCamPointTrigger(CameraPoint cps)
    {
        while (cpList.Contains(cps))
        {
            cpList.Remove(cps);
        }
    }
}
