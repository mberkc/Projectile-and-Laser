using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
    [SerializeField] float speed;
    [SerializeField] GameObject muzzleFX, hitFX;

    [SerializeField] bool canBounce = true;

    void Start () {
        var projectileTransform = transform;
        GameObject muzzle = Instantiate (muzzleFX, projectileTransform.position, projectileTransform.rotation);
        Destroy (muzzle, 1.5f);
    }

    void Update () {
        if (speed == 0)
            return;
        var projectileTransform = transform;
        projectileTransform.position += projectileTransform.forward * (speed * Time.deltaTime);
    }

    void OnTriggerEnter (Collider other) {

        if (canBounce && Physics.Raycast (transform.position, transform.forward, out var hit)) {

            GameObject hitFx = Instantiate (hitFX, hit.point, Quaternion.FromToRotation (Vector3.up, hit.normal));

            Destroy (hitFx, 1.5f);

            Vector3 reflectDirection = Vector3.Reflect (transform.forward, hit.normal);
            transform.rotation = Quaternion.LookRotation (reflectDirection);
            return;
        }

        Destroy (gameObject);
    }
}