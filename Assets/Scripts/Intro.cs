﻿using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class Intro : MonoBehaviour
{
    public UnityStandardAssets.Characters.FirstPerson.FirstPersonController Controller;
    public GameObject SpeechBubble;
    public UnityEngine.UI.Text SpeechText;
    public SpeechSequence[] Sequence1;
    public SpeechSequence[] Sequence2;
    public SpeechSequence[] Sequence3;
    public SpeechSequence[] Sequence4JeffSobbing;
    public SpeechSequence[] Sequence5Potions;
    public SpeechSequence[] Sequence6JeffGoat;
    public SpeechSequence BringMePotion;
    public float DisableControlsForSeconds;
    public UnityEvent EndingEvent;

    public Transform[] Cultists;
    public Transform[] Cultists2;
    public float CultistSpeed = 4;
    public Transform Door;
    public Transform Door2;

    public Transform[] Pillars;

    public Door FirstDoor;

    float defWalkSpeed;
    int pillarsDone = 0;
    int ending = 0;
    int potions = 0;

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

    public void PlayEnding()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("outro");
    }

    IEnumerator IntroSeq()
    {
        foreach (SpeechSequence seq in Sequence1)
        {
            SpeechBubble.SetActive(true);
            SpeechText.text = seq.Text;
            yield return new WaitForSeconds(seq.Time);
        }
        SpeechBubble.SetActive(false);
    }

    public void PlaySequence2()
    {
        StartCoroutine(Seq2());
    }
    public void PlaySequence3()
    {
        StartCoroutine(Seq3());
    }
    public void PlaySequence4JeffSobbing()
    {
        StartCoroutine(_PlaySequence4JeffSobbing());
    }
    public void PlaySequence5Potions()
    {
        StartCoroutine(_PlaySequence5Potions());
    }
    public void PlaySequence6JeffGoat()
    {
        StartCoroutine(_PlaySequence6JeffGoat());
    }

    public void PlaceSkullOnPillar()
    {
        ++pillarsDone;

        StartCoroutine(MovePillars());

        if (pillarsDone == Pillars.Length)
        {
            FirstDoor.Activate();
        }
    }
    public void WantsPotion()
    {
        StartCoroutine(_BringMePotion());
    }

    public void AddEnding()
    {
        ++ending;
        if (ending == 2)
        {
            EndingEvent.Invoke();
        }
    }

    IEnumerator Seq2()
    {
        foreach (SpeechSequence seq in Sequence2)
        {
            SpeechBubble.SetActive(true);
            SpeechText.text = seq.Text;
            yield return new WaitForSeconds(seq.Time);
        }

        SpeechBubble.SetActive(false);

        // Send dudes out the door
        for (;;)
        {
            int reached = 0;
            foreach (Transform tr in Cultists)
            {
                if (Vector3.SqrMagnitude(Door.position - tr.position) > 0.1f)
                {
                    tr.position = tr.position + (Door.position - tr.position).normalized * Time.deltaTime * CultistSpeed;
                }
                else
                {
                    tr.gameObject.SetActive(false);
                    ++reached;
                }
            }

            yield return null;

            if (reached == Cultists.Length)
                break;
        }
    }

    IEnumerator Seq3()
    {
        Controller.m_WalkSpeed = 0;
        Controller.m_RunSpeed = 0;
        
        foreach (SpeechSequence seq in Sequence3)
        {
            SpeechBubble.SetActive(true);
            SpeechText.text = seq.Text;
            yield return new WaitForSeconds(seq.Time);
        }

        SpeechBubble.SetActive(false);

        Controller.m_WalkSpeed = defWalkSpeed;
        Controller.m_RunSpeed = defWalkSpeed;

        // Send dudes out the door
        for (;;)
        {
            int reached = 0;
            foreach (Transform tr in Cultists2)
            {
                if (Vector3.SqrMagnitude(Door2.position - tr.position) > 0.1f)
                {
                    tr.position = tr.position + (Door2.position - tr.position).normalized * Time.deltaTime * CultistSpeed;
                }
                else
                {
                    tr.gameObject.SetActive(false);
                    ++reached;
                }
            }

            yield return null;

            if (reached == Cultists2.Length)
                break;
        }
    }

    IEnumerator _PlaySequence4JeffSobbing()
    {
        foreach (SpeechSequence seq in Sequence4JeffSobbing)
        {
            SpeechBubble.SetActive(true);
            SpeechText.text = seq.Text;
            yield return new WaitForSeconds(seq.Time);
        }

        SpeechBubble.SetActive(false);
    }
    IEnumerator _PlaySequence5Potions()
    {
        SpeechBubble.SetActive(true);
        SpeechText.text = Sequence5Potions[potions].Text;
        yield return new WaitForSeconds(Sequence5Potions[potions].Time);
        SpeechBubble.SetActive(false);

        ++potions;

        if (potions == 3)
        {
            AddEnding();
        }
    }
    IEnumerator _PlaySequence6JeffGoat()
    {
        foreach (SpeechSequence seq in Sequence6JeffGoat)
        {
            SpeechBubble.SetActive(true);
            SpeechText.text = seq.Text;
            yield return new WaitForSeconds(seq.Time);
        }

        SpeechBubble.SetActive(false);

        AddEnding();
    }

    IEnumerator _BringMePotion()
    {
        SpeechBubble.SetActive(true);
        SpeechText.text = BringMePotion.Text;
        yield return new WaitForSeconds(BringMePotion.Time);
        SpeechBubble.SetActive(false);
    }

    IEnumerator MovePillars()
    {
        float duration = 1f;
        float movement = 0.22f;

        float startTime = Time.time;
        while ((Time.time - startTime) < duration)
        {
            foreach (Transform tr in Pillars)
                tr.position += new Vector3(0, -1f, 0) * Time.deltaTime * (movement / duration);

            yield return null;
        }
    }

    void EnableControls()
    {
        Controller.m_WalkSpeed = defWalkSpeed;
        Controller.m_RunSpeed = defWalkSpeed;
    }
}
