using UnityEngine;
using TMPro;

public class gameManager : MonoBehaviour
{
    private int score = 0;
    private float timer = 120f;

    public TMP_Text scoreLabel;
    public TMP_Text timerLabel;

    public static gameManager instance;
 

    void Awake()
    {
        instance = this;
        scoreLabel = GameObject.Find("Score").GetComponent<TMP_Text>();
        scoreLabel.text = "Score: 0";
        timerLabel = GameObject.Find("Timer").GetComponent<TMP_Text>();
        timerLabel.text = "Time: 120";

    }

    void Start()
    {
          
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            timer = 0;
            // Handle end of game here
        }
        UpdateTimer(timer);
    }

    public void AddScore(int points)
    {
        score += points;
        scoreLabel.text = "Score: " + score;
        Debug.Log("Score: " + score);
    }

    public void UpdateTimer(float time)
    {
        timer = time;
        timerLabel.text = "Time: " + Mathf.CeilToInt(timer);
    }
}
