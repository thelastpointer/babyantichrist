using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Multiline]
    public string[] PrologueTexts;
    public Text PrologueLabel;

    public void StartGame()
    {
        StartCoroutine(Prologue());
    }
    public void ExitGame()
    {
        Application.Quit();
    }

    IEnumerator Prologue()
    {
        foreach (string str in PrologueTexts)
        {
            PrologueLabel.text = str;
            yield return new WaitForSeconds(7f);
        }

        SceneManager.LoadScene(1);
    }
}
