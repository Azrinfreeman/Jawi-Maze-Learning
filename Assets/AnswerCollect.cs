using System.Collections;
using System.Collections.Generic;
using AudioHandler;
using GameHandler;
using UnityEngine;

public class answerCollect : MonoBehaviour
{
    public int points = 10;
    public bool correctAnswer;
    private AudioSource _audioSource;
    private SpriteRenderer _spriteRenderer;
    private Collider2D _collider2D;

    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _collider2D = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update() { }

    IEnumerator onTriggerTouchAnswer(Collider2D collision)
    {
        if (correctAnswer)
        {
            if (!collision.gameObject.CompareTag(TagsConstants.PlayerTag))
                yield break;

            //add score
            ScoreHandler.ScoreHandler.Instance.AddScore(points);
            _collider2D.enabled = false;
            _spriteRenderer.enabled = false;
            //reset position
            GameHandler.GameHandler.Instance.DecrementPacGumNumber();
            GameHandler.GameHandler.Instance.ResetGhostsAndPlayer();
            //stop time
            Time.timeScale = 0;
            Time.fixedDeltaTime = 0;
            MusicHandler.MusicHandler.Instance.StopMusic();
            MusicHandler.MusicHandler.Instance.PlayIntermission();
            //wait 5 seconds
            yield return new WaitForSecondsRealtime(5.5f);
            //start time
            Time.timeScale = Time.timeScale = TimeConstants.TimeScaleNormal;
            Time.fixedDeltaTime = TimeConstants.FixedDeltaTime;

            // Reset the ghost mode.
            GameHandler.GameHandler.Instance._allTimersPaused = true;
        }
        else
        {
            if (!collision.gameObject.CompareTag(TagsConstants.PlayerTag))
                yield break;

            _collider2D.enabled = false;
            _spriteRenderer.enabled = false;

            GameHandler.GameHandler.Instance.KillPlayer();
        }
        GameStartHandler.Instance.answer[0].GetComponent<AnswerController>().SelectRandomAnswer();
        GetComponent<Transform>().gameObject.SetActive(false);
        gameObject
            .transform.parent.GetComponent<AnswerController>()
            .answerCollection.RemoveAt(
                gameObject.transform.parent.GetComponent<AnswerController>().randomNumber
            );
    }

    IEnumerator resetAnswer()
    {
        yield return new WaitForSecondsRealtime(3f);
        GameObject.Find("EventSystem").GetComponent<AudioSource>().Play();
        GameStartHandler.Instance.answer[0].GetComponent<AnswerController>().SelectRandomAnswer();
        GameStartHandler.Instance.answer[0].GetComponent<AnswerController>().playSound();
    }

    public void ResetAnswer()
    {
        StartCoroutine(resetAnswer());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(onTriggerTouchAnswer(collision));
    }
}
