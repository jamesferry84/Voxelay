using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float firingDelay = .5f;

    [SerializeField] private Projectile projectile;

    // private float zValue = 0f;
    private float yValue = 0f;
    private float elapsed = 0f;
    

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        elapsed += Time.time;
        if (elapsed >= firingDelay)
        {
            if (Input.GetButton("Fire1"))
            {
                Instantiate(projectile, transform.position, Quaternion.Euler(90,0,0));
            }

            elapsed = 0f;
        }
        

        float xValue = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        float zValue = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;

        transform.Translate(xValue, yValue, zValue);
    }
}