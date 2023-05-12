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

    [Header("Description pieces")]
    public Image HexagonType;
    public Text Name;
    public Text Description;
    public GameObject StrengthPointsParent;

    public GameObject AtributePrefab;

    public void OnPointerClick(PointerEventData eventData)
    {
        SetInformation();
    }
    void SetInformation()
    {
        foreach (Transform child in StrengthPointsParent.transform)
        {
            Destroy(child.gameObject);
        }
        //GameManager.InfoPiece.DeselectHexagon();
        Name.text = GameManager.InfoPiece.Name();
        switch (GameManager.InfoPiece.Type)
        {
            case HexagonPiece.type.discover:
                HexagonType.sprite = Discover;
                switch (GameManager.InfoPiece.Name())
                {
                    case "KPI":
                        Description.text = "Insert description here";
                        break;
                    case "Emphatise":
                        Description.text = "Insert description here";
                        break;
                    case "Risk Assesment":
                        Description.text = "Insert description here";
                        break;
                    case "Insight":
                        Description.text = "Insert description here";
                        break;
                    case "Requirements":
                        Description.text = "Insert description here";
                        break;
                    case "Research":
                        Description.text = "Insert description here";
                        break;
                    case "Analyse":
                        Description.text = "Insert description here";
                        break;
                    case "Initiation":
                        Description.text = "Insert description here";
                        break;
                    case "Awareness":
                        Description.text = "Insert description here";
                        break;
                    case "Design Space Analysis":
                        Description.text = "Insert description here";
                        break;
                    case "Player Investigation":
                        Description.text = "Insert description here";
                        break;
                    case "Journey Investigation":
                        Description.text = "Insert description here";
                        break;
                    case "Game Storm":
                        Description.text = "Insert description here";
                        break;
                    case "Subject Matter Expert Investigation":
                        Description.text = "Insert description here";
                        break;
                    case "Game Concept":
                        Description.text = "Insert description here";
                        break;
                    case "Identify Game Loop":
                        Description.text = "Insert description here";
                        break;
                    case "Feature Roadmap":
                        Description.text = "Insert description here";
                        break;
                    default: break;
                }
                break;
            case HexagonPiece.type.develop:
                HexagonType.sprite = Develop;
                switch (GameManager.InfoPiece.Name())
                {
                    case "Action":
                        Description.text = "Insert description here";
                        break;
                    case "Minimum Viable Product":
                        Description.text = "Insert description here";
                        break;
                    case "Minimum Usable Product":
                        Description.text = "Insert description here";
                        break;
                    case "Solution":
                        Description.text = "Insert description here";
                        break;
                    case "Prototyping":
                        Description.text = "Insert description here";
                        break;
                    case "Proof of Concept":
                        Description.text = "Insert description here";
                        break;
                    case "Execution":
                        Description.text = "Insert description here";
                        break;
                    case "Fabrication":
                        Description.text = "Insert description here";
                        break;
                    case "Concept":
                        Description.text = "Insert description here";
                        break;
                    case "Playtest":
                        Description.text = "Insert description here";
                        break;
                    case "Iterate":
                        Description.text = "Insert description here";
                        break;
                    case "Elaboration":
                        Description.text = "Insert description here";
                        break;
                    case "Ideation":
                        Description.text = "Insert description here";
                        break;
                    case "Design":
                        Description.text = "Insert description here";
                        break;
                    case "Define":
                        Description.text = "Insert description here";
                        break;
                    case "Design Documentation":
                        Description.text = "Insert description here";
                        break;
                    case "Wireframes":
                        Description.text = "Insert description here";
                        break;
                    case "Paper Prototype":
                        Description.text = "Insert description here";
                        break;
                    case "Click-thru":
                        Description.text = "Insert description here";
                        break;
                    case "Feature Roadmap":
                        Description.text = "Insert description here";
                        break;
                    case "Design Elaborations":
                        Description.text = "Insert description here";
                        break;
                    default: break;
                }
                break;
            case HexagonPiece.type.deliver:
                HexagonType.sprite = Deliver;
                switch (GameManager.InfoPiece.Name())
                {
                    case "Playtest":
                        Description.text = "Insert description here";
                        break;
                    case "Validate":
                        Description.text = "Insert description here";
                        break;
                    case "Launch":
                        Description.text = "Insert description here";
                        break;
                    case "Design Tuning":
                        Description.text = "Insert description here";
                        break;
                    case "Implementation":
                        Description.text = "Insert description here";
                        break;
                    case "Closing":
                        Description.text = "Insert description here";
                        break;
                    case "Business Model":
                        Description.text = "Insert description here";
                        break;
                    case "Production":
                        Description.text = "Insert description here";
                        break;
                    case "User Stories":
                        Description.text = "Insert description here";
                        break;
                    case "Minimum Usable Product":
                        Description.text = "Insert description here";
                        break;
                    case "Minimum Viable Product":
                        Description.text = "Insert description here";
                        break;
                    case "Feature Roadmap":
                        Description.text = "Insert description here";
                        break;
                    default: break;
                }
                break;
            case HexagonPiece.type.upkeep:
                HexagonType.sprite = Upkeep;
                switch (GameManager.InfoPiece.Name())
                {
                    case "Monetise":
                        Description.text = "Insert description here";
                        break;
                    case "Upscale":
                        Description.text = "Insert description here";
                        break;
                    case "Support":
                        Description.text = "Insert description here";
                        break;
                    case "Updates":
                        Description.text = "Insert description here";
                        break;
                    case "Maintenance":
                        Description.text = "Insert description here";
                        break;
                    case "Feature Roadmap":
                        Description.text = "Insert description here";
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
            if (GameManager.InfoPiece.Experience > 1)
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
            if (GameManager.InfoPiece.Sustainability > 1)
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
            if (GameManager.InfoPiece.Purpose > 1)
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
}
