using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private float speed = 3;
    private Vector3 direction;

    void Update()
    {
        transform.position += direction * (Time.deltaTime * speed);
    }

    public void SetDirection(Vector3 direction)
    {
        this.direction = direction;
    }
}