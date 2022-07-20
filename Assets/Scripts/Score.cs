using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    public static Score Instance;

    public TextMeshProUGUI score;
    public TextMeshProUGUI hp;
    void Start()
    {
        ClearScore();
        UpdateHP();
    }
    public void Awake()
    {
        Instance = this;
    }
    public void UpdateScore(int points)
    {
        score.text = (int.Parse( score.text) + points).ToString();
    }
    public void ClearScore()
    {
        score.text = 0.ToString();
    }

    public void UpdateHP()
    {
        hp.text = Ship_logic.HP.ToString();
    }
}
