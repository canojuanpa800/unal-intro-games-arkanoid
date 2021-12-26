using UnityEngine;
using TMPro;

public class UILevelScore : MonoBehaviour
{
    private const string SCORE_TEXT_TEMPLATE = "{0} pts";
    
    private CanvasGroup _canvasGroup;
    private TextMeshProUGUI _scoreText;
    private TextMeshProUGUI _levelText;
    
    void Start()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _canvasGroup.alpha = 0;
        
        _scoreText = transform.Find("ScoreText").GetComponent<TextMeshProUGUI>();
        _levelText = transform.Find("LevelText").GetComponent<TextMeshProUGUI>();
        
        ArkanoidEvent.OnScoreUpdatedEvent += OnScoreUpdated;
        ArkanoidEvent.OnGameStartEvent += OnGameStart;
        ArkanoidEvent.OnGameOverEvent += OnGameOver;
    }
    
    private void OnDestroy()
    {
        ArkanoidEvent.OnScoreUpdatedEvent -= OnScoreUpdated;
        ArkanoidEvent.OnGameStartEvent -= OnGameStart;
        ArkanoidEvent.OnGameOverEvent -= OnGameOver;
    }
    
    private void OnGameStart()
    {
        _canvasGroup.alpha = 1;
    }
   
    private void OnGameOver()
    {
        _canvasGroup.alpha = 0;
    }
    
    private void OnScoreUpdated(int score, int totalScore)
    {
        _scoreText.text = string.Format(SCORE_TEXT_TEMPLATE, totalScore);
    }
}