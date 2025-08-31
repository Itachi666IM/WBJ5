using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject[] upgradeImages;
    [SerializeField] TMP_Text[] upgradeObjects;
    [SerializeField] GameObject wreckButton;
    [SerializeField] TMP_Text targetScoreText;
    [SerializeField] TMP_Text forceText;
    [SerializeField] TMP_Text massText;
    [SerializeField] TMP_Text chainText;

    public List<string> availableUpgrades;
    List<string> copyList;

    [SerializeField] GameObject[] towerPrefabs;
    [SerializeField] Transform spawnPos;

    [SerializeField] float delayTime;

    WBall wreckingBall;
    bool hasUsed = false;

    float forceAmount;
    float massAmount;
    float chainAmount;
    int score;
    private int requiredTarget = 50;

    [SerializeField] GameObject scoreCalculator;
    [SerializeField] Transform scoreCalculatorPos;
    private void Awake()
    {
        targetScoreText.text = "Target - 0/" + requiredTarget;
        copyList = availableUpgrades;
        wreckingBall = FindAnyObjectByType<WBall>();
        Instantiate(scoreCalculator, scoreCalculatorPos);
    }

    void EnableUpgradeImageObjects()
    {
        foreach(var upgradeObject in upgradeObjects)
        {
            int index = Random.Range(0,copyList.Count);
            upgradeObject.text = copyList[index];
            copyList.Remove(upgradeObject.text);
        }
        foreach(var upgradeImage in upgradeImages)
        {
            upgradeImage.SetActive(true);
        }
        copyList = availableUpgrades;
    }

    void DisableUpgradeImageObjects()
    {
        foreach(var upgradeImage in upgradeImages)
        {
            upgradeImage.SetActive(false);
        }
        wreckButton.SetActive(true);
    }

    public void HasUsedUpgrades()
    {
        hasUsed = false;
        DisableUpgradeImageObjects();
        requiredTarget += 10;
        targetScoreText.text = "Target - 0/" + requiredTarget;
        Instantiate(scoreCalculator, scoreCalculatorPos);
        int index = Random.Range(0,towerPrefabs.Length);
        GameObject towerToSetNext = towerPrefabs[index];
        if(towerToSetNext != null)
        {
            Instantiate(towerToSetNext,spawnPos.transform);
        }
    }

    public void HasUsedWreckingBall()
    {
        hasUsed = true;
        wreckButton.SetActive(false);
        if(score<requiredTarget)
        {
            SceneManager.LoadScene("Game Over");
        }
        else
        {
            EnableUpgradeImageObjects();
            FindAndDestroyExistingTower();
        }
    }

    public void DelayUpgradeWindow()
    {
        Invoke(nameof(HasUsedWreckingBall), delayTime);
    }

    void FindAndDestroyExistingTower()
    {
        Tower tower = FindAnyObjectByType<Tower>();
        Destroy(tower.gameObject);
    }

    private void FixedUpdate()
    {
        if(hasUsed)
        { 
            wreckingBall.rb.linearVelocity = new Vector2(0, 0);
            wreckingBall.rb.angularVelocity = 0;
            wreckingBall.rb.position= new Vector2(0,-1.59f);
        }
    }

    public void CalculateScore()
    {
        score = (int)(Mathf.Sqrt(Mathf.Pow(wreckingBall.rb.linearVelocity.magnitude,2) + Mathf.Pow(wreckingBall.rb.angularVelocity,2)));
        targetScoreText.text = "Target - " + score.ToString() + "/" + requiredTarget;
    }
}
