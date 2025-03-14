using System;
using UnityEngine;

public class DummyIceberg : MonoBehaviour
{

    [SerializeField] private float speed = 3f;
    
    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}