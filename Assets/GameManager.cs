using UnityEngine;
using TMPro;

public class gameManager : MonoBehaviour
{
    private int score = 0;
    private float timer = 120f;

    private bool QTEActive = false;
    private float QTETimeLimit = 2f;
    private bool QTEFail = false;
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

    public void MashQTE()
    {
        if (QTEActive)
        {
            //QTEStartTime = Time.time;
            //NumberOfPresses = 0;
            //PressesRequired = 20; // Example threshold
            
            QTEFail = false; // Player succeeded by mashing
        }
    }

    public void MultiQTE()
    {
        if (QTEActive)
        {
            QTEFail = true; // Player failed by pressing the wrong button
        }
    }

    public void StartQTE()
    {
        QTEActive = true;
        QTEFail = false;
        Invoke("EndQTE", QTETimeLimit);
    }

    public void EndQTE()
    {
        QTEActive = false;
        if (QTEFail)
        {
            Debug.Log("QTE Failed!");
            // Handle failure consequences here
        }
        else
        {
            Debug.Log("QTE Succeeded!");
            // Handle success rewards here
        }
    }
}
