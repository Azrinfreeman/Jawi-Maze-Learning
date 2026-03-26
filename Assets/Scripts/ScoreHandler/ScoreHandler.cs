using TMPro;
using UnityEngine;

namespace ScoreHandler
{
    public class ScoreHandler : MonoBehaviour
    {
        public string highScoreKey = "HighScore";
        public TextMeshProUGUI scoreText;
        public TextMeshProUGUI highScoreText;
        private bool _reachHighScore;
        public int _score;
        public static ScoreHandler Instance { get; private set; }

        private AudioSource _audioSource;

        private void Awake()
        {
            if (Instance != null && Instance != this)
                Destroy(this);
            else
                Instance = this;
        }

        public void UpdateScores()
        {
            PlayerPrefs.SetInt("StarsCollected_" + PlayerPrefs.GetInt("CurrentPlayerNo_"), +_score);
            //PlayerPrefs.GetInt("StarsCollected_" + PlayerPrefs.GetInt("CurrentPlayerNo_"))
        }

        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
            scoreText.SetText("Point: " + _score);
            highScoreText.SetText("High Point: " + PlayerPrefs.GetInt(highScoreKey));
        }

        public void ResetScore()
        {
            _score = 0;
            scoreText.SetText("Point: " + _score);
        }

        public void AddScore(int score)
        {
            _score += score;
            scoreText.SetText("Point: " + _score);
            if (_score > PlayerPrefs.GetInt(highScoreKey))
            {
                PlayerPrefs.SetInt(highScoreKey, _score);
                highScoreText.SetText("High Point: " + _score);

                if (!_reachHighScore)
                    _audioSource.Play();
                _reachHighScore = true;
            }
        }

        public void UpdateHighScore()
        {
            if (_score <= PlayerPrefs.GetInt(highScoreKey))
                return;
            PlayerPrefs.SetInt(highScoreKey, _score);
        }
    }
}
