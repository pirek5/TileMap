using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class HealthPanel : MonoBehaviour {

    List<Heart> hearts;
    List<Heart> reversedHearts = new List<Heart>();

    void Start ()
    {
        Heart[] tempHearts = FindObjectsOfType<Heart>();
        hearts = tempHearts.OrderBy(o => o.heartIndex).ToList();  // sorted list by index
        foreach (Heart heart in hearts)
        {
            reversedHearts.Insert(0,heart); //reversed list by index
        }
    }

    public void HeartLose(int heartsToLose)
    {
        foreach(Heart heart in hearts)
        {
            int i = 0;
            if (heart.isCollected)
            {
                i++;
                heart.ChangeHealth();
                if(i>= heartsToLose)
                {
                    return;
                }
            }
        }
    }

    public void HeartGain()
    {
        foreach(Heart heart in reversedHearts)
        {
            if (!heart.isCollected)
            {
                heart.ChangeHealth();
                return;
            }
        }
    }
}
