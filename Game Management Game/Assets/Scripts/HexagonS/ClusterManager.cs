using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ClusterManager : MonoBehaviour
{
    //avg cluster valures
    public float _avgPurpose = 0;
    public float _avgSustainability = 0;
    public float _avgExperience = 0;
    List<HexagonPiece> _HexagonPieces = new List<HexagonPiece>();
    int prevChildCount = 0;
    private void Start()
    {
        _HexagonPieces = new List<HexagonPiece>();
        this.gameObject.tag = "Cluster";
    }

    void Update()
    {
        if (this._avgExperience>=GameManager.experience && this._avgPurpose>= GameManager.purpose && this._avgSustainability>=GameManager.sustainability)
        {
            GameManager.mainClusterM = this;
            GameManager.purpose = _avgPurpose;
            GameManager.sustainability = _avgSustainability; 
            GameManager.experience = _avgExperience;
            
        }
        if (this.transform.childCount == 0)
        {
            if (this==GameManager.mainClusterM)
            {
                GameManager.mainClusterM = new ClusterManager();
                GameManager.purpose = 0;
                GameManager.sustainability =0;
                GameManager.experience = 0;
            }
                Destroy(this.gameObject);
        }
            
        if (prevChildCount != transform.childCount)
        {
            RefreshValues();
            prevChildCount = transform.childCount;
        }
    }

    public void RefreshValues()
    {
        float exp = 0;
        float pur = 0;
        float sust = 0;
        

        foreach (Transform obj in transform)
        { 
            //heres where the calculations for how much each hexagon adds. This changes with person and resource
            if (obj.TryGetComponent<HexagonPiece>(out HexagonPiece piece))
            {
                exp += piece.Experience;
                pur += piece.Purpose;
                sust += piece.Sustainability;
                switch (piece.GetPersonOccupation())
                {
                    case "Stakeholder":
                        if (piece.Type == HexagonPiece.type.discover || piece.Name() == "Design Elaborations")
                        {
                            sust += 2;
                            pur += 3;
                        }
                        break;
                    case "Sponsor":
                        if (piece.Type == HexagonPiece.type.upkeep || piece.Name() == "Game Concept")
                        {
                            sust += 3;
                        }
                        break;
                    case "EndUser":
                        if (piece.Name() == "Playtest" || piece.Name() == "Paper Prototype" || piece.Name() == "User Stories")
                        {
                            exp += 3;
                        }
                        break;
                    case "UXdesigner":
                        if (piece.Name() == "Fine-Tuning" || piece.Name() == "Execution" || piece.Name() == "Updates" || piece.Name() == "Paper Prototype")
                        {
                            exp += 3;
                        }
                        break;
                    case "ProjectManager":
                        sust += 3;
                        exp += 1;

                        break;
                    case "PSO":
                        if (piece.Type == HexagonPiece.type.upkeep)
                        {
                            sust += 3;
                            exp += 3;
                        }
                        break;
                    case "SME":
                        if (piece.Name() == "Solution" || piece.Name() == "Concept" || piece.Name() == "Execution" || piece.Name() == "Proof of Concept")
                        {
                            sust += 2;
                            exp += 2;
                        }
                        else if (piece.Name() == "Subject Matter Expert Investigation")
                        {
                            sust += 4;
                            pur += 4;
                            exp += 4;
                        }
                        break;
                    case "ProductOwner":
                        if (piece.Type == HexagonPiece.type.discover)
                        {
                            pur += 3;
                        }
                        break;
                    case "Programmer":
                        if (piece.Type == HexagonPiece.type.develop)
                        {
                            sust += 2;
                            pur += 3;
                        }
                        break;

                    case "ExperienceExpert":
                        if (piece.Name() == "Fine-Tuning" || piece.Name() == "Executiion" || piece.Name() == "Updates" || piece.Name() == "Paper Prototype" || piece.Name() == "Wireframes")
                        {
                            exp += 3;
                        }
                        break;
                    case "VisualDesigner":
                        if (piece.Name() == "Design" || piece.Name() == "Prototype" || piece.Name() == "Proof of Concept" || piece.Name() == "Paper Prototype")
                        {
                            pur += 3;
                            exp += 2;
                        }
                        break;
                    case "Tester":
                        sust += 1;
                        pur += 1;
                        exp += 1;
                        break;
                    default:
                        Debug.Log("Ok, so it doesn't give errors");
                        break;
                }
                switch (piece.GetResourceValue())
                {
                    case 1:
                        exp *= 1.2f;
                        pur *= 1.2f;
                        sust *= 1.2f;
                        break;
                    case 2:
                        pur *= 1.4f;
                        pur *= 1.4f;
                        sust *= 1.4f;
                        break;
                    case 3:
                        sust *= 1.6f;
                        pur *= 1.6f;
                        sust *= 1.6f;
                        break;
                    default:
                        Debug.Log("Which is good, so I can add the modifiers in these two switch statements");
                        break;
                }
            }
        }
        
        _avgExperience = exp;
        _avgPurpose = pur;
        _avgSustainability = sust;
        
        
    }
    public bool isInHere(HexagonPiece piece)
    {
        return _HexagonPieces.Contains(piece);
    }
    public void RemoveHexagon(HexagonPiece piece)
    {
        _avgExperience -= piece.Experience;
        _avgPurpose -= piece.Purpose;
        _avgSustainability -= piece.Sustainability;
    }
    public int NrOfHexagons()
    {
        return _HexagonPieces.Count;
    }
    public void MergeClusters(ClusterManager cl)
    {
        Transform newparent;
        if (cl.transform)
        {
            if (cl.transform.childCount >= this.transform.childCount)
                newparent = cl.transform;
            else
                newparent = transform;
            foreach (Transform obj in transform)
            {
                obj.SetParent(newparent);
                newparent.GetComponent<ClusterManager>().RefreshValues();
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        
        if(collision.TryGetComponent<HexagonPiece>(out HexagonPiece p))
        {
            if (p.transform.parent)
            {
                if (p.transform.parent.childCount >= 1)
                    this.MergeClusters(p.GetClusterManager());
            }
            

        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<HexagonPiece>(out HexagonPiece p))
        {
            RemoveHexagon(p);
        }
    }

}
