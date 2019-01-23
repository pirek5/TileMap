﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LevelManagement.Data;

namespace LevelManagement
{
    public class MainMenu : Menu<MainMenu>
    {
        [SerializeField] private InputField inputField;
        private DataManager dataManager;

        protected override void Awake()
        {
            base.Awake();
            dataManager = FindObjectOfType<DataManager>();
        }

        public void Start()
        {
            LoadData();
        }

        public void OnPlayPressed()
        {
            ChooseLevelMenu.Open();
            ChooseLevelMenu menu = FindObjectOfType<ChooseLevelMenu>(); //TODO
            menu.Init();
        }

        public void OnSettingsPressed()
        {
            SettingsMenu.Open();
        }

        public void OnCreditsPressed()
        {
            CreditsMenu.Open();
        }

        public void OnPlayerNameValueChanged()
        {
            if(dataManager != null)
            {
                dataManager.PlayerName = inputField.text;
            }
        }

        public void OnPlayerNameEndEdit()
        {
            if(dataManager != null)
            {
                dataManager.Save();
            }
        }

        public override void OnBackPressed()
        {
            Application.Quit();
        }

        private void LoadData()
        {
            if(dataManager != null && inputField != null)
            {
                dataManager.Load();
                inputField.text = dataManager.PlayerName;
            }       
        }

    }
}
