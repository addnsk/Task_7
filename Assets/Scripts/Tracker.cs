using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tracker : MonoBehaviour
{
    [SerializeField] private GameObject _astronaut;
    [SerializeField] private float _paralaxCoefX;
    [SerializeField] private float _paralaxCoefY;
    [SerializeField] private float _smooth;


    private float _corectedY;
    private float _corectedX;

    private Vector2 _newPos;

    void Update()
    {
        _corectedY = _astronaut.transform.position.y - (_astronaut.transform.position.y * _paralaxCoefY);
        _corectedX = _astronaut.transform.position.x - (_astronaut.transform.position.x * _paralaxCoefX);

        _newPos = new Vector2(_corectedX, _corectedY);
    }

    private void FixedUpdate()
    {
        transform.position = Vector2.Lerp(transform.position, _newPos, _smooth * Time.deltaTime);
    }
}
