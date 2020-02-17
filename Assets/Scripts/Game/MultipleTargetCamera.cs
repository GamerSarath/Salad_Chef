using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera)) ]
public class MultipleTargetCamera : MonoBehaviour
{
    [SerializeField]
    private GameObject[] targets;
    public Vector3 offset;
    private Vector3 velocity;
    [SerializeField]
    float smoothTime = 0.5f;
    public float minZoom = 50f;
    public float maxZoom = 10f;
    [SerializeField]
    private float zoomLimitter = 50f;
    private Camera cam;
    void Start()
    {
        cam = GetComponent<Camera>();
        targets = new GameObject[2];
        targets = GameObject.FindGameObjectsWithTag("Target");
    }
    void LateUpdate()
    {
        Move();
        Zoom();

    }
    void Move()
    {
            if (targets.Length == 0)
                return;
            Vector3 centerPoint = GetCenterPoint();
            Vector3 newPosition = centerPoint + offset;
            transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothTime);
        
    }

    void Zoom()
    {
        float newZoom = Mathf.Lerp(maxZoom, minZoom, GetGreatestDistance() / zoomLimitter);
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, newZoom,Time.deltaTime);
    }

    float GetGreatestDistance()
    {
        var bounds = new Bounds(targets[0].transform.position, Vector3.zero);
        for (int i = 0; i < targets.Length; i++)
        {
            bounds.Encapsulate(targets[i].transform.position);
        }
        return bounds.size.z;
    }
    Vector3 GetCenterPoint()
    {
        if(targets.Length == 1)
        {
            return targets[0].transform.position;
        }

        var bounds = new Bounds(targets[0].transform.position, Vector3.zero);
        for(int i= 0; i < targets.Length; i++)
        {
            bounds.Encapsulate(targets[i].transform.position);
        }
        return bounds.center;
    }
       
}
