using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.EventSystems;
using UnityEditor.Experimental.GraphView;
using UnityEditor.SearchService;
using UnityEditor;


public class GameManager : MonoBehaviour
{
    //Changes the game
    public static bool isMobile = false;
    public static bool isInMenu = true;

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
        isInMenu = false;
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
        isInMenu = true;
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
        isInMenu = true;
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
                    currentClient = new Client("Helix Technologies", "What kind of serious game would Helix Technologies , a technology company focused on developing " +
                        "cutting-edge virtual reality and augmented reality gaming experiences, would ask you to build ? Helix Technologies could ask another company " +
                        "to build a game that focuses on teaching the user about the latest virtual reality and augmented reality technologies. The game needs to have " +
                        "a strong sense of purpose in every feature there is in it as to teach the user things from the fundamentals of VR and AR, to how to use the latest " +
                        "\technology available in the field.Because of this, Try to also take note of the experience of the user with the learning material. The company has, " +
                        "luckily offered you a pretty long timeframe to complete this project.Good luck!", 60, 20, 40, persons,5,80);
                    currentClient.SetInvestment(2, 6, 6);
                    break;
                case 1:
                    persons.Add(new Person(Person.Occupation.Programmer));
                    persons.Add(new Person(Person.Occupation.VisualDesigner));
                    persons.Add(new Person(Person.Occupation.ExperienceExpert));
                    persons.Add(new Person(Person.Occupation.UXdesigner));
                    persons.Add(new Person(Person.Occupation.Tester));
                    persons.Add(new Person(Person.Occupation.EndUser));
                    currentClient = new Client("Trove Games", "What kind of videogame would Trove games-A video game company specializing in multiplayer online role-playing games," +
                        " Would ask you to work on with them? Their third planned MMO game, of course! After receiving some feedback from other current projects, they want to make a" +
                        " game which has a much better experience than usual. Don't ignore the other aspects, though. After all, an MMO needs to be easily maintained and sustained by" +
                        " the company. They got quite a bit of resources and quite a bit of time to offer before they release it, Lucky for you!", 20, 40, 60, persons, 4, 60);
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
                    currentClient = new Client("MoonScape", "What kind of service would MoonScape ,a leading mobile gaming company offering a variety of exciting and " +
                        "innovative titles, would ask of you to build? A remote app to better train their future employees how to work on their custom, in-house engine," +
                        " of course. For this project, they wish for this app to be sustainable, easily adjustable so that they can easily update it alongside their engine." +
                        " The educational purpose of the app shouldn't be ignored either. You have a bit of extra time above the usual ammount for the project,but spend it wisely!", 50, 70, 20, persons, 4, 60);
                    currentClient.SetInvestment(1, 4, 7);
                    break;
                case 3:
                    persons.Add(new Person(Person.Occupation.Programmer));
                    persons.Add(new Person(Person.Occupation.Programmer));
                    persons.Add(new Person(Person.Occupation.Tester));
                    persons.Add(new Person(Person.Occupation.Tester));
                    persons.Add(new Person(Person.Occupation.PSO));
                    persons.Add(new Person(Person.Occupation.SME));
                    currentClient = new Client("HyperTek", "What kind of product would HyperTek , a technology company that specializes in developing cyber security" +
                        " solutions for businesses, would want to build through a collaboration with you? Well, you've agreed to work with them to develop a security " +
                        "system for small businesses that's affordable to broaden the available services provided by the company. Their requirements for it are that it " +
                        "can be updated to detect newer viruses to sustain it and to have an easy to use interface for an easy user experience. Average time for this project. Good luck!", 20, 70, 40, persons, 3, 50);
                    currentClient.SetInvestment(6,6, 6);
                    break;
                case 4:
                    persons.Add(new Person(Person.Occupation.Programmer));
                    persons.Add(new Person(Person.Occupation.VisualDesigner));
                    persons.Add(new Person(Person.Occupation.ExperienceExpert));
                    persons.Add(new Person(Person.Occupation.UXdesigner));
                    persons.Add(new Person(Person.Occupation.Tester));
                    currentClient = new Client("DigiPlay", "What would Digiplay, an independent video game developer and publisher that develops original titles for " +
                        "multiple platforms, Would need to develop alongside you? They had an idea for a project they wanted to be done, but need your help to make it. " +
                        "It needs to offer a strong experience to leave a lasting impression, as this game could be the game that puts them in the spotlight. Be careful " +
                        "to not forget other aspects of the game, however. As a relatively new company, they don't have that much time to spare to get the project off the " +
                        "ground. But can you use that time to the fullest?", 10, 10, 70, persons, 2, 40);
                    currentClient.SetInvestment(8, 4, 5);
                    break;
                case 5:
                    persons.Add(new Person(Person.Occupation.Stakeholder));
                    persons.Add(new Person(Person.Occupation.Programmer));
                    persons.Add(new Person(Person.Occupation.ExperienceExpert));
                    persons.Add(new Person(Person.Occupation.Tester));
                    persons.Add(new Person(Person.Occupation.PSO));
                    persons.Add(new Person(Person.Occupation.SME));
                    currentClient = new Client("Stratos Systems", "What kind of software would Stratos Systems, a technology company specializing in the development of " +
                        "innovative cloud-based software solutions, ask you to develop for them? They agreed to colaborate with you on a program meant to securely save " +
                        "the data from a client's social media page on a cloud server and to make the app compatible with the antivirus provided by the Stratos Systems. " +
                        "This is the purpose of the project, but it's necessary to also keep an eye on how to make the software easier to sustain through updates. " +
                        "Average time for this project. Good luck!", 70, 40, 20, persons, 3, 50);
                    currentClient.SetInvestment(4,6, 3);
                    break;
                case 6:
                    persons.Add(new Person(Person.Occupation.Stakeholder));
                    persons.Add(new Person(Person.Occupation.Programmer));
                    persons.Add(new Person(Person.Occupation.ExperienceExpert));
                    persons.Add(new Person(Person.Occupation.Tester));
                    persons.Add(new Person(Person.Occupation.PSO));
                    persons.Add(new Person(Person.Occupation.SME));
                    currentClient = new Client("Automata Systems", "What kind of robot would Automata Systems , a robotics and artificial intelligence company that produces " +
                        "autonomous machines to help with everyday tasks, ask you to make for them? They asked you to develop a small and compact color sensor that can detect " +
                        "both bright and dark colors from a camera and can do different actions based on the color when it finds it. The small robot needs to have all its aspects" +
                        " have a purpose due to how small it is, and a lot of it at that. You have a decent ammount of time available, but you're short on resources. " +
                        "Can you make it work?", 80, 10, 10, persons, 2, 60);
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
                    currentClient = new Client("Forge Entertainment", "What kind of game would Forge Entertainment, an entertainment company that produces and distributes top-tier " +
                        "video games and virtual reality experiences, would ask your company to develop for them?As a business with currently limited resources, they asked you to help " +
                        "with the development of their new game that is to release on the market soon with the purpose to increase their success on the market. They don't want to go " +
                        "cheap on the player's gameplay experience, however. You established a pretty reasonable timeframe with them for the project. Best of luck on developing!", 60, 20, 40, persons, 2, 60);
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
                    currentClient = new Client("NoduX", "What kind of product would NoduX, a tech company focused on creating innovative, user-friendly web and mobile applications, " +
                        "would they ask you to develop with them as part of a partnership? Well,they desire to create a new application targeted towards non-verbal indivduals to help " +
                        "them communicate with other people either in person or online in real time. It's important for the user experience with the product is to be noticeably good and " +
                        "to make the purpose and use of the product to be not only apparent, but also palpable in its effects. Average time for this project. Good luck!", 60, 10, 60, persons, 4, 50);
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
                    currentClient = new Client("Outliers Technologies", "What kind of project would Outliers Technologies , a technology company that specializes in creating innovative, " +
                        "user-friendly hardware and software solutions, would like to develop with your help? They decided that developing an app which allows its users to easily prepare " +
                        "and create their own portfolios for job applications. The company has stated that this app will have to be more sustainable to match with changing trends in the work " +
                        "environments of the user. Unfortunately, there's not that much time or resources that can be accorded due to planned changes. You still persisted with the project and " +
                        "now you're about to start development. Are you still up to the task?", 10, 70, 10, persons, 2, 40);
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

    public static float[] ExtraPointReveal()
    {
        float[] diff = new float[3];
        diff[0] = sustainability - currentClient.reqSustainability;
        diff[1] = experience - currentClient.reqExperience;
        diff[2] = purpose - currentClient.reqPurpose;
        return diff;
    } 
}
