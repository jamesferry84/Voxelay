using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Renderer meshRenderer;
    private Color originalColor;

    private void Awake()
    {
        meshRenderer = GetComponentInChildren<MeshRenderer>();
        originalColor = meshRenderer.sharedMaterial.color;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        meshRenderer.sharedMaterial.color = Color.red;
    }

    private void OnTriggerExit(Collider other)
    {
        meshRenderer.sharedMaterial.color = originalColor;
    }
}
