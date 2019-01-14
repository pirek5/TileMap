using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace LevelManagement
{
    public class PauseMenu : Menu<PauseMenu>
    {
        private void Update()
        {
            if (CrossPlatformInputManager.GetButtonDown("Cancel"))
            {
                OnResumePressed();
            }
        }

        public void OnResumePressed()
        {
            Time.timeScale = 1;
            base.OnBackPressed();
        }

        public void OnRestartPressed()
        {
            print("1");
            if (GameManager.Instance != null)
            {
                print("2");
                Time.timeScale = 1;
                LevelLoader.ReloadLevel();
                base.OnBackPressed();
            }
        }

        public void OnMainMenuPressed()
        {
            Time.timeScale = 1;
            LevelLoader.LoadMainMenuLevel();
            MainMenu.Open();
        }

        public void OnQuitPressed()
        {
            Application.Quit();
        }
    }
}


