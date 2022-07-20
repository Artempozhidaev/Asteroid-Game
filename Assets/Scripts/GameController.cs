using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    public int PlayerHP = 4;

    public TextMeshProUGUI TMP_Score;
    public TextMeshProUGUI TMP_ScoreText;
    public TextMeshProUGUI TMP_HP;
    public TextMeshProUGUI TMP_HPText;
    public TextMeshProUGUI TMP_Lost;
    public TextMeshProUGUI TMP_buttonControlText;

    public GameObject button_NewGame;
    public GameObject button_Continue;
    public GameObject button_KeyControl;

    public GameObject PlayerShip_prefab;
    public GameObject targets;
    public static bool MouseControl { get; private set; } = false;

    private bool isPause = false;

    private void Awake()
    {
        instance = this;
    }
    private void Update()
    {
        try
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (isPause)
                {
                    
                    PauseGame();
                    Time.timeScale = 1.0f;
                    Object_spawn._generatortimerOn = true;
                    isPause = false;
                }
                else
                {
                    
                    PauseGame();
                    Time.timeScale = 0;
                    Object_spawn._generatortimerOn = false;
                    isPause = true;
                }
            }
        }
        catch { }
    }
    public void NewGameClick()
    {
        button_NewGame.SetActive(false);
        button_Continue.SetActive(false);
        button_KeyControl.SetActive(false);
        TMP_Lost.gameObject.SetActive(false);

        TMP_Score.gameObject.SetActive(true);
        TMP_ScoreText.gameObject.SetActive(true);
        TMP_HP.gameObject.SetActive(true);
        TMP_HPText.gameObject.SetActive(true);
        

        Ship_logic.HP = PlayerHP;

        Instantiate(PlayerShip_prefab, transform);

        Score.Instance.ClearScore();
        Score.Instance.UpdateHP();

        Object_spawn.Instance.onStart();
        Object_spawn._generatortimerOn = true;
    }

    private void PauseGame()
    {
        if (!isPause)
        {
            button_NewGame.SetActive(true);
            button_Continue.SetActive(true);
            button_KeyControl.SetActive(true);

        }
        else
        {
            button_NewGame.SetActive(false);
            button_Continue.SetActive(false);
            button_KeyControl.SetActive(false);

        }
    }

    public void —ontinueClick()
    {
        Time.timeScale = 1.0f;
        PauseGame();
        Object_spawn._generatortimerOn = true;
        isPause = false;
    }


    public void KeyboardClick()
    {
        if (MouseControl)
        {
            MouseControl = false;
            var rt = button_KeyControl.GetComponent<RectTransform>();
            rt.sizeDelta = new Vector2(160, 30);
            TMP_buttonControlText.text = " À¿¬»¿“”–¿";
        }
        else
        {
            MouseControl = true;
            var rt = button_KeyControl.GetComponent<RectTransform>();
            rt.sizeDelta = new Vector2(160,50);
            TMP_buttonControlText.text = "Ã€ÿ‹ +  À¿¬»¿“”–¿";
        }
        
    }
    public void MouseAndKeyboardClick()
    {
        
    }

    public void EndGame()
    {
        Object_spawn._generatortimerOn = false;
        if (!Object_spawn._timerOn)
            UFO_logic.Instance.EndGame();

        if (Object_spawn.asteroidsOnScene > 0)
        {
            Transform asteroid = targets.transform.GetChild(2);
            for (int i = 0; i < asteroid.transform.childCount; i++)
                if(asteroid.transform.GetChild(i).gameObject.activeInHierarchy)
                    asteroid.transform.GetChild(i).GetComponent<Asteroid_logic>().EndGame();
        }

        TMP_Lost.gameObject.SetActive(true);
        button_NewGame.SetActive(true);
        button_KeyControl.SetActive(true);


        TMP_HP.gameObject.SetActive(false);
        TMP_HPText.gameObject.SetActive(false);
    }
}
