using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelData {

    public bool isUnlocked;
    public int numberOfStars;

    public LevelData(int stars, bool unlocked)
    {
        isUnlocked = unlocked;
        numberOfStars = stars;
    }
}
