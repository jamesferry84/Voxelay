using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float xMove = .1f;
    [SerializeField] private float yMove = .1f;
    [SerializeField] private float zMove = .1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            transform.Translate(xMove,yMove,zMove);
        }
    }
}
