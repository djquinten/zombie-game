using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public Transform slide;
    public float slideBackDistance = -0.1f;
    public float slideSpeed = 0.1f;

    private Vector3 _slideInitialPosition;
    private bool _isShooting;
    private float _slideTime;
    
    void Start()
    {
        _slideInitialPosition = slide.localPosition;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !_isShooting)
        {
            _isShooting = true;
            _slideTime = 0f;
        }

        if (_isShooting)
        {
            _slideTime += Time.deltaTime;

            // Move the slide back and forth
            if (_slideTime < slideSpeed)
            {
                slide.localPosition = Vector3.Lerp(_slideInitialPosition, _slideInitialPosition - new Vector3(0, 0, slideBackDistance), _slideTime / slideSpeed);
            }
            else if (_slideTime < slideSpeed * 2)
            {
                slide.localPosition = Vector3.Lerp(_slideInitialPosition - new Vector3(0, 0, slideBackDistance), _slideInitialPosition, (_slideTime - slideSpeed) / slideSpeed);
            }
            else
            {
                slide.localPosition = _slideInitialPosition;
                _isShooting = false;
            }
        }
    }
}
