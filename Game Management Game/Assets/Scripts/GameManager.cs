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
    public static int difficulty = 1;

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
        List<Person> persons = new List<Person>
        {
            new Person(Person.Occupation.ProductOwner),
            new Person(Person.Occupation.ProjectManager),
            new Person(Person.Occupation.EndUser),
            new Person(Person.Occupation.Stakeholder)
        };
        if (rnd!=prevrnd) 
        {
            switch (rnd)
            {
                case 0:
                    persons.Add(new Person(Person.Occupation.Stakeholder));
                    persons.Add(new Person(Person.Occupation.Programmer));
                    persons.Add(new Person(Person.Occupation.Programmer));
                    persons.Add(new Person(Person.Occupation.ExperienceExpert));
                    persons.Add(new Person(Person.Occupation.Tester));
                    persons.Add(new Person(Person.Occupation.PSO));
                    persons.Add(new Person(Person.Occupation.SME));
                    currentClient = new Client("Helix Technologies", "A large technology company focused on developing cutting-edge virtual reality and augmented reality gaming experiences." +
                    " They've been operating for 5 years and have agreed with you to find a way to create a way to make a new VR game they've been planning to make for several years.", 60, 50, 60, persons,5);
                    currentClient.SetInvestment(2, 6, 6);
                    break;
                case 1:
                    persons.Add(new Person(Person.Occupation.Programmer));
                    persons.Add(new Person(Person.Occupation.VisualDesigner));
                    persons.Add(new Person(Person.Occupation.ExperienceExpert));
                    persons.Add(new Person(Person.Occupation.UXdesigner));
                    persons.Add(new Person(Person.Occupation.Tester));
                    persons.Add(new Person(Person.Occupation.EndUser));
                    currentClient = new Client("Trove Games", "A video game company specializing in multiplayer online role-playing games. " +
                    " This company has gotten in contact with you in order to make their third MMO game, which they just hired a new team's worth of people for it and planned to have it be a side game compared to the other two titles they already have.", 40, 30, 50, persons, 4);
                    currentClient.SetInvestment(8, 6, 5);
                    break;
                case 2:
                    persons.Add(new Person(Person.Occupation.Programmer));
                    persons.Add(new Person(Person.Occupation.Programmer));
                    persons.Add(new Person(Person.Occupation.Programmer));
                    persons.Add(new Person(Person.Occupation.VisualDesigner));
                    persons.Add(new Person(Person.Occupation.VisualDesigner));
                    persons.Add(new Person(Person.Occupation.VisualDesigner));
                    persons.Add(new Person(Person.Occupation.ExperienceExpert));
                    persons.Add(new Person(Person.Occupation.UXdesigner));
                    persons.Add(new Person(Person.Occupation.UXdesigner));
                    persons.Add(new Person(Person.Occupation.Tester));
                    persons.Add(new Person(Person.Occupation.Tester));
                    currentClient = new Client("MoonScape", "A leading mobile gaming company offering a variety of exciting and innovative titles." +
                    " They've been succesfully working for 6 years and now they decided to partner up with you to re-work a dropped-off project." +
                    " However, they don't have a plan to how they're going to re-vamp the project, so they wish to start from scratch.", 60, 30, 70, persons, 4);
                    currentClient.SetInvestment(1, 4, 7);
                    break;
                case 3:
                    persons.Add(new Person(Person.Occupation.Programmer));
                    persons.Add(new Person(Person.Occupation.Programmer));
                    persons.Add(new Person(Person.Occupation.Tester));
                    persons.Add(new Person(Person.Occupation.Tester));
                    persons.Add(new Person(Person.Occupation.PSO));
                    persons.Add(new Person(Person.Occupation.SME));
                    currentClient = new Client("HyperTek", "A technology company that specializes in developing cyber security solutions for businesses." +
                    " You've agreed to work with them to develop a security system for small businesses that's affordable to broaden the available services provided by the company.", 60, 40, 40, persons, 3);
                    currentClient.SetInvestment(6,6, 6);
                    break;
                case 4:
                    persons.Add(new Person(Person.Occupation.Programmer));
                    persons.Add(new Person(Person.Occupation.VisualDesigner));
                    persons.Add(new Person(Person.Occupation.ExperienceExpert));
                    persons.Add(new Person(Person.Occupation.UXdesigner));
                    persons.Add(new Person(Person.Occupation.Tester));
                    currentClient = new Client("DigiPlay", "An independent video game developer and publisher that develops original titles for multiple platforms." +
                    " As a small company, they want to try to make a much more fleshed out game, but they have a limited ammount of resources and persons that they have together. ", 30, 50, 60, persons, 2);
                    currentClient.SetInvestment(8, 4, 5);
                    break;
                case 5:
                    persons.Add(new Person(Person.Occupation.Stakeholder));
                    persons.Add(new Person(Person.Occupation.Programmer));
                    persons.Add(new Person(Person.Occupation.ExperienceExpert));
                    persons.Add(new Person(Person.Occupation.Tester));
                    persons.Add(new Person(Person.Occupation.PSO));
                    persons.Add(new Person(Person.Occupation.SME));
                    currentClient = new Client("Stratos Systems", "A technology company specializing in the development of innovative cloud-based software solutions." +
                    " Through your collaboration with them, you now have to make a secure software meant to save the data from a client's social media page.", 70, 80, 50, persons, 3);
                    currentClient.SetInvestment(4,6, 3);
                    break;
                case 6:
                    persons.Add(new Person(Person.Occupation.Stakeholder));
                    persons.Add(new Person(Person.Occupation.Programmer));
                    persons.Add(new Person(Person.Occupation.ExperienceExpert));
                    persons.Add(new Person(Person.Occupation.Tester));
                    persons.Add(new Person(Person.Occupation.PSO));
                    persons.Add(new Person(Person.Occupation.SME));
                    currentClient = new Client("Automata Systems", "A robotics and artificial intelligence company that produces autonomous machines to help with everyday tasks." +
                    " As they chose to create a robotic device that detects different colours more efficiently, but they can't afford many resources on the project, since they currently use most on them on other projects." +
                    " They do, however, told you that almost all employees consent to work on the project.", 50, 70, 50, persons, 2);
                    currentClient.SetInvestment(5, 6, 4);
                    break;
                case 7:
                    persons.Add(new Person(Person.Occupation.Programmer));
                    persons.Add(new Person(Person.Occupation.VisualDesigner));
                    persons.Add(new Person(Person.Occupation.ExperienceExpert));
                    persons.Add(new Person(Person.Occupation.UXdesigner));
                    persons.Add(new Person(Person.Occupation.UXdesigner));
                    persons.Add(new Person(Person.Occupation.Tester));
                    persons.Add(new Person(Person.Occupation.Tester));
                    currentClient = new Client("Forge Entertainment", "An entertainment company that produces and distributes top-tier video games and virtual reality experiences." +
                    " As a business with currently limited resources, they asked you to help with the development of their new game that is to release on the market in the hope it will increase their success on the market", 20, 30, 65, persons, 2);
                    currentClient.SetInvestment(2, 7, 5);
                    break;
                case 8:
                    persons.Add(new Person(Person.Occupation.Programmer));
                    persons.Add(new Person(Person.Occupation.VisualDesigner));
                    persons.Add(new Person(Person.Occupation.ExperienceExpert));
                    persons.Add(new Person(Person.Occupation.UXdesigner));
                    persons.Add(new Person(Person.Occupation.Tester));
                    persons.Add(new Person(Person.Occupation.Tester));
                    persons.Add(new Person(Person.Occupation.Tester));
                    currentClient = new Client("NoduX", "A tech company focused on creating innovative, user-friendly web and mobile applications." +
                    " As a relatively recent and relatively succesful company, they desire to create a new application targeted towards people with disabilities.", 70, 40, 40, persons, 4);
                    currentClient.SetInvestment(3, 3, 8);
                    break;
                case 9:
                    persons.Add(new Person(Person.Occupation.Programmer));
                    persons.Add(new Person(Person.Occupation.Programmer));
                    persons.Add(new Person(Person.Occupation.Programmer));
                    persons.Add(new Person(Person.Occupation.ExperienceExpert));
                    persons.Add(new Person(Person.Occupation.Tester));
                    persons.Add(new Person(Person.Occupation.PSO));
                    persons.Add(new Person(Person.Occupation.SME));
                    currentClient = new Client("Outliers Technologies", "A technology company that specializes in creating innovative, user-friendly hardware and software solutions." +
                    " As they are a small company in a strained financial situation, they came to you with the idea to make a new product that will help them reach a new part of their target audience.", 80, 40, 80, persons, 2);
                    currentClient.SetInvestment(2, 1, 5);  
                    break;
                default:
                    break;
            }
            prevrnd = rnd;
            currentClient.SetPeople(persons);
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
