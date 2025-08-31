using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class StoryManager : MonoBehaviour
{
    [SerializeField] TMP_Text storyText;
    public string[] dialogs;
    string currentDialogue;
    int index = 0;
    [SerializeField] float typingSpeed;
    [SerializeField] GameObject continueButton;
    [SerializeField] GameObject startButton;
    [SerializeField] GameObject skipButton;

    private void Awake()
    {
        currentDialogue = dialogs[index];
        storyText.text = "";
        StartCoroutine(WriteSentence());
    }

    public void NextSentence()
    {
        if(index < dialogs.Length-1)
        {
            index++;
            currentDialogue = dialogs[index];
            storyText.text = "";
            StartCoroutine(WriteSentence());
        }
    }

    IEnumerator WriteSentence()
    {
        storyText.text = "";
        foreach(char c in currentDialogue.ToCharArray())
        {
            storyText.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }
        yield return new WaitForSeconds(typingSpeed);
        if(index == dialogs.Length-1)
        {
            startButton.SetActive(true);
            skipButton.SetActive(false);
        }
        else
        {
            continueButton.SetActive(true);
        }
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
