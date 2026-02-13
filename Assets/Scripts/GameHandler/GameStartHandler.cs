using System.Collections;
using UnityEngine;

namespace GameHandler
{
    public class GameStartHandler : MonoBehaviour
    {
        public static GameStartHandler Instance { get; private set; }
        public AudioSource StartAudioSource { get; private set; }
        public bool IsStarterPaused { get; private set; }
public AnswerController[] answer;
        private void Awake()
        {
            if (Instance != null && Instance != this)
                Destroy(this);
            else
                Instance = this;
        }
private IEnumerator PlaySoundWithDelay()
{
    for (int i = 0; i < 3; i++)
    {
        answer[0]
            .GetComponent<AnswerController>()
            .playSound();

        yield return new WaitForSeconds(2f);
    }
}
        private void Start()
        {
            StartAudioSource = GetComponent<AudioSource>();
            StartAudioSource.Play();
        }

        private void Update()
        {
            if (StartAudioSource.isPlaying || IsStarterPaused)
                return;


        StartCoroutine(PlaySoundWithDelay());

        GameHandler.Instance.ActivateGhostsAndPlayer();

            enabled = false;
        }


        public void TogglePause()
        {
            if (StartAudioSource.isPlaying)
                StartAudioSource.Pause();
            else
                StartAudioSource.Play();

            IsStarterPaused = !IsStarterPaused;
        }
    }
}
