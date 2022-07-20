using UnityEngine;

public class UFO_logic : MonoBehaviour
{
    public static UFO_logic Instance;

    public AudioSource ShotSound;

    [SerializeField]
    private PoolSystem.ObjectInfo.ObjectType bulletType;

    float time_to_shoot = 3f;

    private void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        time_to_shoot -= Time.deltaTime;
        if (time_to_shoot < 0)
        {
            Shot();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        try
        {
            if ((collision.gameObject.tag != null) && (collision.gameObject.CompareTag("Asteroid(Big)") || collision.gameObject.CompareTag("Asteroid(Medium)") || collision.gameObject.CompareTag("Asteroid(Small)")))
            {
                PoolSystem.instance.DestroyObject(collision.gameObject);
                Object_spawn.asteroidsOnScene--;
                Object_spawn._timerOn = true;
                Destroy(gameObject);
            }
        }
        catch { }
    }
    void Shot()
    {
        var bullet = PoolSystem.instance.GetObject(bulletType);
        bullet.GetComponent<Bullet_logic>().onCreate(gameObject.transform.position - transform.up * 6f, transform.rotation);

        var ownLocalAngles = transform.localEulerAngles;
        ownLocalAngles.z = 120f;
        ownLocalAngles.z += GetRandomRotation();
        bullet.transform.localEulerAngles = ownLocalAngles;
        ShotSound.Play();
        time_to_shoot = Random.Range(2f, 5f);
    }
    private float GetRandomRotation()
    {
        float rotation = Random.Range(1f, 140f);
        return rotation;
    }

    public void EndGame()
    {
        Destroy(gameObject);
        Object_spawn._timerOn = true;
    }
}
