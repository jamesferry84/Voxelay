using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using TextMeshProUGUI = TMPro.TextMeshProUGUI;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float firingDelay = .5f;

    [SerializeField] private Projectile projectile; 
    private int currentGun;

    [SerializeField] private TextMeshProUGUI[] guns;

    private Projectile currentProjectile;
    private Projectile currentProjectileTwo;

    // private float zValue = 0f;
    private float yValue = 0f;
    private float elapsed = 0f;

    private float maxHoldTime = 5f;
    private float holdTime = 0f;
    // private float angle;

    // Start is called before the first frame update

    private bool isHolding = false;
    private float xValue;
    private float zValue;

    private float currentAngle = 0f;
    public float rollFactor = -40f;
    

    void Start()
    {
        if (guns == null)
        {
            guns = new TextMeshProUGUI[] {};
        }

        currentGun = 0;
        guns[currentGun].color = Color.red;
    }

    // Update is called once per frame
    void Update()
    {
        elapsed += Time.time;

        if (Input.GetButtonDown("Fire3"))
        {
            guns[currentGun].color = Color.white;

            currentGun++;
            if (currentGun >= guns.Length)
            {
                currentGun = 0;
            }
            guns[currentGun].color = Color.red;
        }

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
                FireProjectileOne();
                FireProjectileTwo();
                elapsed = 0f;
            }
        }

        if (Input.GetButtonUp("Fire1"))
        {
            isHolding = false;
            holdTime = 0f;
            holdTime -= Time.deltaTime;
            holdTime = Mathf.Clamp(holdTime, 0, maxHoldTime);
            float angle = Mathf.Lerp(5, 180, holdTime / maxHoldTime);
            if (angle <= 180f)
            {
                if (elapsed >= firingDelay)
                {
                    elapsed = 0f;
                    Quaternion rotation = Quaternion.Euler(0, angle, 0);
                    currentProjectile = Instantiate(projectile, transform.position, rotation);
                }
            }
        }

        float xRoll = Input.GetAxis("Horizontal");
        xValue = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        zValue = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        float roll = xRoll * rollFactor;

        transform.localRotation = Quaternion.Euler(0f, 0f, roll);

        // transform.Translate(xValue, 0, zValue);
        transform.position = transform.position += new Vector3(xValue, 0, zValue);
    }

    void FireProjectileOne()
    {
        float angle;

        angle = Mathf.Lerp(180, 15, holdTime / maxHoldTime);

        if (!isHolding)
        {
            angle *= -1;
        }

        if (elapsed >= firingDelay)
        {
            //elapsed = 0f;
            Quaternion rotation = Quaternion.Euler(0, angle, 0);
            currentProjectile = Instantiate(projectile, transform.position, rotation);
            Vector3 direction = rotation * Vector3.forward;
            Vector3 playerVelocity = new Vector3(xValue, 0f, zValue) * Time.deltaTime;
            currentProjectile.Initialize(direction, playerVelocity);
        }
    }

    void FireProjectileTwo()
    {
        float angle;
        angle = Mathf.Lerp(180, 355, holdTime / maxHoldTime);
        Debug.Log(angle);
        if (elapsed >= firingDelay)
        {
            //elapsed = 0f;
            Quaternion rotation = Quaternion.Euler(0, angle, 0);
            currentProjectileTwo = Instantiate(projectile, transform.position, rotation);
            Vector3 direction = rotation * Vector3.forward;
            Vector3 playerVelocity = new Vector3(xValue, 0f, zValue) * Time.deltaTime;
            currentProjectileTwo.Initialize(direction, playerVelocity);
        }
    }
}