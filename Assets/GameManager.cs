using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class gameManager : MonoBehaviour
{
    private int score = 0;
    private float timer = 120f;

    // --- QTE State ---
    public enum QTEType { None, Mash, Multi }
    private QTEType activeQTE = QTEType.None;
    
    private float QTETimer = 0f;

    // Mash QTE
    private int mashTarget = 10;       // presses needed to succeed
    private int mashCount = 0;

    // Multi QTE
    private Key[] keySequence;         // the sequence the player must press
    private int sequenceIndex = 0;     // which step they're on
    private Key[] possibleKeys = { Key.Q, Key.E, Key.R, Key.F, Key.G, Key.T, Key.Y };

    public TMP_Text scoreLabel;
    public TMP_Text timerLabel;
    public TMP_Text controlsLabel;
    public TMP_Text QTELabel_Mash;
    public TMP_Text QTELabel_Multi;

    public static gameManager instance;

    void Awake()
    {
        instance = this;
        scoreLabel = GameObject.Find("Score").GetComponent<TMP_Text>();
        scoreLabel.text = "Score: 0";
        timerLabel = GameObject.Find("Timer").GetComponent<TMP_Text>();
        timerLabel.text = "Time: 120";
        controlsLabel = GameObject.Find("Controls").GetComponent<TMP_Text>();
        controlsLabel.text = "Controls:\nMove - WASD\nFish - left click";

        QTELabel_Mash = GameObject.Find("QTE_Mash").GetComponent<TMP_Text>();
        QTELabel_Multi = GameObject.Find("QTE_Multi").GetComponent<TMP_Text>();

        HideQTELabels();
        StartMashQTE(); // Start with a test QTE for demonstration
    }

    void Update()
    {
        // Main game timer
        if (activeQTE == QTEType.None)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                timer = 0;
                // TODO: Handle end of game
            }
            UpdateTimer(timer);
        }

        // QTE countdown + input
        if (activeQTE == QTEType.Mash)
        {
            QTETimer -= Time.deltaTime;
            QTELabel_Mash.text = $"MASH [Space]! {mashCount}/{mashTarget}\n{QTETimer:F1}s";

            if (Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                mashCount++;
                if (mashCount >= mashTarget)
                    EndQTE(true);
            }

            if (QTETimer <= 0)
                EndQTE(mashCount >= mashTarget);
        }
        else if (activeQTE == QTEType.Multi)
        {
            QTETimer -= Time.deltaTime;
            UpdateMultiLabel();

            foreach (Key key in possibleKeys)
            {
                if (Keyboard.current[key].wasPressedThisFrame)
                {
                    if (key == keySequence[sequenceIndex])
                    {
                        sequenceIndex++;
                        if (sequenceIndex >= keySequence.Length)
                            EndQTE(true); // All keys pressed correctly
                    }
                    else
                    {
                        EndQTE(false); // Wrong key pressed
                    }
                    return;
                }
            }

            if (QTETimer <= 0)
                EndQTE(false);
        }
    }

    // --- QTE Control ---

    public void StartMashQTE()
    {
        activeQTE = QTEType.Mash;
        QTETimer = 3f;
        mashCount = 0;
        QTELabel_Mash.gameObject.SetActive(true);
        QTELabel_Mash.text = $"MASH [Space]! 0/{mashTarget}";
    }

    public void StartMultiQTE()
    {
        activeQTE = QTEType.Multi;
        QTETimer = 4f; // slightly longer for multi-step
        sequenceIndex = 0;

        // Build a sequence of 4 random keys (no repeats back-to-back)
        keySequence = new Key[4];
        for (int i = 0; i < keySequence.Length; i++)
        {
            Key pick;
            do { pick = possibleKeys[Random.Range(0, possibleKeys.Length)]; }
            while (i > 0 && pick == keySequence[i - 1]); // avoid immediate repeats
            keySequence[i] = pick;
        }

        QTELabel_Multi.gameObject.SetActive(true);
        UpdateMultiLabel();
    }

    private void UpdateMultiLabel()
    {
        string display = "";
        for (int i = 0; i < keySequence.Length; i++)
        {
            if (i < sequenceIndex)
                display += $"[<=c>] ";           // already pressed
            else if (i == sequenceIndex)
                display += $"[{keySequence[i]}] <-"; // current prompt
            else
                display += $"[{keySequence[i]}] ";  // upcoming
        }
        QTELabel_Multi.text = $"{display}\n{QTETimer:F1}s";
    }

    private void EndQTE(bool success)
    {
        activeQTE = QTEType.None;
        HideQTELabels();

        if (success)
        {
            Debug.Log("QTE Succeeded!");
            AddScore(50);
            // TODO: e.g. reel in the fish faster
        }
        else
        {
            Debug.Log("QTE Failed!");
            // TODO: e.g. fish escapes, lose the catch
        }
    }

    private void HideQTELabels()
    {
        QTELabel_Mash.gameObject.SetActive(false);
        QTELabel_Multi.gameObject.SetActive(false);
    }

    // --- Score & Timer ---

    public void AddScore(int points)
    {
        score += points;
        scoreLabel.text = "Score: " + score;
    }

    public void UpdateTimer(float time)
    {
        timerLabel.text = "Time: " + Mathf.CeilToInt(time);
    }
}