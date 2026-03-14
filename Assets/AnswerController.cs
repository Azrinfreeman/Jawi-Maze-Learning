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
        AnswerAssign();
        SelectRandomAnswer();
    }

    public void AnswerAssign()
    {
        answerCollection.Clear();
        for (int i = 0; i < transform.childCount; i++)
        {
            answerCollection.Add(transform.GetChild(i).GetComponent<answerCollect>());
        }
    }

    public void SelectRandomAnswer()
    {
        randomNumber = UnityEngine.Random.Range(0, answerCollection.Count);

        answerCollection[randomNumber].GetComponent<answerCollect>().correctAnswer = true;
    }

    public void RemoveTheAnswerTick()
    {
        for (int i = 0; i < answerCollection.Count; i++)
        {
            answerCollection[i].GetComponent<answerCollect>().correctAnswer = false;
        }
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

    public void stopSound()
    {
        for (int i = 0; i < answerCollection.Count; i++)
        {
            answerCollection[i].GetComponent<AudioSource>().Stop();
        }
    }

    // Update is called once per frame
    void Update() { }
}
