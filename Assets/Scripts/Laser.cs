using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour {
    [SerializeField] bool canBounce;

    LineRenderer lineRenderer;
    Quaternion lastRotation;
    float laserRange = 25f; // Last section of Linerenderer

    void Awake () {
        lineRenderer = GetComponentInChildren<LineRenderer> ();
    }

    void Start () {
        UpdateLaser ();
    }

    void Update () {
        UpdateLaser ();
    }

    void UpdateLaser () {
        if (lastRotation == transform.rotation)
            return;
        Transform laserTransform = transform;
        lastRotation = laserTransform.rotation;

        Vector3 origin = laserTransform.position;
        int index = 0;
        Vector3 direction = laserTransform.forward;
        while (Physics.Raycast (origin, direction, out var hit)) {
            lineRenderer.SetPosition (index++, origin);
            lineRenderer.SetPosition (index++, hit.point);
            origin = hit.point;
            direction = Vector3.Reflect (direction, hit.normal);
        }
        if (index == 0)
            lineRenderer.SetPosition (index++, origin);
        if (index >= lineRenderer.positionCount)
            return;
        Vector3 targetPos = direction * laserRange;
        lineRenderer.SetPosition (index++, targetPos);

        for (int i = index; i < lineRenderer.positionCount; i++)
            lineRenderer.SetPosition (i, targetPos);
    }
}