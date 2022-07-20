using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_logic : MonoBehaviour, IPooledObject
{
    public float Speed = 10;

    public float Max_distan = 180;

    public int Points_UFO = 200;
    public int Points_A_small = 100;
    public int Points_A_medium = 50;
    public int Points_A_big = 20;

    public PoolSystem.ObjectInfo.ObjectType Type => type;

    [SerializeField]
    private PoolSystem.ObjectInfo.ObjectType type;

    private void Start()
    {
        Max_distan = 180;
    }
    void Update()
    {
        transform.position += transform.up * Speed * Time.deltaTime;
        ScreenPosition();

        Max_distan -=  Speed * Time.deltaTime;
        if (tag == "Bullet(player)")
            UpdateDistanceToDestroy();
        else
            UpdateDistanceToDestroy();
    }

    public void onCreate(Vector2 position, Quaternion rotation)
    {
        transform.position = position;
        transform.rotation = rotation;
        Max_distan = 180;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (CompareTag("Bullet(player)"))
        {
            if (collision.gameObject.CompareTag("Asteroid(Big)"))
            {
                PoolSystem.instance.DestroyObject(gameObject);
                collision.gameObject.GetComponent<Asteroid_logic>().IfCrshed();

                Score.Instance.UpdateScore(Points_A_big);

            }
            else if (collision.gameObject.CompareTag("Asteroid(Medium)"))
            {
                PoolSystem.instance.DestroyObject(gameObject);
                collision.gameObject.GetComponent<Asteroid_logic>().IfCrshed();

                Score.Instance.UpdateScore(Points_A_medium);

            }
            else if (collision.gameObject.CompareTag("Asteroid(Small)"))
            {
                PoolSystem.instance.DestroyObject(gameObject);
                collision.gameObject.GetComponent<Asteroid_logic>().IfCrshed();

                Score.Instance.UpdateScore(Points_A_small);

            }
            else if (collision.gameObject.CompareTag("UFO"))
            {
                PoolSystem.instance.DestroyObject(gameObject);

                Destroy(collision.gameObject);
                Object_spawn._timerOn = true;

                Score.Instance.UpdateScore(Points_UFO);
            }
            else 
            {
                PoolSystem.instance.DestroyObject(gameObject);
            }
        }
        else
        {
            if (collision.gameObject.CompareTag("UFO"))
            {
                PoolSystem.instance.DestroyObject(gameObject);
            }
        }
    }

    void UpdateDistanceToDestroy()
    {
        if (Max_distan <= 0)
        {
            PoolSystem.instance.DestroyObject(gameObject);
        }
    }
    void ScreenPosition()
    {
        if (transform.position.x < -91)
            transform.position = new Vector3(transform.position.x + 182f, transform.position.y, transform.position.z);
        else if (transform.position.x > 91)
            transform.position = new Vector3(transform.position.x - 182f, transform.position.y, transform.position.z);

        if (transform.position.y < -51)
            transform.position = new Vector3(transform.position.x, transform.position.y + 102, transform.position.z);
        else if (transform.position.y > 51)
            transform.position = new Vector3(transform.position.x, transform.position.y - 102, transform.position.z);
    }
}
