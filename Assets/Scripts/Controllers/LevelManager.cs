using Elementary.Scripts.Data.Management;
using Levels;
using System;
using UnityEngine;

namespace Controllers
{
    public class LevelManager : MonoBehaviour
    {
        #region DELEGATE

        public Action<Level> OnLevelLoad;

        public Action<Level> OnLevelStart;

        public Action<Level> OnLevelComplete;

        public Action<Level> OnLevelFail;

        #endregion


        #region PUBLIC FIELDS / PROPS

        public static LevelManager Instance { get; private set; }

        #endregion

        #region SERIALIZE PRIVATE FIELDS

        [SerializeField] private LevelSource levelSource;

        [SerializeField] private GameObject levelSpawnPoint;

        [SerializeField] private int loopLevelsStartIndex = 1;

        [SerializeField] private bool loopLevelGetRandom = true;

        #endregion

        #region PRIVATE FIELDS

        private GameObject _activeLevel;
        private SOLevelData _saveData;



        #endregion

        #region PRIVATE METHODS

        private void CheckRepeatLevelIndex()
        {
            if (loopLevelsStartIndex < levelSource.levelData.Length) return;
            loopLevelsStartIndex = 0;
        }

        private GameObject GetLevel()
        {
            var levelIndex = _saveData.LevelIndex;
            if (levelIndex >= levelSource.levelData.Length)
            {
                if (loopLevelGetRandom)
                {
                    var randomLevelIndex = UnityEngine.Random.Range(loopLevelsStartIndex, levelSource.levelData.Length - 1);
                    _saveData.LevelIndex = randomLevelIndex;
                }
            }

            var level = levelSource.levelData[levelIndex];

            var levelData = level.GetComponent<Level>();

            levelData.levelIndex = levelIndex;
            levelData.levelNumber = levelIndex + 1;
            SaveData();
            return level;
        }


        private void HandleLoadData()
        {
            var loadedData = DataManager.GetWithJson<SOLevelData>();
            if (loadedData == null)
            {
                _saveData = new SOLevelData()
                {
                    LevelIndex = 0,
                };

                SaveData();
            }
            else
                _saveData = loadedData;
        }

        private void SaveData()
        {
            DataManager.SaveWithJson(_saveData);
        }

        #endregion

        #region PUBLIC METHODS

        /// <summary>
        ///     Method that loads the next level
        /// </summary>
        public void LevelLoad()
        {
            _activeLevel = Instantiate(GetLevel(), levelSpawnPoint.transform, false);
            OnLevelLoad?.Invoke(_activeLevel.GetComponent<Level>());
        }

        /// <summary>
        ///     Method that starts the last loaded level
        /// </summary>
        public void LevelStart()
        {
            OnLevelStart?.Invoke(_activeLevel.GetComponent<Level>());
        }



        public void LevelComplete()
        {
            _saveData.LevelIndex += 1;
            SaveData();
            OnLevelComplete?.Invoke(_activeLevel.GetComponent<Level>());
        }


        /// <summary>
        ///     The method to be called when the level played fails.
        /// </summary>
        public void LevelFail()
        {
            OnLevelFail?.Invoke(_activeLevel.GetComponent<Level>());
        }

        #endregion

        #region UNITY EVENT METHODS

        private void Awake()
        {
            HandleLoadData();
            CheckRepeatLevelIndex();
            Instance = this;
        }

        private void Start() => LevelLoad();

        #endregion
    }
}