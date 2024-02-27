using Elementary.Scripts.Data.Management;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    public static ScoreController Instance { get; private set; }
    [SerializeField] private Image scoreBar;
    private SOScoreData scoreData;
    private float maxScore = 1;
    private ChangeState changeState;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        HandleLoadData();
        changeState = GetComponent<ChangeState>();
    }

    private void OnEnable()
    {
        GameManager.Instance.OnCollect += SetScore;
    }


    public void SetScore(float value)
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
