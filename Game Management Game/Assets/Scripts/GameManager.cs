using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.EventSystems;


public class GameManager : MonoBehaviour
{
    //Changes the game
    public static bool isMobile = false;


    public static GameObject mobileJoystick;
    static int nrOfLives = 3;
    public static bool _Win = false;
    public static float purpose = 0;
    public static float sustainability = 0;
    public static float experience = 0;
    public static int difficulty = 1;

    public static bool sawTutorial=false;

    public static ClusterManager mainClusterM = new ClusterManager();

    public static HexagonPiece selectedPiece = new HexagonPiece();

    public static HexagonPiece InfoPiece = new HexagonPiece();


    // Menu navigation 
    static Canvas _mainMenu;
    static Canvas _tutorialMenu;
    static Canvas _settingsMenu;
    static Canvas _gameMenu;

    //Settings navigation
    public static float sfxVolume = .5f;
    public static float bkgVolume = .5f;



    public static ResourceObject currentRes;
    public static PersonObject currentPer;

    public static RPiece currentPiece;

    public static MDesMenuScript descriptionMenu;

    public static Client currentClient;

    public static int availableChances;

    public static float currentTimeLeft;

    public static AudioSource buttonAudio;
    public static AudioSource bkgAudio;

    static int _prevrnd = 10;

    //Tutorial values
    public static bool WasPlaced = false;
    public static bool WasCheckedForInfo = false;
    public static bool WasMenuOpened = false;

    // Start is called before the first frame update
    private void Awake()
    {
        if (isMobile)
        {
            Screen.orientation = ScreenOrientation.LandscapeLeft;
        }
        AudioSource[] allaudios=GameObject.FindObjectsOfType<AudioSource>();
        foreach(AudioSource audio in allaudios)
        {
            if (audio.gameObject.name == "ButtonAudio")
            {
                buttonAudio = audio;
            }
            if (audio.gameObject.name == "BKGAudio")
            {
                bkgAudio = audio;
            }
        }
        GetNewClient();
    }
    void Start()
    {
        Canvas[] canvases = GameObject.FindObjectsOfType<Canvas>(true);
        foreach (Canvas c in canvases)
        {
            if (c.name == "MainMenu")
                _mainMenu = c;
            if (c.name == "GameMenu")
                _gameMenu = c;
            if (c.name == "TutorialMenu")
                _tutorialMenu = c;
            if (c.name == "SettingsMenu")
                _settingsMenu = c;
        }
        
        
    }
    // Update is called once per frame
    void Update()
    {
        if (experience >= 100)
        { experience = 100; }
        else if (experience <= 0)
        { experience = 0; }

        if (purpose >= 100)
        { purpose = 100; }
        else if (purpose <= 0)
        { purpose = 0; }

        if (sustainability >= 100)
        { sustainability = 100; }
        else if (sustainability <= 0)
        { sustainability = 0; }

        
    }

    public static void StartGame()
    {
        buttonAudio.Play();
        if (!sawTutorial)
        {
            _tutorialMenu.gameObject.SetActive(true);
            _settingsMenu.gameObject.SetActive(false);
            _gameMenu.gameObject.SetActive(false);
            _mainMenu.gameObject.SetActive(false);
            sawTutorial = true;
            return;
        }
        else
        {
            
            _tutorialMenu.gameObject.SetActive(false);
            _settingsMenu.gameObject.SetActive(false);
            _gameMenu.gameObject.SetActive(true);
            _mainMenu.gameObject.SetActive(false);
            if (!isMobile)
            {
                mobileJoystick.GetComponent<Image>().enabled = false;
                mobileJoystick.SetActive(false);
            }
            return;
        }
    }

    public static void OpenSettings()
    {
        Text gBText = _settingsMenu.transform.Find("Start").GetComponentInChildren<Text>();
        if(_mainMenu.gameObject.activeSelf)
        {
            gBText.text = "Start";
        }
        else if (_gameMenu.gameObject.activeSelf)
        {
            gBText.text = "Back To Game";
        }
        if(!isMobile)
        {
            _settingsMenu.transform.Find("Visuals").Find("Luminosity").gameObject.SetActive(false);
        }
        buttonAudio.Play();
        _mainMenu.gameObject.SetActive(false);
        _tutorialMenu.gameObject.SetActive(false);
        _settingsMenu.gameObject.SetActive(true);
        _gameMenu.gameObject.SetActive(false);
    }

    public static void OpenTutorial()
    {
        buttonAudio.Play();
        _mainMenu.gameObject.SetActive(false);
        _tutorialMenu.gameObject.SetActive(true);
        _settingsMenu.gameObject.SetActive(false);
        _gameMenu.gameObject.SetActive(false);
        sawTutorial = true;
    }
    public static void BacktoMenu()
    {
        buttonAudio.Play();
        TryAgain();
        _mainMenu.gameObject.SetActive(true);
        _tutorialMenu.gameObject.SetActive(false);
        _settingsMenu.gameObject.SetActive(false);
        _gameMenu.gameObject.SetActive(false);
    }

    public Vector3 GetMousePos()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
    public static void GetNewClient()
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
        if (rnd!=_prevrnd) 
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
                    " They've been operating for 5 years and have agreed with you to find a way to create a way to make a new VR game they've been planning to make for several years.", 60, 50, 60, persons,5,80);
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
                    " This company has gotten in contact with you in order to make their third MMO game, which they just hired a new team's worth of people for it and planned to have it be a side game compared to the other two titles they already have.", 40, 30, 50, persons, 4, 80);
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
                    " However, they don't have a plan to how they're going to re-vamp the project, so they wish to start from scratch.", 60, 30, 70, persons, 4, 80);
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
                    " You've agreed to work with them to develop a security system for small businesses that's affordable to broaden the available services provided by the company.", 60, 40, 40, persons, 3, 80);
                    currentClient.SetInvestment(6,6, 6);
                    break;
                case 4:
                    persons.Add(new Person(Person.Occupation.Programmer));
                    persons.Add(new Person(Person.Occupation.VisualDesigner));
                    persons.Add(new Person(Person.Occupation.ExperienceExpert));
                    persons.Add(new Person(Person.Occupation.UXdesigner));
                    persons.Add(new Person(Person.Occupation.Tester));
                    currentClient = new Client("DigiPlay", "An independent video game developer and publisher that develops original titles for multiple platforms." +
                    " As a small company, they want to try to make a much more fleshed out game, but they have a limited ammount of resources and persons that they have together. ", 30, 50, 60, persons, 2, 80);
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
                    " Through your collaboration with them, you now have to make a secure software meant to save the data from a client's social media page.", 70, 80, 50, persons, 3, 80);
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
                    " They do, however, told you that almost all employees consent to work on the project.", 50, 70, 50, persons, 2, 80);
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
                    " As a business with currently limited resources, they asked you to help with the development of their new game that is to release on the market in the hope it will increase their success on the market", 20, 30, 65, persons, 2, 80);
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
                    " As a relatively recent and relatively succesful company, they desire to create a new application targeted towards people with disabilities.", 70, 40, 40, persons, 4, 80);
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
                    " As they are a small company in a strained financial situation, they came to you with the idea to make a new product that will help them reach a new part of their target audience.", 80, 40, 80, persons, 2, 80);
                    currentClient.SetInvestment(2, 1, 5);  
                    break;
                default:
                    break;
            }
            _prevrnd = rnd;
            currentClient.SetPeople(persons);
            currentTimeLeft = currentClient.GetRemainingTime();
        }
        
    }
    public static void CheckIfWon(GameObject g)
    {
        if(currentClient != null)
        {
            if(nrOfLives > 0)
            {
                if (experience >= currentClient.reqExperience && sustainability >= currentClient.reqSustainability && purpose >= currentClient.reqPurpose && currentClient.GetRemainingTime()>0)
                {
                    _Win = true;
                    g.SetActive(true);
                }
                else
                {
                    nrOfLives--;
                }
            }
            else
            {
                g.SetActive(true);
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

        //find all UI hexagons and then re-activate the inactive ones.
        UIHexagon[] HexagonsToActivate= GameObject.FindObjectsOfType<UIHexagon>(true).Where(sr => !sr.gameObject.activeInHierarchy).ToArray(); 

        foreach(UIHexagon Hexagon in HexagonsToActivate)
        {
            Hexagon.gameObject.SetActive(true);
        }

        // Find alll clusters and delete them
        ClusterManager[] Clusters = GameObject.FindObjectsOfType<ClusterManager>();

        foreach (ClusterManager cl in Clusters)
        {
            foreach(Transform child in cl.transform)
            {
                child.GetComponent<HexagonPiece>().DestroyHexagon();
            }
        }
        //get new client 
        GetNewClient();
        //reset values to zero
        mainClusterM = new ClusterManager();
        selectedPiece = new HexagonPiece();

        currentPer = null;
        currentRes = null;
    }
    public void Borrow(GameObject prompt)
    {
        prompt.SetActive(true);
        currentClient.LetBorrow();
        if (currentClient.GetChances() > 0)
        {
            prompt.transform.Find("Message").GetComponent<Text>().text = "The company has offered you more resources. Their expectations for the project also increased.";
        }
        else
        {
            prompt.transform.Find("Message").GetComponent<Text>().text = "The company can't offer you more resources. It's best to work with what you have.";
        }
    }
    public static void JustPlayPressAudio()
    {
        buttonAudio.volume = sfxVolume;
        buttonAudio.Play();
    }
    public static bool IsPointerOverUIElement()
    {
        return IsPointerOverUIElement(GetEventSystemRaycastResults());
    }
    public static void CheckIfClusterExists()
    {
        ClusterManager[] Clusters = GameObject.FindObjectsOfType<ClusterManager>();
        bool itExists = false;

        foreach (ClusterManager cl in Clusters)
        {
            if (cl.Equals(GameManager.mainClusterM))
            {
                cl.SetAsMainCluster();
                itExists = true;
            }
        }
        if (!itExists)
        {
            GameManager.mainClusterM = new ClusterManager();
            GameManager.purpose = 0;
            GameManager.sustainability = 0;
            GameManager.experience = 0;
            GameManager.currentTimeLeft = GameManager.currentClient.GetRemainingTime();
        }
    }
    //Returns 'true' if we touched or hovering on Unity UI element.
    private static bool IsPointerOverUIElement(List<RaycastResult> eventSystemRaysastResults)
    {
        for (int index = 0; index < eventSystemRaysastResults.Count; index++)
        {
            RaycastResult curRaysastResult = eventSystemRaysastResults[index];
            if (curRaysastResult.gameObject.layer == LayerMask.NameToLayer("UI"))
                return true;
        }
        return false;
    }
    //Gets all event system raycast results of current mouse or touch position.
    static List<RaycastResult> GetEventSystemRaycastResults()
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;
        List<RaycastResult> raysastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, raysastResults);
        return raysastResults;
    }


    ///Tutorial functions
    
    public static void SetSFXVolume(Slider sl)
    {
        sfxVolume = sl.value;
    }
    public static void SetBKGVolume(Slider bk)
    {
        bkgVolume = bk.value;
    }

    public static void Zoom(Slider zo)
    {
        Camera.main.orthographicSize = zo.value;
    }

    public static void Brightness(Slider br)
    {
        Screen.brightness = br.value;
    }
    public static void SetFilter(Dropdown d)
    {
        int index=d.value;
        switch (index)
        {
            case 0:
                Camera.main.GetComponent<ColorBlindFilter>().mode = ColorBlindMode.Normal;
                break;
            case 1:
                Camera.main.GetComponent<ColorBlindFilter>().mode = ColorBlindMode.Protanopia;
                break;
            case 2:
                Camera.main.GetComponent<ColorBlindFilter>().mode = ColorBlindMode.Protanomaly;
                break;
            case 3:
                Camera.main.GetComponent<ColorBlindFilter>().mode = ColorBlindMode.Deuteranopia;
                break;
            case 4:
                Camera.main.GetComponent<ColorBlindFilter>().mode = ColorBlindMode.Deuteranomaly;
                break;
            case 5:
                Camera.main.GetComponent<ColorBlindFilter>().mode = ColorBlindMode.Tritanopia;
                break;
            case 6:
                Camera.main.GetComponent<ColorBlindFilter>().mode = ColorBlindMode.Tritanomaly;
                break;
            case 7:
                Camera.main.GetComponent<ColorBlindFilter>().mode = ColorBlindMode.Achromatopsia;
                break;
            case 8:
                Camera.main.GetComponent<ColorBlindFilter>().mode = ColorBlindMode.Achromatomaly;
                break;
            default:
                Camera.main.GetComponent<ColorBlindFilter>().mode = ColorBlindMode.Normal;
                break;

        }
        
    }
}
