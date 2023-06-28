using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class MDesMenuScript : MonoBehaviour,IPointerClickHandler
{
    [Header("Sprites")]
    public Sprite Discover;
    public Sprite Develop;
    public Sprite Deliver;
    public Sprite Upkeep;
    Sprite _Base;
    AudioSource _AudioSource;

    [Header("Description pieces")]
    public Image HexagonType;
    public Text Name;
    public Text Description;
    public GameObject StrengthPointsParent;
    public GameObject AtributePrefab;
    void Start()
    {
        GameManager.descriptionMenu = this;
        _Base = HexagonType.sprite;
        _AudioSource = GetComponent<AudioSource>();

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        SetInformation();
    }
    public void SetInformation()
    {
        _AudioSource.volume = GameManager.sfxVolume;
        _AudioSource.Play();
        if (GameManager.InfoPiece)
            GameManager.WasCheckedForInfo = true;
        foreach (Transform child in StrengthPointsParent.transform)
        {
            Destroy(child.gameObject);
        }
        if(GameManager.InfoPiece)
        GameManager.InfoPiece.DeselectHexagon();
        Name.text = GameManager.InfoPiece.Name();
        switch (GameManager.InfoPiece.Type)
        {
            case HexagonPiece.type.discover:
                HexagonType.sprite = Discover;
                switch (GameManager.InfoPiece.Name())
                {
                    case "KPI":
                        Description.text = "Key Performance Indicators. They allow you to get clear metrics that indicate where you want the project to be and how to get there.";
                        break;
                    case "Emphatise":
                        Description.text = "Take into account the perspective of the target audience when designing the product.";
                        break;
                    case "Risk Assesment":
                        Description.text = "Asess the risk factors that may arise during development.";
                        break;
                    case "Insight":
                        Description.text = "Get insight into the target audience's needs and desires.";
                        break;
                    case "Requirements":
                        Description.text = "Form design requirements to get a clear idea of what the product needs to contain.";
                        break;
                    case "Research":
                        Description.text = "Further research into the topics related to the product.";
                        break;
                    case "Analyse":
                        Description.text = "Analyse data obtained through research.";
                        break;
                    case "Initiation":
                        Description.text = "Thoroughly prepare new workers on the project for what they'll work on.";
                        break;
                    case "Awareness":
                        Description.text = "Take more effort to make yourself aware of the intricacies of the project.";
                        break;
                    case "Design Space Analysis":
                        Description.text = "Use Questions, Options, and Criteria (QOC) to reflect on your design choices and identify issues.";
                        break;
                    case "Player Investigation":
                        Description.text = "Investigate what players would want.";
                        break;
                    case "Journey Investigation":
                        Description.text = "Find out what would be the ideal journey the customer would go through to get to know the company.";
                        break;
                    case "Game Storm":
                        Description.text = "Brainstorm gameplay ideas.";
                        break;
                    case "SME Investigation":
                        Description.text = "Subject Matter Expert Investigation. Investigate if the subject matter of the project is appropriate for the target audience.";
                        break;
                    case "Game Concept":
                        Description.text = "Create a clearly defined concept for the product.";
                        break;
                    case "Identify Game Loop":
                        Description.text = "Clearly define the gameplay loop which the user will follow.";
                        break;
                    case "Feature Roadmap":
                        Description.text = "Create a roadmap of what features would be made when.";
                        break;
                    default: break;
                }
                break;
            case HexagonPiece.type.develop:
                HexagonType.sprite = Develop;
                switch (GameManager.InfoPiece.Name())
                {
                    case "Action":
                        Description.text = "Take action to continue development";
                        break;
                    case "Minimum Viable Product":
                        Description.text = "Make a product that would technically be usable by the target audience despite the lack of polish for testing.";
                        break;
                    case "Minimum Usable Product":
                        Description.text = "Make a product that has several functional features for testing.";
                        break;
                    case "Solution":
                        Description.text = "Find how to fix the customer's problems that the product is meant to fix.";
                        break;
                    case "Prototyping":
                        Description.text = "Develop a prototype for testing.";
                        break;
                    case "Proof of Concept":
                        Description.text = "Create a prototype which has the purpose of proving that the concept is viable.";
                        break;
                    case "Execution":
                        Description.text = "Review how features of your product are implemented.";
                        break;
                    case "Fabrication":
                        Description.text = "Use material that is meant to present the finished product. This isn't substitute for the real thing.";
                        break;
                    case "Concept":
                        Description.text = "Have a concept set and recorded somewhere.";
                        break;
                    case "Playtest":
                        Description.text = "Test out the prototype for the game";
                        break;
                    case "Iterate":
                        Description.text = "Create a better iteration for the game.";
                        break;
                    case "Elaboration":
                        Description.text = "Ekaborate on the process development ";
                        break;
                    case "Ideation":
                        Description.text = "Create several ideas for the project and write them down somewhere";
                        break;
                    case "Design":
                        Description.text = "Review your product's design process.";
                        break;
                    case "Define":
                        Description.text = "Take time to define specific elements of the process.";
                        break;
                    case "Design Documentation":
                        Description.text = "Document design ideas and requirements.";
                        break;
                    case "Wireframes":
                        Description.text = "Prototypes which have very basic functionality. Used to design the user interface.";
                        break;
                    case "Paper Prototype":
                        Description.text = "Prototype made out of components that don't reflect the quality of the finished product, but the idea and functionality of it.";
                        break;
                    case "Click-thru":
                        Description.text = "Prototype where the user can only click through screens. Used to test out UI navigation .";
                        break;
                    case "Feature Roadmap":
                        Description.text = "Create a roadmap of what features would be made when.";
                        break;
                    case "Design Elaborations":
                        Description.text = "Elaborate on the design ideas.";
                        break;
                    default: break;
                }
                break;
            case HexagonPiece.type.deliver:
                HexagonType.sprite = Deliver;
                switch (GameManager.InfoPiece.Name())
                {
                    case "Playtest":
                        Description.text = "Test out the prototype for the game";
                        break;
                    case "Validate":
                        Description.text = "Find results which further validate design decisions.";
                        break;
                    case "Launch":
                        Description.text = "Launch the product";
                        break;
                    case "Design Tuning":
                        Description.text = "Adjust the design based of feedback.";
                        break;
                    case "Implementation":
                        Description.text = "Add feedback to the project.";
                        break;
                    case "Closing":
                        Description.text = "Place emphasis on the finishing touches of the prototype before the end of the project.";
                        break;
                    case "Business Model":
                        Description.text = "Prepare a business model canvas to visualize and assess your business idea or concept.";
                        break;
                    case "Production":
                        Description.text = "Begin production of the product in full.";
                        break;
                    case "User Stories":
                        Description.text = "Use stories the game does to inform the design decision.";
                        break;
                    case "Minimum Viable Product":
                        Description.text = "Make a product that would technically be usable by the target audience despite the lack of polish for testing.";
                        break;
                    case "Minimum Usable Product":
                        Description.text = "Make a product that has several functional features for testing.";
                        break;
                    case "Feature Roadmap":
                        Description.text = "Create a roadmap of what features would be made when.";
                        break;
                    default: break;
                }
                break;
            case HexagonPiece.type.upkeep:
                HexagonType.sprite = Upkeep;
                switch (GameManager.InfoPiece.Name())
                {
                    case "Monetise":
                        Description.text = "Add methods to get more funds from the product in the long run.";
                        break;
                    case "Upscale":
                        Description.text = "Increase the scope of the product.";
                        break;
                    case "Support":
                        Description.text = "Increase support and focus on the project.";
                        break;
                    case "Updates":
                        Description.text = "Update the product based on feedback.";
                        break;
                    case "Maintenance":
                        Description.text = "Keep the base systems of the project updated to keep functionality.";
                        break;
                    case "Feature Roadmap":
                        Description.text = "Create a roadmap of what features would be made when.";
                        break;
                    default: break;
                }
                break;
            default:
                break;
        }
        if (GameManager.InfoPiece.Experience > 0)
        {
            GameObject e = Instantiate(AtributePrefab);
            Image ie = e.GetComponent<Image>();
            Text te = e.GetComponentInChildren<Text>();
            te.text = "Experience";
            if (GameManager.InfoPiece.Experience > 2)
            {
                ie.color = Color.green;
            }
            else
            {
                ie.color = Color.yellow;
            }
            e.transform.localScale = new Vector3(1, 1, 1);
            e.transform.SetParent(StrengthPointsParent.transform, false);
        }
        if (GameManager.InfoPiece.Sustainability > 0)
        {
            GameObject s = Instantiate(AtributePrefab);
            Image ies = s.GetComponent<Image>();
            Text ts = s.GetComponentInChildren<Text>();
            ts.text = "Sustainability";
            if (GameManager.InfoPiece.Sustainability > 2)
            {
                ies.color = Color.green;
            }
            else
            {
                ies.color = Color.yellow;
            }
            s.transform.localScale = new Vector3(1, 1, 1);
            s.transform.SetParent(StrengthPointsParent.transform, false);
        }
        if (GameManager.InfoPiece.Purpose > 0)
        {
            GameObject p = Instantiate(AtributePrefab);
            Image ip = p.GetComponent<Image>();
            Text tp = p.GetComponentInChildren<Text>();
            tp.text = "Purpose";
            if (GameManager.InfoPiece.Purpose > 2)
            {
                ip.color = Color.green;
            }
            else
            {
                ip.color = Color.yellow;
            }
            p.transform.localScale = new Vector3(1, 1, 1);
            p.transform.SetParent(StrengthPointsParent.transform, false);
        }

    }

    public void Clear()
    {
        foreach (Transform child in StrengthPointsParent.transform)
        {
            Destroy(child.gameObject);
        }
        HexagonType.sprite = _Base;
        Description.text = "";
        Name.text = "";
    }
}
