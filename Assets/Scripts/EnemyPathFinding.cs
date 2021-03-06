﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathFinding : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _distance;
    [SerializeField] private Transform _detector;

    private RaycastHit2D _groundInfo;
    private bool _isLeft;


    private void Update()
    {
        transform.Translate(Vector2.left * _speed * Time.deltaTime);

        _groundInfo = Physics2D.Raycast(_detector.position, Vector2.down, _distance);

        if (!_groundInfo)
        {
            if (_isLeft)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                _isLeft = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                _isLeft = true;
            }
        }
    }
}
