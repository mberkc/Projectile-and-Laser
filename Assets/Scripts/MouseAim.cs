using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseAim : MonoBehaviour {
    Camera mainCam;

    Ray mouseRay;
    Vector3 position, direction;
    Quaternion rotation;

    void Awake () {
        mainCam = Camera.main;
    }

    void Update () {
        Vector3 mousePos = Input.mousePosition;
        mouseRay = mainCam.ScreenPointToRay (mousePos);
        LookTarget (Physics.Raycast (mouseRay.origin, mouseRay.direction, out var hit, 1000f) ?
            hit.point :
            mouseRay.GetPoint (100f));
    }

    void LookTarget (Vector3 target) {
        direction = target - transform.position;
        rotation = Quaternion.LookRotation (direction);
        transform.localRotation = Quaternion.Lerp (transform.rotation, rotation, 1);
    }
}