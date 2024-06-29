using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 5f;

    [SerializeField] private bool rotateShoot = false;
    
    [SerializeField] Transform player;

    private Vector3 moveDirection;
    private Vector3 initialVelocity;


    public void Initialize(Vector3 direciton, Vector3 playerVelocity)
    {
        initialVelocity = playerVelocity;
        moveDirection = direciton.normalized;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position += transform.forward * speed * Time.deltaTime;
        transform.position += (moveDirection * speed + initialVelocity) * Time.deltaTime;
    }
}