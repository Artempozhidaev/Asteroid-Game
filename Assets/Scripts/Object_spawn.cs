using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_spawn : MonoBehaviour
{
    public static Object_spawn Instance;

    [SerializeField]
    private PoolSystem.ObjectInfo.ObjectType type;  // Поле выбора типа объекта для спавна
    public GameObject UFO_prefab;                   // Префаб НЛО для спавна
    public GameObject Targets;                      

    // Параметры таймера
    public float time_to_spawn_Asteroids = 2;
    private float _timeLeft = 0f;
    public static bool _timerOn = true;
    public static bool ufoOnScene = false;
    public static bool _generatortimerOn = false;

    Vector2 min;// bottom-left corner
    Vector2 max;// top-right corner
    // Параметры уровня
    private int currentLVL = 0;
    public static int asteroidsOnScene = 0;

    public void Awake()
    {
        Instance = this;
    }
    
    public void onStart()
    {
        min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0)); // bottom-left corner
        max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1)); // top-right corner
        _timeLeft = GetRandomTime(1, 1);

        //Создание двух астероидов вначале игры
        for (int i =0; i < 2; i++)
        {
            Spawn_Asteroid(GetRandomPosition(8000,4500), GetRandomRotation(), 2);
            asteroidsOnScene++;
        }
        
    }

    void Update()
    {
        if (_generatortimerOn)
        {
            if (_timerOn)//Таймер до появления НЛО
            {
                if (_timeLeft > 0)
                {
                    _timeLeft -= Time.deltaTime;
                    UpdateTimeToSpawnUFO();
                }
                else
                {
                    _timeLeft = GetRandomTime(20, 40);
                    _timerOn = false;
                }
            }

            if (asteroidsOnScene < 1) // проверка на кол-во астероидов
            {
                if ((time_to_spawn_Asteroids -= Time.deltaTime) < 0)
                {
                    currentLVL++;
                    for (int i = 0; i < currentLVL + 2; i++)
                    {
                        Spawn_Asteroid(GetRandomPosition(8000, 4500), GetRandomRotation(), 2);
                        asteroidsOnScene++;
                    }
                    time_to_spawn_Asteroids = 2;
                }
            }
        }
    }

    private void UpdateTimeToSpawnUFO()
    {
        if (_timeLeft < 0)
        {
            Spawn_UFO(GetRandomPosition(7200,4000));
            _timeLeft = GetRandomTime(1, 1);
            _timerOn = false;
        }
    }

    private float GetRandomTime(float minTime, float maxTime)
    {
        float _time = Random.Range(minTime, maxTime);
        return _time;
    }

    //Получение случайной точки на поле для спавна
    private Vector3 GetRandomPosition(int x, int y)
    {

        float get_x = Random.Range(min.x, max.x);
        float get_y = Random.Range(min.y, max.y);
        Vector3 position = new Vector3(get_x, get_y,0);
        return position;
    }

    //Получение случайного разворота объекта
    private float GetRandomRotation()
    {
        float rotation = Random.Range(-900, 900);
        return rotation / 10;
    }

    void Spawn_UFO(Vector3 position)
    {
        GameObject UFO = Instantiate(UFO_prefab, position, Quaternion.identity, Targets.transform);
    }
    public void Spawn_Asteroid(Vector3 position,float rotation, int asteroid_size)
    {
        //0 = Small, 1 = Medium, 2 = Big
        if (asteroid_size == 0)
        {
            var Asteroid = PoolSystem.instance.GetObject(type);
            Asteroid.GetComponent<Asteroid_logic>().OnCreate(position, transform.rotation, 0);

            Asteroid.transform.Rotate(0, 0, rotation, Space.Self);
        }
        else if (asteroid_size == 1)
        {
            var Asteroid = PoolSystem.instance.GetObject(type);
            Asteroid.GetComponent<Asteroid_logic>().OnCreate(position, transform.rotation, 1);

            Asteroid.transform.Rotate(0, 0, rotation, Space.Self);
        }
        else
        {
            var Asteroid = PoolSystem.instance.GetObject(type);
            Asteroid.GetComponent<Asteroid_logic>().OnCreate(position, transform.rotation, 2);

            Asteroid.transform.Rotate(0, 0, rotation, Space.Self);
        }
        
    }
}
