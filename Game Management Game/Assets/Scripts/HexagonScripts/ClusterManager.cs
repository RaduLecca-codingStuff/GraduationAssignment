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

        if (this._avgExperience >= GameManager.experience && this._avgPurpose >= GameManager.purpose && this._avgSustainability >= GameManager.sustainability)
        {
            GameManager.mainClusterM = this;
            GameManager.purpose = _avgPurpose;
            GameManager.sustainability = _avgSustainability;
            GameManager.experience = _avgExperience;
            GameManager.currentTimeLeft = GameManager.currentClient.GetRemainingTime() - _timeRequired;
        }
        if (prevChildCount != transform.childCount)
        {
            RefreshValues();
            prevChildCount = transform.childCount;
        }
        if (this.transform.childCount == 0)
        {
            if (this == GameManager.mainClusterM)
            {
                GameManager.mainClusterM = new ClusterManager();
                GameManager.purpose = 0;
                GameManager.sustainability = 0;
                GameManager.experience = 0;
            }
            Destroy(this.gameObject);
        }

    }

    //This is where calculations for each method's bonus is given in a 
    public void RefreshValues()
    {
        _avgExperience = 0;
        _avgPurpose =0;
        _avgSustainability = 0;
        float exp = 0;
        float pur = 0;
        float sust = 0;
        GameManager.CheckIfClusterExists();
        foreach (HexagonPiece hexagon in transform.gameObject.GetComponentsInChildren<HexagonPiece>())
        {
            exp = hexagon.Experience;
            sust = hexagon.Sustainability;
            pur = hexagon.Purpose;
            foreach(HexagonPiece neighbour in hexagon.GetHexagonNeighbours())
            {
                if(hexagon.Type == neighbour.Type)
                {
                    exp += .1f;
                    sust += .1f;
                    pur += .1f;
                }
                if((hexagon.Type==HexagonPiece.type.discover && neighbour.Type == HexagonPiece.type.develop) ||
                    (hexagon.Type == HexagonPiece.type.develop && neighbour.Type == HexagonPiece.type.deliver)||
                    (hexagon.Type == HexagonPiece.type.deliver && neighbour.Type == HexagonPiece.type.upkeep))
                {
                    exp += .5f;
                    sust += .5f;
                    pur += .5f;
                }

                if ((hexagon.Type == HexagonPiece.type.discover && neighbour.Type == HexagonPiece.type.deliver)||
                    (hexagon.Type == HexagonPiece.type.discover && neighbour.Type == HexagonPiece.type.upkeep))
                {
                    exp -= 5;
                    sust -= 5;
                    pur -= 5;
                    neighbour.StartCoroutine(neighbour.WarningCoroutine());
                }

            }
            //add bonuses here
            switch (hexagon.GetPersonOccupation())
            {
                case "Stakeholder":
                    if (hexagon.Type == HexagonPiece.type.discover)
                    {
                        sust += 2;
                        pur += 2;
                        exp += 2;
                    }
                    if (hexagon.Name() == "Design Elaborations")
                    {
                        sust += 2;
                        pur += 2;
                    }
                    break;
                case "Sponsor":
                    if (hexagon.Type == HexagonPiece.type.upkeep)
                    {
                        sust += 2;
                        pur += 2;
                        exp += 2;
                    }
                    if (hexagon.Name() == "Game Concept")
                    {
                        sust += 2;
                    }
                    break;
                case "EndUser":
                    sust += 2;
                    pur += 2;
                    exp += 2;
                    if (hexagon.Name() == "Playtest" || hexagon.Name() == "Paper Prototype" || hexagon.Name() == "User Stories")
                    {
                        exp += 2;
                    }
                    break;
                case "UXdesigner":
                    if (hexagon.Type == HexagonPiece.type.develop)
                    {
                        sust += 2;
                        pur += 2;
                        exp += 2;
                    }
                    if (hexagon.Name() == "Fine-Tuning" || hexagon.Name() == "Execution" || hexagon.Name() == "Updates" || hexagon.Name() == "Paper Prototype")
                    {
                        exp += 2;
                    }
                    break;
                case "ProjectManager":
                    sust += 2;
                    pur += 2;
                    exp += 2;

                    break;
                case "PSO":
                    if (hexagon.Type == HexagonPiece.type.upkeep)
                    {
                        sust += 2;
                        exp += 2;
                    }
                    break;
                case "SME":
                    if (hexagon.Type == HexagonPiece.type.develop)
                    {
                        sust += 2;
                        pur += 2;
                        exp += 2;
                    }
                    if (hexagon.Name() == "Solution" || hexagon.Name() == "Concept" || hexagon.Name() == "Execution" || hexagon.Name() == "Proof of Concept")
                    {
                        sust += 2;
                        exp += 2;
                    }
                    else if (hexagon.Name() == "Subject Matter Expert Investigation")
                    {
                        sust += 3;
                        pur += 3;
                        exp += 3;
                    }
                    break;
                case "ProductOwner":
                    if (hexagon.Type == HexagonPiece.type.discover)
                    {
                        sust += 2;
                        pur += 2;
                        exp += 2;
                    }
                    break;
                case "Programmer":
                    if (hexagon.Type == HexagonPiece.type.develop)
                    {
                        sust += 2;
                        pur += 2;
                        exp += 2;
                    }
                    break;

                case "ExperienceExpert":
                    sust += 2;
                    pur += 2;
                    exp += 2;
                    break;
                case "VisualDesigner":
                    if (hexagon.Type == HexagonPiece.type.develop)
                    {
                        sust += 2;
                        pur += 2;
                        exp += 2;
                    }
                    if (hexagon.Name() == "Design" || hexagon.Name() == "Prototype" || hexagon.Name() == "Proof of Concept" || hexagon.Name() == "Paper Prototype")
                    {
                        pur += 2;
                        exp += 2;
                    }
                    break;
                case "Tester":
                    sust += 2;
                    pur += 2;
                    exp += 2;
                    break;
            }
            //resources buff
            switch (hexagon.GetResourceValue())
            {
                default: break;
                case 1:
                    exp *= 1.3f;
                    sust *= 1.3f;
                    pur *= 1.3f;
                    break;
                case 2:
                    exp *= 1.6f;
                    sust *= 1.6f;
                    pur *= 1.6f;
                    break;
                case 3:
                    exp *= 1.9f;
                    sust *= 1.9f;
                    pur *= 1.9f;
                    break;

            }


            _avgExperience += exp;
            _avgPurpose += pur; 
            _avgSustainability += sust;

        }
        RefreshRemainingTime();

    }
    public void RefreshRemainingTime()
    {
        float time = 0;
        float maxTime = 0;
        if (this == GameManager.mainClusterM)
        {
            foreach (Transform obj in transform)
            {
                if (obj.TryGetComponent<HexagonPiece>(out HexagonPiece piece))
                {
                    time += piece.timeNeeded;
                    if (piece.timeNeeded > maxTime)
                        maxTime = piece.timeNeeded;
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
                        if (piece.timeNeeded == maxTime || piece.timeNeeded == maxTime - 1 || piece.timeNeeded == maxTime - 2)
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
        if (cl)
        {
            if (cl.transform.childCount >= this.transform.childCount)
                newparent = cl.transform;
            else
                newparent = transform;
            foreach (Transform obj in transform)
            {
                obj.SetParent(newparent);
                RefreshValues();
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.TryGetComponent<HexagonPiece>(out HexagonPiece p))
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
        foreach (Transform obj in transform)
        {
            Destroy(obj);
        }
    }

}
