using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;
using UnityEngine.Events;

public class HexagonPiece : MonoBehaviour
{
    bool _drag=false;
    bool _inMenu = true;

    Vector3 _offset;
    List<HexagonPiece> _neighbours= new List<HexagonPiece>();

    //previous position and the next position would snap
    Vector3 _currentPos;
    Vector3 _newPos;

    Tilemap _tiles;
    SpriteRenderer _renderer;
    Vector3Int _prevTile;
    
    public enum type
    {
        discover,
        develop,
        deliver,
        upkeep
    }
    [Header("Impact values")]
    public int Purpose;
    public int Sustainability;
    public int Experience;
    [Header("Possible Sprites")]
    public Sprite spr1;
    public Sprite spr2;
    public Sprite spr3;
    public Sprite spr4;
    [Header("Miscellaneous")]
    public type Type;
    public string Name;
    
    private void Awake()
    {
        _tiles=GameObject.FindGameObjectWithTag("Grid").GetComponentInChildren<Tilemap>();
        
        _renderer=GetComponent<SpriteRenderer>();
        switch (Type)
        {
            case type.discover:
                _renderer.sprite = spr1;
                break;
            case type.develop:
                _renderer.sprite = spr2;
                break;
            case type.deliver:
                _renderer.sprite = spr3;
                break;
            case type.upkeep:
                _renderer.sprite = spr4;
                break;
        }
    }
    void Update()
    {
        var mousepos = GetMousePos();
        RaycastHit2D hit = Physics2D.Raycast(mousepos, Vector2.up, .1f, 1 << 6);
        if (Input.GetMouseButtonDown(0))
        {
            if (!_drag && hit.collider != null)
            {
                TakeHexagon(hit.collider.transform.GetComponent<HexagonPiece>());
                return;
            }
            else if (_drag)
            {
                PlaceHexagon();
                return;
            }
        }
        //cast ray that only reaches the layer the hexagons are on and then get the piece from there
        if (_drag)
        {
            UnityEngine.Color color = new UnityEngine.Color(255, 255, 255);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 worldPoint = ray.GetPoint(-ray.origin.z / ray.direction.z);
            Vector3Int cellPoint = _tiles.WorldToCell(worldPoint);
            
            //color change
            if (cellPoint != _prevTile)
            {
                _tiles.SetTileFlags(_prevTile, TileFlags.None);
                color = new UnityEngine.Color(1, 1, 1);
                _tiles.SetColor(_prevTile, color);
            }
            _tiles.SetTileFlags(cellPoint, TileFlags.None);
            color = new UnityEngine.Color(255, 255, 255);
            _tiles.SetColor(cellPoint, color);
            _prevTile = cellPoint;
        }


    }
    public void SetUpImpact(int p,int s, int e)
    {
        Purpose= p;
        Sustainability= s;
        Experience= e;
    }
    
    private void TakeHexagon(HexagonPiece p)
    {
        GameManager.selectedPiece = p;
        _drag = true;
    }
    
    private void PlaceHexagon()
    {
        Vector3Int cellPoint = _tiles.WorldToCell(GetMousePos());
        Vector3 mouseP= _tiles.CellToWorld(cellPoint);
        GameManager.selectedPiece.transform.position = new Vector3(mouseP.x,mouseP.y, GameManager.selectedPiece.transform.position.z);
        _drag = false;
    }

    Vector3 GetMousePos()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
    public bool IsInMenu()
    {
        return _inMenu;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 5)
        {
            _inMenu = true;
        }
        else if (collision.gameObject.TryGetComponent<HexagonPiece>(out HexagonPiece piece))
        {
            //might need to remove at least some parts of this because they're redundant
            if (!_drag && !_inMenu)
            {
                if (!_neighbours.Contains(piece) )
                {
                    _neighbours.Add(piece);
                }
                if (transform.parent && collision.transform.parent)
                {
                    transform.SetParent(piece.transform.parent);
                    piece.GetClusterManager().AddHexagon(this);
                }
            }
        }
        else
            return;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<HexagonPiece>(out HexagonPiece piece))
        {
            _neighbours.Clear();
        }
        if (collision.gameObject.layer == 5)
        {
            _inMenu = false;
        }
    }
    public ClusterManager GetClusterManager()
    {
        return transform.parent.GetComponent<ClusterManager>();
    }

    public ClusterManager GetOutlierCluster()
    {
        HexagonPiece highest=new HexagonPiece();
        foreach( HexagonPiece piece in _neighbours )
        {
            if(piece.GetClusterManager().NrOfHexagons()>highest.GetClusterManager().NrOfHexagons())
                highest = piece;
        }
        return highest.GetClusterManager();
    }
}
