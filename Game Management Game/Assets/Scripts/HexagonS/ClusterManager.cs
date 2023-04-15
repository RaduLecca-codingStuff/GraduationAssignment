using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClusterManager : MonoBehaviour
{
    public int avgPurpose = 0;
    public int avgSustainability = 0;
    public int avgExperience = 0;
    List<HexagonPiece> _HexagonPieces = new List<HexagonPiece>();
    private void Start()
    {
        _HexagonPieces = new List<HexagonPiece>();
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
        foreach (Transform obj in transform)
        {
            if (obj.TryGetComponent<HexagonPiece>(out HexagonPiece piece))
            {
                avgExperience += piece.Experience;
                avgPurpose += piece.Purpose;
                avgSustainability += piece.Sustainability;
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
