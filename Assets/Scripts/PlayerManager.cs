using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float firingDelay = .5f;

    [SerializeField] private Projectile projectile;

    private Projectile currentProjectile;
    private Projectile currentProjectileTwo;

    // private float zValue = 0f;
    private float yValue = 0f;
    private float elapsed = 0f;

    private float maxHoldTime = 5f;
    private float holdTime = 0f;
    // private float angle;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        elapsed += Time.time;
       

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
        if (Input.GetButtonUp("Fire1") )
        {
            holdTime -= Time.deltaTime;
            holdTime = Mathf.Clamp(holdTime, 0, maxHoldTime);
            float angle = Mathf.Lerp(5, 180, holdTime / maxHoldTime);
            if (angle <= 180f)
            {
                if (elapsed >= firingDelay)
                {
                    elapsed = 0f;
                    Quaternion rotation = Quaternion.Euler(0,angle,0);
                    currentProjectile = Instantiate(projectile, transform.position, rotation);
                }
            }
            
        }


        float xValue = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        float zValue = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;

        transform.Translate(xValue, yValue, zValue);
    }

    void FireProjectileOne()
    {
        float angle = Mathf.Lerp(180, 15, holdTime / maxHoldTime);
        if (elapsed >= firingDelay)
        {
            //elapsed = 0f;
            Quaternion rotation = Quaternion.Euler(0,angle,0);
            currentProjectile = Instantiate(projectile, transform.position, rotation);
        }
    }
    
    void FireProjectileTwo()
    { 
        Debug.Log("Projectile Two");
        float angle = Mathf.Lerp(180, 355, holdTime / maxHoldTime);
        Debug.Log(angle);
        if (elapsed >= firingDelay)
        {
            //elapsed = 0f;
            Quaternion rotation = Quaternion.Euler(0,angle,0);
            currentProjectileTwo = Instantiate(projectile, transform.position, rotation);
        }
    }
}