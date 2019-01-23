using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LevelManagement.Data;

namespace LevelManagement
{
    public class ChooseLevelMenu : Menu<ChooseLevelMenu>
    {

        [SerializeField] private IconPanel level1IconPanel;
        [SerializeField] private IconPanel level2IconPanel;
        [SerializeField] private IconPanel level3IconPanel;
        [SerializeField] private IconPanel level4IconPanel;

        public void Init()
        {
            DataManager.instance.Load();
            level1IconPanel.IconEnable(4);
            level1IconPanel.IconDisable(4 - DataManager.instance.LevelsData[0]);
            level2IconPanel.IconEnable(4);
            level2IconPanel.IconDisable(4 - DataManager.instance.LevelsData[1]);
            level3IconPanel.IconEnable(4);
            level3IconPanel.IconDisable(4 - DataManager.instance.LevelsData[2]);
            level4IconPanel.IconEnable(4);
            level4IconPanel.IconDisable(4 - DataManager.instance.LevelsData[3]);
        }

        public void OnLevelPressed(int level)
        {
            LevelLoader.instance.LoadLevel(level);
            GameMenu.Open();
        }

        public override void OnBackPressed()
        {
            base.OnBackPressed();
        }
    }
}
