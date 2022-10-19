using Levels;
using Storage;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Controllers
{
    public class UiController : MonoBehaviour
    {
        #region PUBLIC PROPS

        public static UiController Instance { get; private set; }

        #endregion

        #region SERIALIZE FIELDS

        [Header("Panels")]
        [SerializeField] private GameObject gamePlayPanel;

        [SerializeField] private GameObject levelStartPanel;
        [SerializeField] private GameObject levelCompletePanel;
        [SerializeField] private GameObject levelFailPanel;
        [SerializeField] private GameObject tutorialPanel;
        
        [SerializeField] private Text coinText;
        [SerializeField] private Text levelFinishCoinText;

        [Header("Level")]
        [SerializeField] private Text levelText;

        [Header("Settings Value")]
        [SerializeField] private int hideTutorialLevelIndex;

        [SerializeField] private float levelCompletePanelShowDelayTime;
        [SerializeField] private float levelFailPanelShowDelayTime;

        #endregion

        #region PRIVATE FIELDS

        private int _levelFinishTotalCount;

        #endregion

        #region PRIVATE METHODS

        private void Initializer()
        {
            // Level Start
            levelStartPanel.GetComponentInChildren<Button>().onClick.AddListener(() =>
            {
                levelStartPanel.SetActive(false);
                LevelManager.Instance.LevelStart();
            });

            // Level Complete
            levelCompletePanel.GetComponentInChildren<Button>().onClick.AddListener(() =>
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            });

            // Level Fail
            levelFailPanel.GetComponentInChildren<Button>().onClick.AddListener(() =>
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            });

            // Set TotalCoin;
            coinText.text = PlayerPrefsController.GetTotalCurrency().ToString();

            // Set Level Number
            var levelNumber = PlayerPrefsController.GetLevelNumber() + 1;
            levelText.text = $"LEVEL {levelNumber}";
        }

        private void ShowTutorial()
        {
            if (PlayerPrefsController.GetLevelIndex() > hideTutorialLevelIndex) return;
            tutorialPanel.SetActive(true);

            Invoke(nameof(HideTutorial), 3);
        }

        private void HideTutorial()
        {
            tutorialPanel.SetActive(false);
        }

        

        private void ShowLevelCompletePanel()
        {
            if (tutorialPanel.activeSelf)
            {
                tutorialPanel.SetActive(false);
            }

            levelCompletePanel.SetActive(true);
        }

        private void ShowLevelFailPanel()
        {
            if (tutorialPanel.activeSelf)
            {
                tutorialPanel.SetActive(false);
            }

            levelFailPanel.SetActive(true);
        }

        #endregion

        #region PUBLIC METHODS

        public void AddCoin(int coinCount)
        {

            var totalCoin = PlayerPrefsController.GetTotalCurrency();

            totalCoin += coinCount;

            PlayerPrefsController.SetCurrency(totalCoin);

            coinText.text = totalCoin.ToString();

            
        }

        public void LevelFinishCoinCount(int coinCount)
        {

            _levelFinishTotalCount += coinCount;

            levelFinishCoinText.text = _levelFinishTotalCount.ToString();
        }

        
        

        #endregion

        #region CUSTOM EVENTS

        private void OnLevelFail(Level levelData)
        {
            Invoke(nameof(ShowLevelFailPanel), levelFailPanelShowDelayTime);
        }

        private void OnLevelStart(Level levelData)
        {
            ShowTutorial();
            gamePlayPanel.SetActive(true);
        }

        private void OnLevelComplete(Level levelData)
        {
            Invoke(nameof(ShowLevelCompletePanel), levelCompletePanelShowDelayTime);
        }

        private void OnLevelStageComplete(Level levelData, int stageIndex)
        {
            // TODO : IF DONT NEED THIS METHODS, YOU DONT REMOVE
        }

        #endregion

        #region UNITY EVENT METHODS

        private void Awake()
        {
            Initializer();

            if (Instance == null) Instance = this;
        }

        private void Start()
        {
            LevelManager.OnLevelStart += OnLevelStart;
            LevelManager.OnLevelComplete += OnLevelComplete;
            LevelManager.OnLevelFail += OnLevelFail;
            LevelManager.OnLevelStageComplete += OnLevelStageComplete;
        }


        private void OnDestroy()
        {
            LevelManager.OnLevelStart -= OnLevelStart;
            LevelManager.OnLevelComplete -= OnLevelComplete;
            LevelManager.OnLevelFail -= OnLevelFail;
            LevelManager.OnLevelStageComplete -= OnLevelStageComplete;
        }

        #endregion
    }
}