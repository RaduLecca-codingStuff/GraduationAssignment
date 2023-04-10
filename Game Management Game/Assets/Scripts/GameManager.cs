using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static bool isMobile=false;
    public static GameObject mobileJoystick;
    public static int nrOfLives = 3;
    static bool _Win=false;
    public static int purpose=0;
    public static int sustainability=0;
    public static int experience=0;
    public static ClusterManager mainClusterM = new ClusterManager();
    public static HexagonPiece selectedPiece = new HexagonPiece();

    public static Client currentClient;

    public static int availableChances;

    // Start is called before the first frame update
    void Start()
    {
        if (!isMobile)
        {
            mobileJoystick.GetComponent<Image>().enabled = false;
            mobileJoystick.SetActive(false);
        }
        GetNewClient();
    }
    // Update is called once per frame
    void Update()
    {
        RangeCheck(experience);
        RangeCheck(purpose);
        RangeCheck(sustainability);
    }
    public Vector3 GetMousePos()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
    void RangeCheck(int val)
    {
        if (val >= 100)
        { val = 100; }
        else if (val <= 0)
        { val = 0; }
    }
    public void GetNewClient()
    {
        int rnd = Random.Range(0, 10);

        //Removoe this and then make sure that the game generates different people for each case
        List<Person> persons = new List<Person>();

        switch (rnd)
        {
            case 0:
                currentClient = new Client("Blank client", "Blank description here because this is a placeholder client until a set of base clients is added to the game", 50, 50, 50, persons);
                currentClient.SetInvestmentTypes(1, 0, 1, 2, 1, 1);
                break;
            case 1:
                currentClient = new Client("Blank client", "Blank description here because this is a placeholder client until a set of base clients is added to the game", 50, 50, 50, persons);
                currentClient.SetInvestmentTypes(1, 0, 1, 2, 1, 1);
                break;
            case 2:
                currentClient = new Client("Blank client", "Blank description here because this is a placeholder client until a set of base clients is added to the game", 50, 50, 50, persons);
                currentClient.SetInvestmentTypes(1, 0, 1, 2, 1, 1);
                break;
            case 3:
                currentClient = new Client("Blank client", "Blank description here because this is a placeholder client until a set of base clients is added to the game", 50, 50, 50, persons);
                currentClient.SetInvestmentTypes(1, 0, 1, 2, 1, 1);
                break;
            case 4:
                currentClient = new Client("Blank client", "Blank description here because this is a placeholder client until a set of base clients is added to the game", 50, 50, 50, persons);
                currentClient.SetInvestmentTypes(1, 0, 1, 2, 1, 1);
                break;
            case 5:
                currentClient = new Client("Blank client", "Blank description here because this is a placeholder client until a set of base clients is added to the game", 50, 50, 50, persons);
                currentClient.SetInvestmentTypes(1, 0, 1, 2, 1, 1);
                break;
            case 6:
                currentClient = new Client("Blank client", "Blank description here because this is a placeholder client until a set of base clients is added to the game", 50, 50, 50, persons);
                currentClient.SetInvestmentTypes(1, 0, 1, 2, 1, 1);
                break;
            case 7:
                currentClient = new Client("Blank client", "Blank description here because this is a placeholder client until a set of base clients is added to the game", 50, 50, 50, persons);
                currentClient.SetInvestmentTypes(1, 0, 1, 2, 1, 1);
                break;
            case 8:
                currentClient = new Client("Blank client", "Blank description here because this is a placeholder client until a set of base clients is added to the game", 50, 50, 50, persons);
                currentClient.SetInvestmentTypes(1, 0, 1, 2, 1, 1);
                break;
            case 9:
                currentClient = new Client("Blank client", "Blank description here because this is a placeholder client until a set of base clients is added to the game", 50, 50, 50, persons);
                currentClient.SetInvestmentTypes(1, 0, 1, 2, 1, 1);
                break;
            case 10:
                currentClient = new Client("Blank client", "Blank description here because this is a placeholder client until a set of base clients is added to the game", 50, 50, 50, persons);
                currentClient.SetInvestmentTypes(1, 0, 1, 2, 1, 1);
                break;
            default:
                currentClient = new Client("Blank client", "Blank description here because this is a placeholder client until a set of base clients is added to the game", 50, 50, 50, persons);
                currentClient.SetInvestmentTypes(1, 1, 1, 1, 1, 1);
                break;
        }

        Debug.Log("Blank client was succesfully added");
    }
    public static void CheckHealth()
    {
        if(currentClient != null)
        {
            if(nrOfLives > 0)
            {
                if (experience >= currentClient.reqExperience && sustainability >= currentClient.reqSustainability && purpose >= currentClient.reqPurpose)
                {
                    _Win = true;
                }
                else
                {
                    nrOfLives--;
                }
            }
        }
    }
    public static void TryAgain()
    {
        nrOfLives = 3;
        _Win = false;
    }
}
