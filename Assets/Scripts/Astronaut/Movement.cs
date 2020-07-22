using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]

public class Movement : MonoBehaviour
{
    [SerializeField] private float _startSpeed;
    [SerializeField] private float _speed;
    [SerializeField] private GameObject _smoke;
    [SerializeField] private GameObject _rightNitro;
    [SerializeField] private GameObject _leftNitro;
    [SerializeField] private GameObject _upNitro;
    [SerializeField] private GameObject _downNitro;

    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;

    private bool _isGround;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

        _rigidbody2D.AddForce(Vector2.right * _startSpeed);
    }

    private void Update()
    {
        _animator.SetBool("walk", false);
        _animator.SetBool("fly", false);

        if (Input.GetKey(KeyCode.RightArrow))
        {
            _spriteRenderer.flipX = false;

            if (!_isGround)
            {
                _rightNitro.SetActive(true);

                _rigidbody2D.AddForce(Vector2.right);
            }
            else
            {
                _rightNitro.SetActive(false);

                transform.position = new Vector3(transform.position.x + _speed * Time.deltaTime, transform.position.y, transform.position.z);

                _animator.SetBool("walk", true);
            }
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            _spriteRenderer.flipX = true;

            if (!_isGround)
            {
                _leftNitro.SetActive(true);

                _rigidbody2D.AddForce(Vector2.left);
            }
            else
            {
                _leftNitro.SetActive(false);

                transform.position = new Vector3(transform.position.x - _speed * Time.deltaTime, transform.position.y, transform.position.z);

                _animator.SetBool("walk", true);
            }
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            _animator.SetBool("fly", true);

            _rigidbody2D.AddForce(Vector2.up);

            if (!_isGround)
                _upNitro.SetActive(true);
            else
                _upNitro.SetActive(false);
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            _rigidbody2D.AddForce(Vector2.down);

            if (!_isGround)
                _downNitro.SetActive(true);
            else
                _downNitro.SetActive(false);
        }

        ResetSetActiveNitro();

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent<Ground>(out Ground ground))
        {
            GetSmoke();
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent<Ground>(out Ground ground))
        {
            _animator.SetBool("idle", true);

            _isGround = true;

            _rigidbody2D.drag = 10;

        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent<Ground>(out Ground ground))
        {
            GetSmoke();

            _animator.SetBool("idle", false);

            _isGround = false;

            _rigidbody2D.drag = 0;
        }
    }

    private void GetSmoke()
    {
        _smoke.transform.position = transform.position;

        _smoke.GetComponent<Animator>().SetTrigger("smoke");
    }

    private void ResetSetActiveNitro()
    {
        if (!Input.GetKey(KeyCode.RightArrow))
            _rightNitro.SetActive(false);
        if (!Input.GetKey(KeyCode.LeftArrow))
            _leftNitro.SetActive(false);
        if (!Input.GetKey(KeyCode.UpArrow))
            _upNitro.SetActive(false);
        if (!Input.GetKey(KeyCode.DownArrow))
            _downNitro.SetActive(false);
    }
}

