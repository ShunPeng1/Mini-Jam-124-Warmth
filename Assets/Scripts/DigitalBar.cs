using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DigitalBar : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    private Collider2D _collider2D;
    
    
    private bool _isDragging = false;

    [SerializeField] private DigitalBarType barType;
    [SerializeField, HideInInspector] private Vector3 lastPosition;
    [SerializeField] private LayerMask touchingLayer;
    
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _collider2D = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_isDragging)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;

            _rigidbody2D.MovePosition(mousePosition);
        }
        
        
    }

    private void OnMouseDown()
    {
        _isDragging = true;
        lastPosition = transform.position;
    }
    
    
    private void OnMouseUp()
    {
        if (_isDragging == false) return;
        
        _isDragging = false;

        if (_collider2D.IsTouchingLayers(touchingLayer))
        {
            _rigidbody2D.MovePosition(lastPosition);
            
        }
        else
        {
            lastPosition = transform.position;
        }
    }
    
    public void SpawnInit()
    {
        _isDragging = true;
    }


    public void DropSpawned()
    {
        _isDragging = false;

        if (_collider2D.IsTouchingLayers(touchingLayer))
        {
            Destroy(gameObject);
            ScoreManager.Instance.IncreaseBarNumber(barType, 1);
        }
        else
        {
            lastPosition = transform.position;
        }
    }
}
