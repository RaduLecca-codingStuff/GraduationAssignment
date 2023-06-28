using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubmittingWinMenu : MonoBehaviour
{
    public Text text;

    void Update()
    {
        text.text=GameManager.GetNrOfLives().ToString();
    }
}
