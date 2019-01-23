using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LevelManagement.Data;
using UnityEngine.SceneManagement;


namespace LevelManagement
{
    public class WinScreen : Menu<WinScreen>
    {
        [SerializeField] private GameObject firstPage;
        [SerializeField] private GameObject secsondPage;

        public void OnContinePressed() // przeniść do DataManager albo gamemanager
        {
            int currentLevelStars = LevelScoreManager.Instance.stars;
            int nextLevelStars = 0;
            int oldCurrentLevelStars = DataManager.instance.LevelsData[SceneManager.GetActiveScene().buildIndex-2];
            int oldNextLevelStars = DataManager.instance.LevelsData[SceneManager.GetActiveScene().buildIndex-1];

            if (currentLevelStars > oldCurrentLevelStars)
            {
                DataManager.instance.LevelsData[SceneManager.GetActiveScene().buildIndex-2] = currentLevelStars;
            }
            if (nextLevelStars > oldNextLevelStars)
            {
                DataManager.instance.LevelsData[SceneManager.GetActiveScene().buildIndex-1] = nextLevelStars;
            }
            DataManager.instance.Save();
            SwitchPages();
        }

        public void OnNextLevelPressed()
        {
            SwitchPages();
            base.OnBackPressed();
            LevelLoader.instance.LoadNextLevel();
        }

        public void OnRestartPressed()
        {
            SwitchPages();
            base.OnBackPressed();
            LevelLoader.instance.ReloadLevel();
        }

        public void OnMainMenuPressed()
        {
            SwitchPages();
            LevelLoader.instance.LoadMainMenuLevel();
            MainMenu.Open();
        }

        private void SwitchPages()
        {
            firstPage.SetActive(!firstPage.activeSelf);
            secsondPage.SetActive(!secsondPage.activeSelf);
        }
    }
}


