using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[ExecuteInEditMode]
public class LookAtCamera : MonoBehaviour
{
    public bool invert;
    public bool update;
    public bool lookForMainCamera;

    public Transform cameraTransform;
    private Vector3 direction;
    private Quaternion rotation;
    // Start is called before the first frame update
    void Start()
    {
        if (lookForMainCamera)
            cameraTransform = Camera.main.transform;
        //cameraTransform = FindObjectOfType<OVRCameraRig>().transform;
        OnEnable();
    }

    void OnEnable()
    {
        if(!cameraTransform) cameraTransform = Camera.main.transform;

        direction = cameraTransform.position - transform.position;
        rotation = Quaternion.LookRotation(invert ? - direction : direction, Vector3.up);
        transform.rotation = rotation;
        enabled = update;
    }

    void Update()
    {
        
        direction = cameraTransform.position - transform.position;
        rotation = Quaternion.LookRotation(invert ? -direction : direction, Vector3.up);
        transform.rotation = rotation;
    }
}
