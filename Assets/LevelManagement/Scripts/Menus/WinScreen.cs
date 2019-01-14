using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LevelManagement
{
    public class WinScreen : Menu<WinScreen>
    {
        [SerializeField] private TransitionFader transitionFader;

        public void OnNextLevelPressed()
        {
            StartCoroutine(OnNextLevelPressedCorutine());
        }

        IEnumerator OnNextLevelPressedCorutine()
        {
            TransitionFader.PlayTransition(transitionFader);
            LevelLoader.LoadNextLevel();
            yield return new WaitForSeconds(transitionFader.Delay + transitionFader.FadeOnDuration);
            base.OnBackPressed();
        }

        public void OnRestartPressed()
        {
            base.OnBackPressed();
            LevelLoader.ReloadLevel();
        }

        public void OnMainMenuPressed()
        {
            LevelLoader.LoadMainMenuLevel();
            MainMenu.Open();
        }
    }
}


