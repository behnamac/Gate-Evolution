using System;
using DG.Tweening;
using Levels;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Controllers
{
    public class UIManager : MonoBehaviour
    {
        #region PUBLIC PROPS

        public static UIManager Instance { get; private set; }
        #endregion

        #region SERIALIZE FIELDS

        [Header("Panels")]
        [SerializeField] private GameObject gamePlayPanel;

        [SerializeField] private GameObject levelStartPanel;
        [SerializeField] private GameObject levelCompletePanel;
        [SerializeField] private GameObject levelFailPanel;
        [SerializeField] private GameObject tutorialPanel;

        [Header("Coin")]
        [SerializeField] private Image coinIcon;


       


        [Header("Settings Value")]
        [SerializeField] private int hideTutorialLevelIndex;
        [SerializeField] private float levelCompletePanelShowDelayTime;
        [SerializeField] private float levelFailPanelShowDelayTime;
        [SerializeField] private SOMoneyData money;

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

          

            // Set Level Number
           // var levelNumber = LevelManager.Instance.GetLevelNumber() + 1;
           // levelText.text = $"LEVEL {levelNumber}";


        }

        private void ShowTutorial()
        {
            //if (LevelManager.Instance.GetLevelNumber() > hideTutorialLevelIndex) return;
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

            // var totalCoin = PlayerPrefsController.GetTotalCurrency();

            // totalCoin += coinCount;

            // PlayerPrefsController.SetCurrency(totalCoin);

            // coinText.text = totalCoin.ToString();

            coinIcon.transform.DOScale(1.2f, 0.2f).SetEase(Ease.InBounce).OnComplete(() =>
            {
                coinIcon.transform.DOScale(Vector3.one, 0.2f).SetEase(Ease.InBounce);
            });
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

            if (Instance == null) Instance = this;
        }

        private void Start()
        {
            Initializer();

            LevelManager.Instance.OnLevelStart += OnLevelStart;
            LevelManager.Instance.OnLevelComplete += OnLevelComplete;
            LevelManager.Instance.OnLevelFail += OnLevelFail;
        }


        private void OnDestroy()
        {
            LevelManager.Instance.OnLevelStart -= OnLevelStart;
            LevelManager.Instance.OnLevelComplete -= OnLevelComplete;
            LevelManager.Instance.OnLevelFail -= OnLevelFail;
        }

        #endregion
    }
}