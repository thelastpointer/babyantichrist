using UnityEngine;
using System.Collections;

public class Outro : MonoBehaviour {

    public SpeechSequence[] Seq;
    public UnityEngine.UI.Text SpeechText;

    bool isDone = false;

    [System.Serializable]
    public class SpeechSequence
    {
        [Multiline]
        public string Text;
        public float Time;
    }
    
    void Start()
    {
        StartCoroutine(outr());
	}

    void Update()
    {
        if (isDone && Input.anyKeyDown)
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
	
    IEnumerator outr()
    {
        foreach (SpeechSequence seq in Seq)
        {
            SpeechText.text = seq.Text;
            yield return new WaitForSeconds(seq.Time);
        }

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        isDone = true;
    }
}
