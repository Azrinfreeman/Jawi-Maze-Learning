using System.Collections;
using System.Collections.Generic;
using AudioHandler;
using GameHandler;
using Ghosts;
using UnityEngine;
using UnityEngine.Tilemaps;

public class answerCollect : MonoBehaviour
{
    public int points = 100;
    public bool correctAnswer;
    private AudioSource _audioSource;
    private SpriteRenderer _spriteRenderer;
    private Collider2D _collider2D;

    // Start is called before the first frame update
    void Start()
    {
        points = 100;
        _audioSource = GetComponent<AudioSource>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _collider2D = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update() { }

    IEnumerator onTriggerTouchAnswer(Collider2D collision)
    {
        Debug.Log(collision.gameObject.name);
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
            GameHandler.GameHandler.Instance.answer[0].GetComponent<AnswerController>().stopSound();
            //wait 5 seconds
            yield return new WaitForSecondsRealtime(5.5f);
            //start time
            Time.timeScale = Time.timeScale = TimeConstants.TimeScaleNormal;
            Time.fixedDeltaTime = TimeConstants.FixedDeltaTime;

            // Reset the ghost mode.
            GameHandler.GameHandler.Instance._allTimersPaused = true;
            gameObject.transform.parent.GetComponent<AnswerController>().RemoveAnswer();
            gameObject.transform.SetAsLastSibling();
        }
        else
        {
            Debug.Log("salah1");
            if (!collision.gameObject.CompareTag(TagsConstants.PlayerTag))
                yield break;
            GameHandler.GameHandler.Instance.answer[0].GetComponent<AnswerController>().stopSound();
            gameObject
                .transform.parent.GetComponent<AnswerController>()
                .answerCollection.RemoveAt(transform.GetSiblingIndex());

            _collider2D.enabled = false;
            _spriteRenderer.enabled = false;

            GameHandler.GameHandler.Instance.KillPlayer();
            gameObject.transform.SetAsLastSibling();
        }

        //if game is ended
        if (
            GameHandler
                .GameHandler.Instance.answer[0]
                .GetComponent<AnswerController>()
                .answerCollection.Count == 0
        )
        {
            //remove at the top, make the gameobject inactive and next child active
            GameObject
                .Find("EventSystem")
                .GetComponent<GameHandler.GameHandler>()
                .answer[0]
                .gameObject.SetActive(false);

            GameObject
                .Find("EventSystem")
                .GetComponent<GameHandler.GameHandler>()
                .answer.RemoveAt(0);

            if (GameHandler.GameHandler.Instance.answer.Count == 0)
            {
                //show exit button
                GameHandler.GameHandler.Instance.exitButton.gameObject.SetActive(true);
                GameHandler.GameHandler.Instance._allTimersPaused = true;
                yield break;
            }
            //
            GameObject
                .Find("EventSystem")
                .GetComponent<GameHandler.GameHandler>()
                .answer[0]
                .gameObject.SetActive(true);

            //change player tilemap to the next one
            GameObject.Find("Player").GetComponent<Player.PlayerController>().tilemap = GameObject
                .Find("EventSystem")
                .GetComponent<GameHandler.GameHandler>()
                .answer[0]
                .GetComponent<Tilemap>();

            //change RedGhostAi tilemap to the next one
            GameObject.Find("RedGhost").GetComponent<RedGhostAiMovement>().tilemap = GameObject
                .Find("EventSystem")
                .GetComponent<GameHandler.GameHandler>()
                .answer[0]
                .GetComponent<Tilemap>();

            //change BlueGhostAi tilemap to the next one
            GameObject.Find("BlueGhost").GetComponent<BlueGhostAiMovement>().tilemap = GameObject
                .Find("EventSystem")
                .GetComponent<GameHandler.GameHandler>()
                .answer[0]
                .GetComponent<Tilemap>();

            //change OrangeGhostAi tilemap to the next one
            GameObject.Find("OrangeGhost").GetComponent<OrangeGhostAiMovement>().tilemap =
                GameObject
                    .Find("EventSystem")
                    .GetComponent<GameHandler.GameHandler>()
                    .answer[0]
                    .GetComponent<Tilemap>();

            //change PinkGhostAi tilemap to the next one
            GameObject.Find("PinkGhost").GetComponent<PinkGhostAiMovement>().tilemap = GameObject
                .Find("EventSystem")
                .GetComponent<GameHandler.GameHandler>()
                .answer[0]
                .GetComponent<Tilemap>();

            GameHandler.GameHandler.Instance.level++;
            GameObject.Find("TextLevel").GetComponent<TMPro.TextMeshProUGUI>().text =
                "Level: " + GameHandler.GameHandler.Instance.level;

            GameHandler.GameHandler.Instance.ChangeGhostSpeed();
        }
        else if (
            GameHandler
                .GameHandler.Instance.answer[0]
                .GetComponent<AnswerController>()
                .answerCollection.Count > 0
        )
        {
            Debug.Log("salah2");
            GameHandler.GameHandler.Instance._allTimersPaused = false;
            //remove answer tick
            //if game is not ended
            GameHandler
                .GameHandler.Instance.answer[0]
                .GetComponent<AnswerController>()
                .RemoveTheAnswerTick();
            yield return new WaitForSecondsRealtime(2f);
            GetComponent<Transform>().gameObject.SetActive(false);
            //if game is not ended
            GameHandler
                .GameHandler.Instance.answer[0]
                .GetComponent<AnswerController>()
                .SelectRandomAnswer();
        }
    }

    IEnumerator resetAnswer()
    {
        yield return new WaitForSecondsRealtime(4f);
        GameObject.Find("EventSystem").GetComponent<AudioSource>().Play();
        GameHandler
            .GameHandler.Instance.answer[0]
            .GetComponent<AnswerController>()
            .SelectRandomAnswer();
        GameHandler.GameHandler.Instance.answer[0].GetComponent<AnswerController>().playSound();
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
