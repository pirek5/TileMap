using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelScore : MonoBehaviour {

    public string maxCoins = "4";
    public string coins = "3";
    public string hearts = "2";
    public int numberOfStars = 3;
    //bool changeNumberOfStars = false;

    //References in editor
    [SerializeField] private IconPanel starsPanel;
    [SerializeField] private Text heartsText;
    [SerializeField] private Text coinsText;

    private void Update()
    {
        hearts = Player.Lives.ToString();
        coinsText.text = coins + "/" + maxCoins;
        heartsText.text = hearts + "/3";
        starsPanel.IconEnable(4);
        starsPanel.IconDisable(4 - numberOfStars);
    }

}
