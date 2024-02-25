using Elementary.Scripts.Data.Management;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    [SerializeField] private Image scoreBar;
    private SOScoreData scoreData;
    private float maxScore = 1;
    private ChangeState changeState;

    private void Awake()
    {
        HandleLoadData();
        changeState = GetComponent<ChangeState>();
    }

    private void OnEnable()
    {
        GameManager.Instance.OnCollect += SetScore;
    }


    private void SetScore(float value)
    {
        scoreData.Score = value;
        scoreBar.fillAmount = scoreData.Score / maxScore;
        changeState.CheckPlayerLevel();
        SaveData();
    }


    private void HandleLoadData()
    {
        var loadedData = DataManager.GetWithJson<SOScoreData>();
        if (loadedData == null)
        {
            scoreData = new SOScoreData()
            {
                Score = 0,
            };

            SaveData();
        }
        else
            scoreData = loadedData;
        scoreBar.fillAmount = scoreData.Score / maxScore;

    }

    private void SaveData()
    {
        DataManager.SaveWithJson(scoreData);
    }
}
