using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LevelManagement
{
    public class LevelLoader : MonoBehaviour
    {
        private static int mainMenuIndex = 1;

        public static void LoadLevel(string levelName)
        {
            if (Application.CanStreamedLevelBeLoaded(levelName))
            {
                SceneManager.LoadScene(levelName);
            }
            else
            {
                Debug.LogWarning("LEVELLOADER Loadlevel Error: invalid scene specified!");
            }

        }

        public static void LoadLevel(int levelIndex)
        {
            if (levelIndex >= 0 && levelIndex < SceneManager.sceneCountInBuildSettings)
            {
                if (levelIndex == mainMenuIndex)
                {
                    MainMenu.Open();
                }
                SceneManager.LoadScene(levelIndex);
            }
            else
            {
                Debug.LogWarning("LEVELLOADER Loadlevel Error: invalid scene specified!");
            }
        }

        public static void ReloadLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public static void LoadNextLevel()
        {
            int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
            if(nextSceneIndex >= SceneManager.sceneCountInBuildSettings)
            {
                LoadMainMenuLevel();
            }
            else
            {
                LoadLevel(nextSceneIndex);
            }
        }

        public static void LoadMainMenuLevel()
        {
            LoadLevel(mainMenuIndex);
        }
    }
}



