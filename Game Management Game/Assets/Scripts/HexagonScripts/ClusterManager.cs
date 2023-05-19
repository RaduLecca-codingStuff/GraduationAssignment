using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class ClusterManager : MonoBehaviour
{
    //avg cluster valures
    float _avgPurpose = 0;
    float _avgSustainability = 0;
    float _avgExperience = 0;
    List<HexagonPiece> _HexagonPieces = new List<HexagonPiece>();
    int prevChildCount = 0;
    float _avgTimeNeeded;
    
    private void Start()
    {
        _HexagonPieces = new List<HexagonPiece>();
        this.gameObject.tag = "Cluster";
    }
    public float _timeRequired;
    void Update()
    {
        
        if (this._avgExperience>=GameManager.experience && this._avgPurpose>= GameManager.purpose && this._avgSustainability>=GameManager.sustainability)
        {
            GameManager.mainClusterM = this;
            GameManager.purpose = _avgPurpose;
            GameManager.sustainability = _avgSustainability; 
            GameManager.experience = _avgExperience;
            GameManager.currentTimeLeft= GameManager.currentClient.GetRemainingTime()-_timeRequired;
            Debug.Log(GameManager.currentClient.GetRemainingTime());
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
    
    //This is where calculations for each method's bonus is given in a 
    public void RefreshValues()
    {
        Debug.Log("Function is reached");
        float exp = 0;
        float pur = 0;
        float sust = 0;
        GameManager.CheckIfClusterExists();
        foreach (Transform obj in transform)
        { 
            //heres where the calculations for how much each hexagon adds. This changes with person and resource
            if (obj.TryGetComponent<HexagonPiece>(out HexagonPiece piece))
            {
                exp += piece.Experience;
                pur += piece.Purpose;
                sust += piece.Sustainability;
                
                //What bonuses are gained from each individual neighbour
                if (piece.GetHexagonNeighbours().Count>0)
                {
                    float e = 0;
                    float s = 0;
                    float p = 0;
                    foreach(HexagonPiece neighbour in piece.GetHexagonNeighbours())
                    {
                        if (neighbour.Type == piece.Type)
                        {
                            e += 1;
                            s += 1;
                            p += 1;
                        }
                        //add here how different types of neighbours 
                        switch (piece.Type)
                        {
                            case HexagonPiece.type.discover:
                                if(neighbour.Type == HexagonPiece.type.upkeep)
                                {
                                    e -= 5;
                                    s -= 5;
                                    p -= 5;
                                    neighbour.StartCoroutine(neighbour.WarningCoroutine());
                                }
                                else if (neighbour.Type == HexagonPiece.type.develop)
                                {
                                    e += 2;
                                    s += 2;
                                    p += 2;
                                }
                                switch (piece.Name())
                                {
                                    case "KPI":
                                        
                                        break;
                                    case "Emphatise":
                                        
                                        break;
                                    case "Risk Assesment":
                                        
                                        break;
                                    case "Insight":

                                        break;
                                    case "Requirements":
                             
                                        break;
                                    case "Research":
                                        
                                        break;
                                    case "Analyse":
                                     
                                        break;
                                    case "Initiation":
                                        
                                        break;
                                    case "Awareness":
                                        
                                        break;
                                    case "Design Space Analysis":
                                        
                                        break;
                                    case "Player Investigation":
                                        
                                        break;
                                    case "Journey Investigation":
                                        
                                        break;
                                    case "Game Storm":
                                        
                                        break;
                                    case "Subject Matter Expert Investigation":
                                       
                                        break;
                                    case "Game Concept":
      
                                        break;
                                    case "Identify Game Loop":
                                        
                                        break;
                                    case "Feature Roadmap":
                                        
                                        break;
                                    default: break;
                                }
                                break;
                            case HexagonPiece.type.develop:
                                if (neighbour.Type == HexagonPiece.type.deliver)
                                {
                                    e += 2;
                                    s += 2;
                                    p += 2;
                                }
                                switch (piece.Name())
                                {
                                    case "Action":

                                        break;
                                    case "Minimum Viable Product":

                                        break;
                                    case "Minimum Usable Product":

                                        break;
                                    case "Solution":

                                        break;
                                    case "Prototyping":

                                        break;
                                    case "Proof of Concept":

                                        break;
                                    case "Execution":

                                        break;
                                    case "Fabrication":

                                        break;
                                    case "Concept":

                                        break;
                                    case "Playtest":

                                        break;
                                    case "Iterate":

                                        break;
                                    case "Elaboration":

                                        break;
                                    case "Ideation":

                                        break;
                                    case "Design":

                                        break;
                                    case "Define":

                                        break;
                                    case "Design Documentation":

                                        break;
                                    case "Wireframes":

                                        break;
                                    case "Paper Prototype":

                                        break;
                                    case "Click-thru":

                                        break;
                                    case "Feature Roadmap":

                                        break;
                                    case "Design Elaborations":

                                        break;
                                    default: break;
                                }
                                break;
                            case HexagonPiece.type.deliver:
                                if (neighbour.Type == HexagonPiece.type.upkeep)
                                {
                                    e += 2;
                                    s += 2;
                                    p += 2;
                                }
                                switch (piece.Name())
                                {
                                    case "Playtest":
                                        
                                        break;
                                    case "Validate":
                                        
                                        break;
                                    case "Launch":
                                        
                                        break;
                                    case "Design Tuning":
                                        
                                        break;
                                    case "Implementation":
                                        
                                        break;
                                    case "Closing":
                                        
                                        break;
                                    case "Business Model":
                                        
                                        break;
                                    case "Production":
                                        
                                        break;
                                    case "User Stories":
                                        
                                        break;
                                    case "Minimum Usable Product":
                                        
                                        break;
                                    case "Minimum Viable Product":
                                       
                                        break;
                                    case "Feature Roadmap":
                                        
                                        break;
                                    default: break;
                                }
                                break;
                            case HexagonPiece.type.upkeep:
                                switch (piece.Name())
                                {
                                    case "Monetise":
                                        
                                        break;
                                    case "Upscale":
                                        
                                        break;
                                    case "Support":
                                        
                                        break;
                                    case "Updates":
                                        
                                        break;
                                    case "Maintenance":
                                        
                                        break;
                                    case "Feature Roadmap":
                                        
                                        break;
                                    default: break;
                                }
                                break;
                            default:
                                
                                break;
                        }
                    }
                    exp += e;
                    sust += s;
                    pur += p;
                }
                //What person is assigned to method
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
                        if (piece.Name() == "Fine-Tuning" || piece.Name() == "Execution" || piece.Name() == "Updates" || piece.Name() == "Paper Prototype" || piece.Name() == "Wireframes")
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
                        
                        break;
                }
                //what resource adds to project
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
                      
                        break;
                }
            }
        }
        
        _avgExperience = exp;
        _avgPurpose = pur;
        _avgSustainability = sust;
        RefreshRemainingTime();
        
    }
    public void RefreshRemainingTime()
    {
        float time=0;
        if (this == GameManager.mainClusterM)
        {
            foreach (Transform obj in transform)
            {
                if (obj.TryGetComponent<HexagonPiece>(out HexagonPiece piece))
                {
                    time += piece.timeNeeded;
                }
            }
            _timeRequired = time;
            
            
            if (GameManager.currentTimeLeft <= 0)
            {
                GameManager.currentTimeLeft = 0;
                foreach (Transform obj in transform)
                {
                    //heres where the calculations for how much each hexagon adds. This changes with person and resource
                    if (obj.TryGetComponent<HexagonPiece>(out HexagonPiece piece))
                    {
                        if (piece.timeNeeded > 1)
                            piece.StartCoroutine(piece.WarningCoroutine());
                    }
                }
            }
            
        }
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

    public void SetAsMainCluster()
    {
        GameManager.mainClusterM = this;
        GameManager.purpose = _avgPurpose;
        GameManager.sustainability = _avgSustainability;
        GameManager.experience = _avgExperience;
        GameManager.currentTimeLeft = GameManager.currentClient.GetRemainingTime() - _timeRequired;
    }
    private void OnDestroy()
    {
        foreach(Transform obj in transform)
        {
            Destroy(obj);
        }
    }

}
