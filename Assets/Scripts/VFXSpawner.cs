using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXSpawner : MonoBehaviour {
    [SerializeField] Transform spawnTarget;
    [SerializeField] GameObject projectilePrefab, laserPrefab;

    GameObject laser;

    int projectileFireRate = 4;
    float canFireProjectile;

    void Start () {
        laser = Instantiate (laserPrefab, spawnTarget.transform);
        laser.transform.parent = spawnTarget;
        DeactivateLaser ();
    }

    void Update () {

#if UNITY_STANDALONE || UNITY_EDITOR
        if (Input.GetMouseButton (0) && Time.time >= canFireProjectile) {
            canFireProjectile = Time.time + (1f / projectileFireRate);
            SpawnProjectile ();
        }
        if (Input.GetKeyDown (KeyCode.Space)) {
            ActivateLaser ();
        } else if (Input.GetKeyUp (KeyCode.Space)) {
            DeactivateLaser ();
        }

#elif UNITY_ANDROID
        if (Input.GetMouseButtonDown (0) && Time.time >= canFireProjectile) {
            canFireProjectile = Time.time + (1f / projectileFireRate);
            SpawnProjectile ();
        }
        if (Input.GetMouseButton (0)) {
            ActivateLaser ();
        } else if (Input.GetMouseButtonUp (0)) {
            DeactivateLaser ();
        }
#endif
    }

    void SpawnProjectile () {
        Instantiate (projectilePrefab, spawnTarget.position, spawnTarget.rotation);
    }

    void ActivateLaser () {
        laser.SetActive (true);
    }

    void DeactivateLaser () {
        laser.SetActive (false);
    }
}