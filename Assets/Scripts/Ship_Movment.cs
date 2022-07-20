using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship_Movment : MonoBehaviour
{
    public float acceleration = 50;
    public float maxSpeed = 50;
    public float rotationSpeed = 400;

    private Vector3 currentMove = Vector3.zero; 
    private float currentscale = 3;

    public AudioSource ThrustEff;
    
    void Update()
    {
        try
        {
            if (GameController.MouseControl)
            {
                if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) || Input.GetMouseButton(1))
                {
                    ShipForward();
                    
                }
                else
                {
                    if (ThrustEff.isPlaying)
                        ThrustEff.Stop();
                }
                    
                MouseShipRotation();
            }
            else
            {
                if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
                {
                    ShipForward();
                }
                else
                {
                    if (ThrustEff.isPlaying)
                        ThrustEff.Stop();
                }


                if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
                {
                    ShipRotation(true);
                }
                else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
                {
                    ShipRotation(false);
                }
            }

            
            
            transform.position += currentMove * Time.deltaTime;
            ScreenPosition();
        }
        catch
        {

        }
    }

    void MouseShipRotation()
    {

        var aimTarget = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var aimDir = aimTarget - transform.position;
        aimDir.z = transform.up.z;
        transform.up = Vector3.MoveTowards(transform.up, aimDir, rotationSpeed / 100 * Time.deltaTime);

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
    //Движение корабля вперед
    void ShipForward()
    {
        currentMove = Vector3.ClampMagnitude(currentMove + transform.up * Time.deltaTime * acceleration, maxSpeed);
        if (!ThrustEff.isPlaying)
            ThrustEff.Play();
    }
    //Поворот корабля
    void ShipRotation(bool direction)
    {
        // Если значение Direction = true, тогда поворот влево, иначе вправо

        if (direction)  //Влево
        {
            transform.rotation *= Quaternion.AngleAxis(rotationSpeed / 2 * Time.deltaTime, transform.forward);
        }
        else            //Вправо
        {
            transform.rotation *= Quaternion.AngleAxis(-rotationSpeed / 2 * Time.deltaTime, transform.forward);
        }
    }
}
