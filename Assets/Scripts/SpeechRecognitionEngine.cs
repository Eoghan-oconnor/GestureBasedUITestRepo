using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows.Speech;

public class SpeechRecognitionEngine : MonoBehaviour
{
    public string[] keywords = new string[] { "First", "Second", "Third", "Fourth" };
    public ConfidenceLevel confidence = ConfidenceLevel.Low;
    public float speed = 1;

    public Text results;
    public Image target;

    protected PhraseRecognizer recognizer;
    protected string word = "Fourth";

    private void Start()
    {
        if (keywords != null)
        {
            recognizer = new KeywordRecognizer(keywords, confidence);
            recognizer.OnPhraseRecognized += Recognizer_OnPhraseRecognized;
            recognizer.Start();
        }
    }

    private void Recognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        word = args.text;
        results.text = "You said: <b>" + word + "</b>";
        //Debug.Log(word);
    }

    private void Update()
    {
        var x = target.transform.position.x;
        var y = target.transform.position.y;

        switch (word)
        {
            case "First":
                y += speed;
                break;
            case "Second":
                y -= speed;
                break;
            case "Third":
                x -= speed;
                break;
            case "Fourth":
                x += speed;
                break;
        }

        target.transform.position = new Vector3(x, y, 0);
    }

    private void OnApplicationQuit()
    {
        if (recognizer != null && recognizer.IsRunning)
        {
            recognizer.OnPhraseRecognized -= Recognizer_OnPhraseRecognized;
            recognizer.Stop();
        }
    }
}
