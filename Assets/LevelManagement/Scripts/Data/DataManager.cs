using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelManagement.Data
{
    public class DataManager : MonoBehaviour
    {
        [SerializeField] int numberOfLevels = 4;
        private SaveData saveData;
        private JsonSaver jsonSaver;
        //public Dictionary<int, LevelData> LevelsData = new Dictionary<int, LevelData>();

        public static  DataManager instance;

        private void Awake()
        {
            if (instance != null)
            {
                Destroy(this.gameObject);
            }
            else
            {
                instance = this;
                saveData = new SaveData();
                jsonSaver = new JsonSaver();
            }
        }

        private void Start()
        {
            LoadDefault();
        }

        private void OnDestroy()
        {
            if (instance == this)
            {
                instance = null;
            }
        }

        public float MasterVolume
        {
            get { return saveData.masterVolume; }
            set { saveData.masterVolume = value; }
        }

        public float SFXVolume
        {
            get { return saveData.sfxVolume; }
            set { saveData.sfxVolume = value; }
        }

        public float MusicVolume
        {
            get { return saveData.musicVolume; }
            set { saveData.musicVolume = value; }
        }

        public int[] LevelsData
        {
            get { return saveData.LevelsData; }
            set { saveData.LevelsData = value; }
        }

        public string PlayerName
        {
            get { return saveData.playerName; }
            set { saveData.playerName = value; }
        }

        public void Save()
        {
            jsonSaver.Save(saveData);
        }

        public void Load()
        {
            jsonSaver.Load(saveData);
        }

        public void Delete()
        {
            jsonSaver.Delete();
        }

        public void LoadDefault()
        {
            LevelsData[0] = 1;
            LevelsData[1] = 0;
            LevelsData[2] = 0;
            LevelsData[3] = 0;
        }
    }
}


