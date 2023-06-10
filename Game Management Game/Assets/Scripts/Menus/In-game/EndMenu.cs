using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndMenu : MonoBehaviour
{
    public Text MainMessage;
    public Text loseContent;
    public Text winContent;
    // Start is called before the first frame update
    void OnEnable()
    {
        if (GameManager._Win)
        {
            MainMessage.text="You Won!";
            winContent.gameObject.SetActive(true);
            loseContent.gameObject.SetActive(false);
            //add code here to have a few changed text
            if (GameManager.ExtraPointReveal()[0]<=5 && GameManager.ExtraPointReveal()[1] <= 5 &&GameManager.ExtraPointReveal()[2] <= 5)
            {
                winContent.text = "Your client was pleased with the product they got out of their development process.";
            }
            else if (GameManager.ExtraPointReveal()[0] <= 10 && GameManager.ExtraPointReveal()[1] <= 10 && GameManager.ExtraPointReveal()[2] <= 10)
            {
                winContent.text = "Your client was rather surpised with the quality of the end product, since it somewhat exceeded their expectations";
            }
            else if (GameManager.ExtraPointReveal()[0] <= 15 && GameManager.ExtraPointReveal()[1] <= 15 && GameManager.ExtraPointReveal()[2] <= 15)
            {
                winContent.text = "Your client was impressed with the quality of your product's development process.It definitely exceeded their expectations.";
            }
            else if (GameManager.sustainability==100 && GameManager.purpose==100 && GameManager.experience == 100)
            {
                winContent.text = "Your client was beyond impressed by the product you made as a result of this process. It has been their most succesful projects yet.";
            }
        }
        else
        {
            MainMessage.text = "You Lost!";
            loseContent.gameObject.SetActive(true);
            winContent.gameObject.SetActive(false);
        }
    }
}
