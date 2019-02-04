using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;

namespace LevelManagement
{
    public class Tutorial : Menu<Tutorial>
    {
        // references set in editor
        [SerializeField] private GameObject[] pages; 
        [SerializeField] private Image ladderRimImage; 
        [SerializeField] private Image crateRimImage;

        //config
        [SerializeField] private float blinkingPeriod;
        [SerializeField] private Vector2 playerStartPosition;
        [SerializeField] private Vector2 crateStartPosition;


        //cached
        private Crate crateToFollow;
        int currentPage;

        public void Init()
        {
            crateToFollow = FindObjectOfType<Crate>();
            crateToFollow.transform.position = crateStartPosition;
            FindObjectOfType<Player>().transform.position = playerStartPosition;
            StartCoroutine(BlinkingRim());
            currentPage = 0;
            pages[0].SetActive(true);
            foreach(TutorialObjective tutorialObj in FindObjectsOfType<TutorialObjective>())
            {
                tutorialObj.Init();
            }
        }

        private void Update()
        {
            crateRimImage.gameObject.transform.position = Camera.main.WorldToScreenPoint(crateToFollow.transform.position);
        }

        public void NextPage()
        {
            currentPage++;
            for(int i = 0; i< pages.Length;i++)
            {
                pages[i].SetActive(false);
                if(i == currentPage)
                {
                    pages[i].SetActive(true);
                }
            }
        }

        public override void OnBackPressed()
        {
            foreach(GameObject page in pages)
            {
                page.SetActive(false);
            }
            base.OnBackPressed();
            base.OnBackPressed();
        }

        IEnumerator BlinkingRim()
        {
            while (true)
            {
                ladderRimImage.enabled = !ladderRimImage.enabled;
                crateRimImage.enabled = !crateRimImage.enabled;
                yield return new WaitForSeconds(blinkingPeriod);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            print("elo");
        }
    }
}


