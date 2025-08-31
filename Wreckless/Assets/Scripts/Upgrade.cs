using TMPro;
using UnityEngine;

public class Upgrade : MonoBehaviour
{
    [SerializeField] TMP_Text myText;
    GameManager gameManager;

    private void Awake()
    {
        gameManager = FindAnyObjectByType<GameManager>();
    }

    public void ViewMyText()
    {
        string upgradeText = myText.text;
        if (upgradeText != null)
        {
            if (upgradeText == "Force +100")
            {
                gameManager.ForceAdd();
            }
            else if (upgradeText == "Force -100")
            {
                gameManager.ForceRemove();
            }
            else if (upgradeText == "Mass +5")
            {
                gameManager.MassAdd();
            }
            else if (upgradeText == "Mass -5")
            {
                gameManager.MassRemove();
            }
        }
    }
}
