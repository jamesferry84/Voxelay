﻿using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Managers
{
    public enum WeaponTypes
    {
        SingleStraight = 0,
        SingleMissile = 1,
        OrbitalShooter = 2
    }

    public class WeaponManager : MonoBehaviour
    {
        [SerializeField] private float firingDelay = .5f;

        [SerializeField] private Projectile singleStraightProjectile;
        [SerializeField] private Projectile singleMissileProjectile;
        [SerializeField] private Projectile orbitalShooterProjectile;

        private Projectile currentProjectileTwo;
        public static WeaponManager Instance { get; private set; }
        private WeaponTypes currentWeaponType = WeaponTypes.SingleStraight;
        private Projectile currentProjectile;
        private float elapsed = 0f;
        private bool isHolding = false;
        private float maxHoldTime = 5f;
        private float holdTime = 0f;
        private float currentAngle = 0f;
        private float xValue;
        private float zValue;
        private float angleTest = 180f;
        public float angleSpeed = 10f;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }
        }

        public void ChangeGun(int choice)
        {
            if (Enum.IsDefined(typeof(WeaponTypes), choice))
            {
                currentWeaponType = (WeaponTypes)choice;
            }
            else
            {
                currentWeaponType = WeaponTypes.SingleStraight;
            }
        }

        private IEnumerator FireProjectile(Projectile projectile)
        {
            if (Input.GetButton("Fire1") && elapsed >= firingDelay)
            {
                // Renderer projectileRenderer = singleStraightProjectile.GetComponentInChildren<MeshRenderer>();
                // float randomRed = Random.Range(0f, 1f);
                // float randomGreen = Random.Range(0f, 1f);
                // float randomBlue = Random.Range(0f, 1f);
                // float alpha = 1f;
                // projectileRenderer.sharedMaterial.color = new Color(randomRed,randomGreen,randomBlue,alpha);
                currentProjectile = Instantiate(projectile, transform.position, Quaternion.identity);
                currentProjectile.Initialize(Vector3.forward, new Vector3(0f, 0f, 5f));
                elapsed = 0f;
            }

            yield return new WaitForSeconds(firingDelay);
        }

        private void Update()
        {
            Debug.Log("isHolding: " + isHolding);
            elapsed += Time.time;


            switch (currentWeaponType)
            {
                case WeaponTypes.SingleStraight:
                {
                    firingDelay = 600f;
                    StartCoroutine(FireProjectile(singleStraightProjectile));
                    break;
                }
                case WeaponTypes.SingleMissile:
                {
                    break;
                }
                case WeaponTypes.OrbitalShooter:
                {
                    firingDelay = 300f;
                    OrbitalProjectileFire();
                    break;
                }
                default:
                {
                    Debug.Log("No Shooter type");
                    break;
                }
            }
        }

        private void OrbitalProjectileFire()
        {
            if (Input.GetButtonDown("Fire1"))
            {
                holdTime = 0f;
                isHolding = true;
            }


            if (Input.GetButton("Fire1"))
            {
                holdTime += Time.deltaTime;
                holdTime = Mathf.Clamp(holdTime, 0, maxHoldTime);
                if (elapsed >= firingDelay)
                {
                    FireProjectileOne(orbitalShooterProjectile);
                    FireProjectileTwo(orbitalShooterProjectile);
                    elapsed = 0f;
                }
            }

            if (Input.GetButtonUp("Fire1"))
            {
                isHolding = false;
            }

            if (isHolding == false)
            {
                holdTime -= Time.deltaTime;
                holdTime = Mathf.Clamp(holdTime, 0, maxHoldTime);
                float angle = Mathf.Lerp(5, 180, holdTime / maxHoldTime);
                // Debug.Log("isHolding == false and angle = " + angle);
                if (angle <= 180f)
                {
                    if (elapsed >= firingDelay)
                    {
                        FireProjectileOne(orbitalShooterProjectile);
                        FireProjectileTwo(orbitalShooterProjectile);
                        elapsed = 0f;
                    }
                }
            }
            
        }

        void FireProjectileOne(Projectile projectile)
        {
            Debug.Log("Firing ProjectileOne");
            if (angleTest <= 180)
            {
                angleTest += angleSpeed;
            }
            float angle = Mathf.Lerp(180, 5, holdTime / maxHoldTime);
            
            if (elapsed >= firingDelay)
            {
                FireProjectileWithDirectiom(projectile, angle);
            }
        }

        private void FireProjectileWithDirectiom(Projectile projectile, float angle)
        {
            Quaternion rotation = Quaternion.Euler(0, angle, 0);
            currentProjectile = Instantiate(projectile, transform.position, rotation);
            Vector3 direction = rotation * Vector3.forward;
            Vector3 playerVelocity = new Vector3(xValue, 0f, zValue) * Time.deltaTime;
            currentProjectile.Initialize(direction, playerVelocity);
        }

        void FireProjectileTwo(Projectile projectile)
        {
            float angle = Mathf.Lerp(180, 355, holdTime / maxHoldTime);
            if (elapsed >= firingDelay)
            {
                FireProjectileWithDirectiom(projectile, angle);
            }
        }
    }
}