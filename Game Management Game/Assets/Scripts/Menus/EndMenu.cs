using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndMenu : MonoBehaviour
{
    public Text MainMessage;
    public GameObject loseContent;
    public GameObject winContent;
    // Start is called before the first frame update
    void OnEnable()
    {
        if (GameManager._Win)
        {
            MainMessage.text="You Won!";
            winContent.SetActive(true);
            loseContent.SetActive(false);
        }
        else
        {
            MainMessage.text = "You Lost!";
            loseContent.SetActive(true);
            winContent.SetActive(false);
        }
    }
}
