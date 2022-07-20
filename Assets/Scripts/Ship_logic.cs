using UnityEngine;

public class Ship_logic : MonoBehaviour
{
    [SerializeField]
    private PoolSystem.ObjectInfo.ObjectType bulletType;
    [SerializeField]
    private Vector2 spawnPosition;
    
    public GameObject bullets;

    public float time_to_crash = 3f;    // Время неуязвимости
    public bool _timerOn = true;        // Активатор таймера

    private float _timeLeft = 3f;       // Для отсчета времени неуязвимости и мерцания
    private float time_to_blick = 0.5f; // Задержка мерцания
    private float time_blick = 0.25f;   // Время мерцания

    public static int HP = 4;

    public AudioSource CrashSound;

    void Start()
    {
        _timeLeft = time_to_crash;
    }

    // Update is called once per frame
    void Update()
    {
        if (HP < 1)
        {
            GameController.instance.EndGame();
            Destroy(gameObject);
        }
            

        if (_timerOn)
        {

            //таймер до прекращения эффекта неуязвимости
            if (_timeLeft > 0)
            {
                _timeLeft -= Time.deltaTime;
                UpdateTime();
            }
            else
            {
                _timeLeft = time_to_crash;
                _timerOn = false;
            }
            time_to_blick -= Time.deltaTime;
            //таймер мерцания
            if (time_to_blick < 0)
            {
                Blick();
            }
        }
        else
        {
            GetComponent<SpriteRenderer>().enabled = true;
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        try
        {
            
            if (collision.gameObject.tag != null)
            {
                if (collision.gameObject.CompareTag("Asteroid(Big)") || collision.gameObject.CompareTag("Asteroid(Medium)") || collision.gameObject.CompareTag("Asteroid(Small)"))
                {
                    PoolSystem.instance.DestroyObject(collision.gameObject);

                    Object_spawn.asteroidsOnScene--;

                    HP--;

                    CrashSound.Play();
                    Score.Instance.UpdateHP();

                    _timeLeft = time_to_crash;
                    time_to_blick = 0;
                    _timerOn = true;
                }
                else if (collision.gameObject.CompareTag("UFO"))
                {
                    Destroy(collision.gameObject);

                    Object_spawn._timerOn = true;

                    HP--;

                    Score.Instance.UpdateHP();

                    CrashSound.Play();
                    _timeLeft = time_to_crash;
                    time_to_blick = 0;
                    _timerOn = true;
                }
                else if (collision.gameObject.CompareTag("Bullet(UFO)"))
                {
                    HP--;

                    Score.Instance.UpdateHP();

                    _timeLeft = time_to_crash;
                    time_to_blick = 0;
                    _timerOn = true;

                    CrashSound.Play();
                    PoolSystem.instance.DestroyObject(collision.gameObject);
                }
            }
        }
        catch
        {

        }
    }


    private void UpdateTime()
    {
        if (_timeLeft < 0)
        {
            GetComponent<BoxCollider2D>().enabled = true;
            _timerOn = false;
        }
        else
        {
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }
    void Blick()
    {
        time_blick -= Time.deltaTime;
        if (time_blick > 0)
        {
            GetComponent<SpriteRenderer>().enabled = false;
        }
        else
        {
            GetComponent<SpriteRenderer>().enabled = true;
            time_blick = 0.25f;
            time_to_blick = 0.5f;
        }
    }
}
