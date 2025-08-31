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

    public List<string> availableUpgrades;

    [SerializeField] GameObject towerPrefab;
    [SerializeField] Transform spawnPos;

    [SerializeField] float delayTime;

    WBall wreckingBall;
    bool hasUsed = false;

    int forceAmount = 1500;
    int massAmount = 5;
    int score;
    private int requiredTarget = 50;

    [SerializeField] GameObject scoreCalculator;
    [SerializeField] Transform scoreCalculatorPos;

    bool hasPressed;
    float timeToReachTarget;
    private void Awake()
    {
        targetScoreText.text = "Target - 0/" + requiredTarget;

        wreckingBall = FindAnyObjectByType<WBall>();
        Instantiate(scoreCalculator, scoreCalculatorPos.transform);
        Instantiate(towerPrefab, spawnPos.transform);
    }

   

    void EnableUpgradeImageObjects()
    {
        foreach(var upgradeObject in upgradeObjects)
        {
            int index = Random.Range(0,availableUpgrades.Count);
            upgradeObject.text = availableUpgrades[index];
        }
        foreach(var upgradeImage in upgradeImages)
        {
            upgradeImage.SetActive(true);
        }
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
        Instantiate(scoreCalculator, scoreCalculatorPos.transform);
        Instantiate(towerPrefab,spawnPos.transform);
    }

    public void HasUsedWreckingBall()
    {
        hasUsed = true;
        hasPressed = false;
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

    private void Update()
    {
        forceText.text = "Force - " + forceAmount;
        massText.text = "Mass - " + massAmount;
        if(hasPressed)
        {
            timeToReachTarget += Time.time;
        }
        else
        {
            timeToReachTarget = 0;
        }

        if(requiredTarget == 350)
        {
            SceneManager.LoadScene("Win");
        }
    }
    public void HasPressedWreckButton()
    {
        hasPressed = true;
    }

    public void CalculateScore()
    {
        int velocityAtImpact = (int)Mathf.Sqrt(Mathf.Pow(wreckingBall.rb.linearVelocity.magnitude, 2) + Mathf.Pow(wreckingBall.rb.angularVelocity, 2));
        int momentumAtImpact = massAmount * velocityAtImpact;
        float averageForce = 2*momentumAtImpact/timeToReachTarget;
        Debug.Log(averageForce);
        score =(int)(averageForce * 1000) + forceAmount/massAmount;
        targetScoreText.text = "Target - " + score.ToString() + "/" + requiredTarget;
    }

    public void ForceAdd()
    {
        forceAmount += 100;
        wreckingBall.forceAmount = forceAmount;
    }

    public void ForceRemove()
    {
        if(forceAmount>100)
        {
            forceAmount -= 100;
        }
        else
        {
            forceAmount = 0;
        }
        wreckingBall.forceAmount = forceAmount;
    }

    public void MassAdd()
    {
        massAmount += 5;
        wreckingBall.rb.mass = massAmount;
    }

    public void MassRemove()
    {
        if(massAmount>5)
        {
            massAmount -= 5;
        }
    }
}
