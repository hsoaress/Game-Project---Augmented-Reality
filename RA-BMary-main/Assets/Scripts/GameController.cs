using UnityEngine;
using TMPro;
using UnityEngine.Video;
using UnityEngine.UI;
using System;

public class GameController : MonoBehaviour
{
    public float totalTime = 300f; // tempo para acabar em segundos ( 300s / 60s  = 5m )
    private float timeRemaining;
    private bool timerRunning = true;
    private bool hasAnswered = false;

    public TMP_Text timerText;
    public TMP_InputField answerInput;
    public Button submitButton;

    public AudioSource music;

    public VideoPlayer videoFailure; // video quando erra ou tempo acaba
    public VideoPlayer videoSuccess; // video quando acerta

    public string correctAnswer = "solução"; // resposta

    public GameObject uiGroup; // um empty que agrupe o timer, input e botão

    void Start()
    {
        timeRemaining = totalTime;
        UpdateTimerDisplay();

        // conecta o botão com o teste
        submitButton.onClick.AddListener(CheckAnswer);
    }

    void Update()
    {
        if (timerRunning && timeRemaining > 0 && !hasAnswered)
        {
            timeRemaining -= Time.deltaTime;
            UpdateTimerDisplay();

            if (timeRemaining <= 0)
            {
                timeRemaining = 0;
                timerRunning = false;
                hasAnswered = true;
                HideUI();
                PlayFailureVideo();
            }
        }
    }

    void UpdateTimerDisplay()
    {
        TimeSpan time = TimeSpan.FromSeconds(timeRemaining);
        timerText.text = string.Format("{0:D2}:{1:D2}", time.Minutes, time.Seconds);
    }

    void CheckAnswer()
    {
        if (hasAnswered) return;

        hasAnswered = true;
        timerRunning = false;
        HideUI();

        string userAnswer = answerInput.text.Trim().ToLower();
        if (userAnswer == correctAnswer.ToLower())
        {
            Destroy(music);
            PlaySuccessVideo();
            
        }
        else
        {
            Destroy(music);
            PlayFailureVideo();
            
        }
    }

    void PlayFailureVideo()
    {
        videoFailure.gameObject.SetActive(true);
        videoFailure.Play();
        
    }

    void PlaySuccessVideo()
    {
        videoSuccess.gameObject.SetActive(true);
        videoSuccess.Play();
        
    }
    
    void HideUI()
    {
        if (uiGroup != null)
        {
            uiGroup.SetActive(false);
        }
    }

}
