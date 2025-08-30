using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] TMP_Text instructionText;
    [SerializeField] GameObject nextButton;
    [SerializeField] GameObject wreckButton;
    public string[] instructions;
    string currentInstruction;
    int index = 0;
    [SerializeField] float typingSpeed;

    private void Awake()
    {
        instructionText.text = "";
        currentInstruction = instructions[index];
        StartCoroutine(WriteSentence());
    }

    IEnumerator WriteSentence()
    {
        foreach(char c in currentInstruction.ToCharArray())
        {
            instructionText.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }
        yield return new WaitForSeconds(typingSpeed);
        nextButton.SetActive(true);
    }

    public void NextSentence()
    {
        if(index < instructions.Length-1)
        {
            index++;
            currentInstruction = instructions[index];
            instructionText.text = "";
            StartCoroutine(WriteSentence());
        }
        else
        {
            nextButton.SetActive(false);
            wreckButton.SetActive(true);
            instructionText.text = "";
        }
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
