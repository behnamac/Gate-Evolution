using Controllers;
using Levels;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameProgressTracker : MonoBehaviour
{

    [SerializeField] private Image scoreBar;
    [SerializeField] private Image stateBar;
    [SerializeField] private TextMeshProUGUI levelTxt;
    [SerializeField] private UpgradeStyle upgradeStyle;

    private float currentScore;
    private float maxScore = 1;

    private float currentLevel;
    private float maxLevel = 1;


    private void Awake()
    {

        if (!upgradeStyle)
            upgradeStyle = GetComponent<UpgradeStyle>();
    }

    private void Start()
    {
        LevelManager.Instance.OnLevelStart += Initialized;
        GameManager.Instance.OnCollect += SetScore;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnCollect -= SetScore;
        LevelManager.Instance.OnLevelStart -= Initialized;

    }



    private void Initialized(Level level)
    {
        stateBar.fillAmount = currentLevel;
        scoreBar.fillAmount = currentScore;
        levelTxt.text = upgradeStyle.GetStyleName();
    }


    public void SetScore(float value)
    {
        var _maxStyleNumber = upgradeStyle.GetStyleNumber();
        currentScore += value / _maxStyleNumber;

        currentScore = Mathf.Clamp(currentScore, 0, maxScore);

        scoreBar.fillAmount = currentScore / maxScore;

        SetStyle(value);
    }



    private void SetStyle(float value)
    {
        currentLevel += value;

        if (currentLevel >= 1)
        {
            currentLevel = 0;
            int _levelUp = 1;
            upgradeStyle.UpgradeCharacter(_levelUp);
        }
        if (currentLevel < 0)
        {
            currentLevel = 0;
            int _levelDown = -1;
            upgradeStyle.UpgradeCharacter(_levelDown);
        }

        stateBar.fillAmount = currentLevel / maxLevel;

        levelTxt.text = upgradeStyle.GetStyleName();

    }




}
