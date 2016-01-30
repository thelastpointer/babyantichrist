using UnityEngine;
using System.Collections;

public class Intro : MonoBehaviour
{
    public UnityStandardAssets.Characters.FirstPerson.FirstPersonController Controller;
    public GameObject SpeechBubble;
    public UnityEngine.UI.Text SpeechText;
    public SpeechSequence[] Sequence1;
    public SpeechSequence[] Sequence2;
    public float DisableControlsForSeconds;

    float defWalkSpeed;

    [System.Serializable]
    public class SpeechSequence
    {
        public string Text;
        public float Time;
    }

	void Start()
    {
        defWalkSpeed = Controller.m_WalkSpeed;
        Controller.m_WalkSpeed = 0;
        Controller.m_RunSpeed = 0;

        StartCoroutine(IntroSeq());
        Invoke("EnableControls", DisableControlsForSeconds);
    }

    IEnumerator IntroSeq()
    {
        foreach (SpeechSequence seq in Sequence1)
        {
            SpeechBubble.SetActive(true);
            SpeechText.text = seq.Text;
            yield return new WaitForSeconds(seq.Time);
        }
    }

    public void PlaySequence2()
    {
        StartCoroutine(Seq2());
    }

    IEnumerator Seq2()
    {
        foreach (SpeechSequence seq in Sequence2)
        {
            SpeechBubble.SetActive(true);
            SpeechText.text = seq.Text;
            yield return new WaitForSeconds(seq.Time);
        }
    }

    void EnableControls()
    {
        Controller.m_WalkSpeed = defWalkSpeed;
        Controller.m_RunSpeed = defWalkSpeed;
    }
}
