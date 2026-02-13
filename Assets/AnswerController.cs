using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerController : MonoBehaviour
{
    public List<answerCollect> answerCollection;
    public int randomNumber;

    // Unity way (0 to 9)

    // Start is called before the first frame update
    void Start()
    {
        SelectRandomAnswer();
    }

    public void SelectRandomAnswer()
    {
        randomNumber = UnityEngine.Random.Range(0, answerCollection.Count);

        answerCollection[randomNumber].GetComponent<answerCollect>().correctAnswer = true;

    }

    public void RemoveAnswer()
    {
        answerCollection.RemoveAt(randomNumber);
        randomNumber = UnityEngine.Random.Range(0, answerCollection.Count);
    }

    public void playSound()
    {
        if (!answerCollection[randomNumber].GetComponent<AudioSource>().isPlaying)
        {
            answerCollection[randomNumber].GetComponent<AudioSource>().Play();
        }
    }

    // Update is called once per frame
    void Update() { }
}
