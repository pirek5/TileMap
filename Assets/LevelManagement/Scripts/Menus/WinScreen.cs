using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LevelManagement
{
    public class WinScreen : Menu<WinScreen>
    {
        [SerializeField] private TransitionFader transitionFader;

        [SerializeField] private GameObject firstPage;
        [SerializeField] private GameObject secsondPage;

        public void OnContinePressed()
        {
            SwitchPages();
        }

        public void OnNextLevelPressed()
        {
            SwitchPages();
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
            SwitchPages();
            base.OnBackPressed();
            LevelLoader.ReloadLevel();
        }

        public void OnMainMenuPressed()
        {
            SwitchPages();
            LevelLoader.LoadMainMenuLevel();
            MainMenu.Open();
        }

        public void SwitchPages()
        {
            firstPage.SetActive(!firstPage.activeSelf);
            secsondPage.SetActive(!secsondPage.activeSelf);
        }
    }
}


