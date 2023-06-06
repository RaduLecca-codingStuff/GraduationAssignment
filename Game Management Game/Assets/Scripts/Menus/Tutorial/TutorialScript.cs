using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class TutorialScript : MonoBehaviour
{
    int stepsTaken = 0;
    public GameObject menu1;
    public GameObject menu2;
    GameObject stage4;
    GameObject stage5;
    GameObject GameUI;

    [SerializeField]
    Button nextButton;
    [SerializeField]
    Text description;

    CameraMovementScript _mainCameraMovement;
    PhoneJoystick _joystick;

    // Start is called before the first frame update
    void OnEnable()
    {
        UpdateStep();
        _mainCameraMovement=Camera.main.GetComponent<CameraMovementScript>();
        _mainCameraMovement.ResetUse();
        if (GameManager.mobileJoystick)
        {
            if (GameManager.mobileJoystick.TryGetComponent<PhoneJoystick>(out PhoneJoystick p))
            {
                _joystick = p;
                _joystick.ResetUse();
            }
        }
        menu1.SetActive(false);
        menu2.SetActive(false);
        GameManager.WasCheckedForInfo = false;
        GameManager.WasPlaced = false;
        GameManager.WasMenuOpened = false;
    }

    // Update is called once per frame
    void Update()
    {
        switch (stepsTaken)
        {
            case 1:
                if (GameManager.isMobile && _joystick.CheckIfUsed())
                {
                    nextButton.interactable = true;
                }
                else
                {
                    if(Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow) ||
                        Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D)) 
                    {
                        nextButton.interactable = true;
                    }
                }
                
                break;
            case 2:
                if(GameManager.WasPlaced)
                    nextButton.interactable = true;
                break;
            case 3:
                if (GameManager.WasCheckedForInfo)
                    nextButton.interactable = true;
                break;
            case 4:
                if (GameManager.WasMenuOpened)
                    nextButton.interactable = true;
                break;
            case 5:
                
                break;
            default:
                break;

        }
    }
    public void UpdateStep()
    {
        nextButton.interactable = false;
        stepsTaken++;
        switch (stepsTaken)
        {
            case 1:
                if (GameManager.isMobile)
                {
                    description.text = "To move around the board, use the joystick to your right.";
                }
                else
                    description.text = "To move around the board, use the arrow keys .";
                break;
            case 2:
                if (GameManager.isMobile)
                {
                    description.text = "To place and move methods on the board, tap on the hexagon method, then on where do you want to place it on the board.";
                }
                else
                    description.text = "To place and move methods on the board, click on the hexagon method, then on where do you want to place it on the board.";
                menu1.SetActive(true);
                break;
            case 3:
                if (GameManager.isMobile)
                {
                    description.text = "To find out more information about the method, tap on the method and then on the method info screen below.";
                }
                else
                    description.text = "To find out more information about the method, click on the method and then on the method info screen below.";
                menu2.SetActive(true);
                break;
            case 4:
                description.text = "Hold on the method for a bit to show the method's person and resource menu. There, you can place methods and resources in their respective slots.";
                break;
            case 5:
                description.text = "Methods and resources can be found in the pieces menu. Persons work only on certain methods, so make sure to pair them properly.";
                break;
            case 6:
                nextButton.GetComponentInChildren<Text>().text = "Begin";
                description.text = "Your end goal is to meet the client's criteria to make a development process with the methods offered to make a product that would meet the client's criteria for sustaiability, user experience and purpose for their desired product.";

                break;
            case 7:
                ResetMenu1();
                nextButton.GetComponentInChildren<Text>().text = "Next";
                menu2.GetComponent<MDesMenuScript>().Clear();
                GameManager.StartGame();
                break;

            default:
                break;

        }
        
    }

    public void SetInteractable()
    {
        nextButton.interactable = true;
    }

    void ResetMenu1()
    {
        UIHexagon[] uihexagons=GameObject.FindObjectsOfType<UIHexagon>(true).Where(sr => !sr.gameObject.activeInHierarchy).ToArray();
        foreach(UIHexagon ui in uihexagons)
        {
            ui.gameObject.SetActive(true);
        }
        HexagonPiece[] pieces= GameObject.FindObjectsOfType<HexagonPiece>(true);
        foreach(HexagonPiece piece in pieces)
        {
            Destroy(piece.gameObject);
        }

    }
}
