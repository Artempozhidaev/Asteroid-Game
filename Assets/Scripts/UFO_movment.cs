using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFO_movment : MonoBehaviour
{
    public float Speed = 5;

    private float currentscale = 5;
    private Vector3 Direction = Vector3.zero;
    void Start()
    {
        if (transform.position.x < 0)
        {
            Direction = -transform.right;
        }
        else
        {
            Direction = transform.right;
        }
    }

    void Update()
    {
        transform.position += Direction * Speed * Time.deltaTime;
        ScreenPosition();
    }

    void ScreenPosition()
    {
        if (transform.position.x < -90 - currentscale / 2)
            transform.position = new Vector3(transform.position.x + (180f + currentscale), transform.position.y, transform.position.z);
        else if (transform.position.x > 90 + currentscale / 2)
            transform.position = new Vector3(transform.position.x - (180f + currentscale), transform.position.y, transform.position.z);

        if (transform.position.y < -50 - currentscale / 2)
            transform.position = new Vector3(transform.position.x, transform.position.y + (100 + currentscale), transform.position.z);
        else if (transform.position.y > 50 + currentscale / 2)
            transform.position = new Vector3(transform.position.x, transform.position.y - (100 + currentscale), transform.position.z);
    }
}
