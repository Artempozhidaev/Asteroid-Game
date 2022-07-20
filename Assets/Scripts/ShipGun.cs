using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipGun : MonoBehaviour
{
    [SerializeField]
    private PoolSystem.ObjectInfo.ObjectType bulletType;

    private float time_to_shoot;        // Время до возможности выстрелить
    public float Shooting_delay = 3f;   // Задержка стрельбы / 10

    public AudioSource ShotSound;
    void Update()
    {
        try
        {


            time_to_shoot -= Time.deltaTime;

            if (GameController.MouseControl) 
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if (time_to_shoot <= 0)
                    {
                        Shot();
                    }
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    if (time_to_shoot <= 0)
                    {
                        Shot();
                    }
                }
            }
                
        }
        catch
        {

        }
    }

    void Shot()
    {
        var bullet = PoolSystem.instance.GetObject(bulletType);
        bullet.GetComponent<Bullet_logic>().onCreate(gameObject.transform.position + transform.up * 3.5f, transform.rotation);
        ShotSound.Play();
        time_to_shoot = Shooting_delay / 10;
    }

}
