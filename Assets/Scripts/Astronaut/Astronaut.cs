using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Astronaut : MonoBehaviour
{
    private float _oil;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Oil>(out Oil oil))
        {
            _oil++;

            Debug.Log(_oil);

            Destroy(oil.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            SceneManager.LoadScene(0);
        }
    }
}
