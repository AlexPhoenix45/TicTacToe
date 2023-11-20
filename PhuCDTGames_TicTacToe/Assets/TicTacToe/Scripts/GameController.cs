using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    //Variables
    public int currentRound = 0;
    public TextMeshProUGUI currentRoundText;

    public bool dummyAI;

    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void NextRound()
    {
        currentRound++;
        currentRoundText.text = "Round " + currentRound;
    }
}
