using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static bool isMobile=false;
    public static GameObject mobileJoystick;
    static int nrOfLives = 3;
    public static bool _Win=false;
    public static float purpose=0;
    public static float sustainability=0;
    public static float experience=0;
    public static ClusterManager mainClusterM = new ClusterManager();

    public static HexagonPiece selectedPiece = new HexagonPiece();
    public static ResourceObject currentRes;
    public static PersonObject currentPer;

    public static Client currentClient;

    public static int availableChances;

    int prevrnd = 10;

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
    void RangeCheck(float val)
    {
        if (val >= 100)
        { val = 100; }
        else if (val <= 0)
        { val = 0; }
    }
    public void GetNewClient()
    {
        int rnd = Random.Range(0, 9);

        //Removoe this and then make sure that the game generates different people for each case
        List<Person> persons = new List<Person>();
        if(rnd!=prevrnd) 
        {
            switch (rnd)
            {
                case 0:
                    currentClient = new Client("Helix Technologies", "A technology company focused on developing cutting-edge virtual reality and augmented reality gaming experiences.", 50, 50, 50, persons);
                    currentClient.SetInvestment(1, 5, 5);
                    persons.Add(new Person(Person.Occupation.EndUser));
                    currentClient.SetPeople(persons);
                    break;
                case 1:
                    currentClient = new Client("Trove Games", "A video game company specializing in multiplayer online role-playing games.", 50, 50, 50, persons);
                    currentClient.SetInvestment(7, 5, 4);
                    persons.Add(new Person(Person.Occupation.EndUser));
                    currentClient.SetPeople(persons);
                    break;
                case 2:
                    currentClient = new Client("MoonScape", "A leading mobile gaming company offering a variety of exciting and innovative titles.", 50, 50, 50, persons);
                    currentClient.SetInvestment(0, 3, 6);
                    persons.Add(new Person(Person.Occupation.EndUser));
                    currentClient.SetPeople(persons);
                    break;
                case 3:
                    currentClient = new Client("HyperTek", "A technology company that specializes in developing cyber security solutions for businesses.", 50, 50, 50, persons);
                    currentClient.SetInvestment(5,5, 5);
                    persons.Add(new Person(Person.Occupation.EndUser));
                    currentClient.SetPeople(persons);
                    break;
                case 4:
                    currentClient = new Client("DigiPlay", "An independent video game developer and publisher that develops original titles for multiple platforms.", 50, 50, 50, persons);
                    currentClient.SetInvestment(7, 3, 4);
                    persons.Add(new Person(Person.Occupation.EndUser));
                    currentClient.SetPeople(persons);
                    break;
                case 5:
                    currentClient = new Client("Stratos Systems", "A technology company specializing in the development of innovative cloud-based software solutions.", 50, 50, 50, persons);
                    currentClient.SetInvestment(3,5, 2);
                    persons.Add(new Person(Person.Occupation.EndUser));
                    currentClient.SetPeople(persons);
                    break;
                case 6:
                    currentClient = new Client("Automata Systems", "A robotics and artificial intelligence company that produces autonomous machines to help with everyday tasks.", 50, 50, 50, persons);
                    currentClient.SetInvestment(4, 5, 7);
                    persons.Add(new Person(Person.Occupation.EndUser));
                    currentClient.SetPeople(persons);
                    break;
                case 7:
                    currentClient = new Client("Forge Entertainment", "An entertainment company that produces and distributes top-tier video games and virtual reality experiences.", 50, 50, 50, persons);
                    currentClient.SetInvestment(1, 6, 4);
                    persons.Add(new Person(Person.Occupation.EndUser));
                    currentClient.SetPeople(persons);
                    break;
                case 8:
                    currentClient = new Client("NoduX", "A tech company focused on creating innovative, user-friendly web and mobile applications.", 50, 50, 50, persons);
                    currentClient.SetInvestment(2, 2, 7);
                    persons.Add(new Person(Person.Occupation.EndUser));
                    currentClient.SetPeople(persons);
                    break;
                case 9:
                    currentClient = new Client("Outliers Technologies", "A technology company that specializes in creating innovative, user-friendly hardware and software solutions.", 50, 50, 50, persons);
                    currentClient.SetInvestment(1, 0, 1);
                    persons.Add(new Person(Person.Occupation.EndUser));
                    currentClient.SetPeople(persons);
                    break;
                default:
                    break;
            }
            prevrnd = rnd;
        }

        Debug.Log("Blank client was succesfully added");
    }
    public static void CheckIfWon()
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
    public static int GetNrOfLives()
    {
        return nrOfLives;
    }
    public static void TryAgain()
    {
        nrOfLives = 3;
        _Win = false;
        GameObject[] clustersToDelete = GameObject.FindGameObjectsWithTag("Cluster");
        GameObject[] hexagonsToReplace = GameObject.FindGameObjectsWithTag("UIHexagon");

        foreach(GameObject cluster in clustersToDelete)
        {
            Destroy(cluster);
        }
        foreach(GameObject hexagon in hexagonsToReplace)
        {
            hexagon.SetActive(true);
        }

    }
}
