using UnityEngine;
using TMPro;
using System.Collections.Generic;

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
    private void Awake()
    {
        copyList = availableUpgrades;
        wreckingBall = FindAnyObjectByType<WBall>();
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
        EnableUpgradeImageObjects();
        FindAndDestroyExistingTower();
        
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
        }
    }
}
