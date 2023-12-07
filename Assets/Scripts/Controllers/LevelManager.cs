using Levels;
using Storage;
using UnityEngine;
using UnityEngine.Events;

namespace Controllers
{
    public class LevelManager : MonoBehaviour
    {
        public enum GameState { Ready, Start,XLine, Finish }

        #region DELEGATE

        public static UnityAction<Level> OnLevelLoad;
        public static UnityAction<Level> OnLevelStart;
        public static UnityAction<Level> OnLevelComplete;
        public static UnityAction<Level> OnLevelStageComplete;
        public static UnityAction<Level> OnLevelFail;
        #endregion

        #region PUBLIC FIELDS / PROPS

        public static LevelManager Instance { get; private set; }

        #endregion

        #region SERIALIZE PRIVATE FIELDS

        [SerializeField] private GameState gameState = GameState.Ready;
        [SerializeField] private LevelSource levelSource;

        [SerializeField] private GameObject levelSpawnPoint;

        [SerializeField] private int loopLevelsStartIndex = 1;

        [SerializeField] private bool loopLevelGetRandom = true;

        #endregion

        #region PRIVATE FIELDS

        private GameObject _activeLevel;

        #endregion

        #region PRIVATE METHODS

        private void CheckRepeatLevelIndex()
        {
            if (loopLevelsStartIndex < levelSource.levelData.Length) return;
            loopLevelsStartIndex = 0;
        }

        private GameObject GetLevel()
        {
            if (PlayerPrefsController.GetLevelIndex() >= levelSource.levelData.Length)
            {
                if (loopLevelGetRandom)
                {
                    var levelIndex = Random.Range(loopLevelsStartIndex, levelSource.levelData.Length - 1);
                    PlayerPrefsController.SetLevelIndex(levelIndex);
                }
            }

            var level = levelSource.levelData[PlayerPrefsController.GetLevelIndex()];

            var levelData = level.GetComponent<Level>();

            levelData.levelIndex = PlayerPrefsController.GetLevelIndex();
            levelData.levelNumber = PlayerPrefsController.GetLevelNumber();

            return level;
        }

        #endregion

        #region PUBLIC METHODS


        public void LevelLoad()
        {
            _activeLevel = Instantiate(GetLevel(), levelSpawnPoint.transform, false);
            OnLevelLoad?.Invoke(_activeLevel.GetComponent<Level>());
        }


        public void LevelStart()
        {
            OnLevelStart?.Invoke(_activeLevel.GetComponent<Level>());
            gameState = GameState.Start;
        }

        public void LevelStageComplete()
        {
            OnLevelStageComplete?.Invoke(_activeLevel.GetComponent<Level>());
            gameState = GameState.XLine;
        }


        public void LevelComplete()
        {
            PlayerPrefsController.SetLevelIndex(PlayerPrefsController.GetLevelIndex() + 1);

            PlayerPrefsController.SetLevelNumber(PlayerPrefsController.GetLevelNumber() + 1);

            OnLevelComplete?.Invoke(_activeLevel.GetComponent<Level>());
            gameState = GameState.Finish;
        }


        public void LevelFail()
        {
            OnLevelFail?.Invoke(_activeLevel.GetComponent<Level>());
        }

        #endregion

        #region UNITY EVENT METHODS

        private void Awake()
        {
            CheckRepeatLevelIndex();
            if (!Instance)
                Instance = this;
        }

        private void Start() => LevelLoad();

        #endregion
    }
}