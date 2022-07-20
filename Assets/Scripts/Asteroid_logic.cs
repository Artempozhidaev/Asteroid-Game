using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid_logic : MonoBehaviour, IPooledObject
{
    public static Asteroid_logic Instance;
    public PoolSystem.ObjectInfo.ObjectType Type => type;

    [SerializeField]
    private PoolSystem.ObjectInfo.ObjectType type;

    private float Speed_force;

    public float ScaleBig;
    public float ScaleMedium;
    public float ScaleSmall;

    public float minSpeed = 0;
    public float maxSpeed = 0;

    private bool _isBig;
    private bool _isMedium;
    private bool _isSmall;

    private float currentscale = 0;

    public void Awake()
    {
        Instance = this;
    }
    public void OnCreate(Vector2 position, Quaternion rotation, int size, float speed)
    {

        Speed_force = speed;
        transform.position = position;
        transform.rotation = rotation;
        if (size < 1) // small
        {
            currentscale = ScaleSmall;

            _isBig = false;
            _isMedium = false;
            _isSmall = true;

            transform.tag = "Asteroid(Small)";

            transform.localScale = new Vector3(ScaleSmall, ScaleSmall, 1);
        }
        else if (size == 1) // medium
        {
            currentscale = ScaleMedium;

            _isBig = false;
            _isMedium = true;
            _isSmall = false;

            transform.tag = "Asteroid(Medium)";

            transform.localScale = new Vector3(ScaleMedium, ScaleMedium, 1);
        }
        else //big
        {
            currentscale = ScaleBig;

            _isBig = true;
            _isMedium = false;
            _isSmall = false;

            transform.tag = "Asteroid(Big)";

            transform.localScale = new Vector3(ScaleBig, ScaleBig, 1);
        }
    }
    public void OnCreate(Vector2 position, Quaternion rotation, int size)
    {

        Speed_force = GetRandomSpeed();
        transform.position = position;
        transform.rotation = rotation;
        if (size < 1) // small
        {
            currentscale = ScaleSmall;

            _isBig = false;
            _isMedium = false;
            _isSmall = true;

            transform.tag = "Asteroid(Small)";

            transform.localScale = new Vector3(ScaleSmall, ScaleSmall, 1);
        }
        else if (size == 1) // medium
        {
            currentscale = ScaleMedium;

            _isBig = false;
            _isMedium = true;
            _isSmall = false;

            transform.tag = "Asteroid(Medium)";

            transform.localScale = new Vector3(ScaleMedium, ScaleMedium, 1);
        }
        else //big
        {
            currentscale = ScaleBig;

            _isBig = true;
            _isMedium = false;
            _isSmall = false;

            transform.tag = "Asteroid(Big)";

            transform.localScale = new Vector3(ScaleBig, ScaleBig, 1);
        }
    }
    void Update()
    {
        transform.Translate(transform.up * Time.deltaTime * Speed_force);

        if (transform.position.x < -90 - currentscale /2)
            transform.position = new Vector3(transform.position.x + (180f + currentscale), transform.position.y, transform.position.z);
        else if (transform.position.x > 90 + currentscale / 2)
            transform.position = new Vector3(transform.position.x - (180f + currentscale), transform.position.y, transform.position.z);

        if (transform.position.y < -50 - currentscale / 2)
            transform.position = new Vector3(transform.position.x, transform.position.y + (100 + currentscale), transform.position.z);
        else if (transform.position.y > 50 + currentscale / 2)
            transform.position = new Vector3(transform.position.x, transform.position.y - (100 + currentscale), transform.position.z);

    }

    private float GetRandomSpeed()
    {
        float Speed = Random.Range(minSpeed, maxSpeed);
        return Speed;
    }


    public void IfCrshed()
    {
        Vector3 positionLeft = transform.up + -transform.right + transform.position;
        Vector3 positionRight = transform.up + transform.right + transform.position;

        float _speed = GetRandomSpeed();

        if (_isBig)
        {
            

            var Asteroid1 = PoolSystem.instance.GetObject(type);
            Asteroid1.GetComponent<Asteroid_logic>().OnCreate(positionLeft, transform.rotation, 1, _speed);

            var Asteroid2 = PoolSystem.instance.GetObject(type);
            Asteroid2.GetComponent<Asteroid_logic>().OnCreate(positionRight, transform.rotation, 1, _speed);

            PoolSystem.instance.DestroyObject(gameObject);

            var ownLocalAngles = transform.localEulerAngles;

            ownLocalAngles.z += -22.5f;
            Asteroid2.transform.localEulerAngles = ownLocalAngles;

            ownLocalAngles.z += 45f;
            Asteroid1.transform.localEulerAngles = ownLocalAngles;

            Object_spawn.asteroidsOnScene++;
        }
        else if (_isMedium)
        {

            var Asteroid1 = PoolSystem.instance.GetObject(type);
            Asteroid1.GetComponent<Asteroid_logic>().OnCreate(positionLeft, transform.rotation, 0, _speed);

            var Asteroid2 = PoolSystem.instance.GetObject(type);
            Asteroid2.GetComponent<Asteroid_logic>().OnCreate(positionRight, transform.rotation, 0, _speed);

            PoolSystem.instance.DestroyObject(gameObject);

            var ownLocalAngles = transform.localEulerAngles;

            ownLocalAngles.z += -22.5f;
            Asteroid2.transform.localEulerAngles = ownLocalAngles;

            ownLocalAngles.z += 45f;
            Asteroid1.transform.localEulerAngles = ownLocalAngles;

            Object_spawn.asteroidsOnScene++;

        }
        else if (_isSmall)
        {
            PoolSystem.instance.DestroyObject(gameObject);

            Object_spawn.asteroidsOnScene--;
        }

    }

    public void EndGame()
    {
        PoolSystem.instance.DestroyObject(gameObject);
    }
}