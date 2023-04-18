using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClusterManager : MonoBehaviour
{
    public float avgPurpose = 0;
    public float avgSustainability = 0;
    public float avgExperience = 0;
    List<HexagonPiece> _HexagonPieces = new List<HexagonPiece>();
    private void Start()
    {
        _HexagonPieces = new List<HexagonPiece>();
        this.gameObject.tag = "Cluster";
    }

    void Update()
    {
        if ((_HexagonPieces.Count > GameManager.mainClusterM._HexagonPieces.Count)&&_HexagonPieces!=null)
        {
            GameManager.mainClusterM = this;
            GameManager.purpose = avgPurpose;
            GameManager.sustainability = avgSustainability; 
            GameManager.experience = avgExperience;
        }
        if(transform.childCount==0)
            Destroy(this.gameObject);
    }
    public void AddHexagon(HexagonPiece piece)
    {
        if (!_HexagonPieces.Contains(piece))
        {
            _HexagonPieces.Add(piece);
            avgExperience += piece.Experience;
            avgPurpose += piece.Purpose;
            avgSustainability += piece.Sustainability;
            piece.transform.SetParent(this.transform);
        }
        
    }
    public void RefreshHexagonList()
    {
        _HexagonPieces= new List<HexagonPiece>();
        foreach (Transform obj in transform)
        {
            if (obj.TryGetComponent<HexagonPiece>(out HexagonPiece piece))
            {
                if(!_HexagonPieces.Contains(piece))
                    _HexagonPieces.Add(piece);
            }
        }
    }
    public void RefreshValues()
    {
        avgExperience =0;
        avgPurpose = 0;
        avgSustainability = 0;

        float exp = 0;
        float pur = 0;
        float sust = 0;

        foreach (Transform obj in transform)
        {
            //heres where the calculations for how much each hexagon adds. This cahnges with person and resource
            if (obj.TryGetComponent<HexagonPiece>(out HexagonPiece piece))
            {
                exp += piece.Experience;
                pur += piece.Purpose;
                sust += piece.Sustainability;
                if (piece.GetPerson() == null)
                {
                    //calculate here what happens here based solely on the resource buff
                    //Switch
                    if(piece.getResource() == null)
                    {
                        return;
                    }
                    else
                    {
                        switch (piece.getResource().getValue())
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
                        }
                    }
                    return;
                }
                else
                {
                    //finish the calculations here later. just make sure that these make sense (ex: if the person is a visual designer, visual aspects of game development are what he's gonna buff up)
                    //make the check based on either the name of the hexagon or the type of hexagon
                    switch (piece.GetPerson().occupation)
                    {
                        default:
                        case Person.Occupation.Stakeholder:
                            if (piece.Type == HexagonPiece.type.discover)
                            {
                                sust += 2;
                                pur += 3;
                            }
                            break;
                        case Person.Occupation.Sponsor:
                            if (piece.Type == HexagonPiece.type.upkeep)
                            {
                                sust += 3;
                            }
                            break;
                        case Person.Occupation.EndUser:
                            if (piece.Name()=="Test" )
                            {
                                exp += 3;
                            }
                            break;
                        case Person.Occupation.UXdesigner:
                            if (piece.Name() == "Fine-Tuning" || piece.Name() == "Execution" || piece.Name() == "Updates")
                            {
                                exp += 3;
                            }
                            break;
                        case Person.Occupation.ProjectManager:
                            sust += 3;
                            exp += 1;
                            
                            break;
                        case Person.Occupation.PSO:
                            if (piece.Type == HexagonPiece.type.upkeep)
                            {
                                sust += 3;
                                exp += 3;
                            }
                            break;
                        case Person.Occupation.SME:
                            if (piece.Type == HexagonPiece.type.discover || piece.Name() == "Solution" || piece.Name() == "Concept" || piece.Name() == "Execution" || piece.Name() == "Proof of Concept")
                            {
                                sust += 3;
                                exp += 3;
                            }
                            break;
                        case Person.Occupation.ProductOwner:
                            if (piece.Type == HexagonPiece.type.discover)
                            {
                                pur += 3;
                            }
                            break;
                        case Person.Occupation.Programmer:
                            if (piece.Type == HexagonPiece.type.develop)
                            {
                                sust += 2;
                                pur += 3;
                            }
                            break;
                            //might need to remove due to redundancy
                        case Person.Occupation.ExperienceExpert:
                            if (piece.Name() == "Fine-Tuning" || piece.Name() == "Executiion" || piece.Name() == "Updates")
                            {
                                exp += 3;
                            }
                            break;
                        case Person.Occupation.VisualDesigner:
                            if (piece.Name() == "Design" || piece.Name() == "Prototype" || piece.Name() == "Proof of Concept")
                            {
                                pur += 3;
                                exp += 2;
                            }
                            break;
                        case Person.Occupation.Tester:
                            sust += 1;
                            pur += 1;
                            exp += 1;
                            break;

                    }
                    if (piece.getResource() == null)
                    {
                        return;
                    }
                    else
                    {
                        switch (piece.getResource().getValue())
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
                        }
                    }

                }
                avgExperience += exp;
                avgPurpose += pur;
                avgSustainability += sust;

            }
        }
    }
    public bool isInHere(HexagonPiece piece)
    {
        return _HexagonPieces.Contains(piece);
    }
    public void RemoveHexagon(HexagonPiece piece)
    {
        avgExperience -= piece.Experience;
        avgPurpose -= piece.Purpose;
        avgSustainability -= piece.Sustainability;
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
                obj.SetParent(newparent.transform);
            }
            RefreshHexagonList();
            RefreshValues();
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
            else
                AddHexagon(p);
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
