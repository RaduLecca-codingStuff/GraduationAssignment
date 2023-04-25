using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;
using UnityEngine.Events;
using TMPro;
using UnityEditorInternal;

public class HexagonPiece : MonoBehaviour
{
    bool _drag=false;
    [SerializeField]
    TMP_Text _Name;
    [SerializeField]
    GameObject RMenu;
    List<HexagonPiece> _neighbours= new List<HexagonPiece>();
    CameraMovementScript _movementScript;
    //Person and resource
    Person _person;
    Resource _resource;
    

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

    private void Awake()
    {
        _tiles=GameObject.FindGameObjectWithTag("Grid").GetComponentInChildren<Tilemap>();
        _movementScript = Camera.main.gameObject.GetComponent<CameraMovementScript>();
        this.SetHexColor();
        _renderer.sortingOrder = 5;
    }
    private void Start()
    {
        gameObject.GetComponentInChildren<Canvas>().overrideSorting = true;
        this.SetHexColor();
        CreateNewCluster();
    }
    void Update()
    {
        var mousepos = GetMousePos();
        RaycastHit2D hit = Physics2D.Raycast(mousepos, Vector2.up, .1f,1<<6);
        if (Input.GetMouseButtonDown(0) && !RMenu.activeSelf)
        {
            if(hit.collider != null )
            {
                if (!_drag )
                {
                    TakeHexagon(hit.collider.transform.GetComponent<HexagonPiece>());
                }
            }
            else if (_drag)
            {
                PlaceHexagon();
                return;
            }
        }
        //changes the color of the hexagon that will be where the hexagon is placed
        if (_drag)
        {
            UnityEngine.Color color = new UnityEngine.Color(255, 255, 255);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 worldPoint = ray.GetPoint(-ray.origin.z / ray.direction.z);
            Vector3Int cellPoint = _tiles.WorldToCell(worldPoint);
            
            //color change
            if (cellPoint != _prevTile )
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

    //Setting hexagons when initiated by other scripts
    public void SetUpHexagon(type T,int e, int s, int p, string name)
    {
        Type = T;
        Purpose = p;
        Sustainability = s;
        Experience = e;
        _Name.text = name;
        SetHexColor();
    }
    void SetHexColor()
    {
        _renderer = GetComponent<SpriteRenderer>();
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

    //Moving hexagons around
    private void TakeHexagon(HexagonPiece p)
    {
        GameManager.selectedPiece = p;
        _drag = true;
        _neighbours.Clear();

    }
    private void PlaceHexagon()
    {
        Vector3Int cellPoint = _tiles.WorldToCell(GetMousePos());
        Vector3 mouseP= _tiles.CellToWorld(cellPoint);
        GameManager.selectedPiece.transform.position = new Vector3(mouseP.x,mouseP.y, GameManager.selectedPiece.transform.position.z);
        GameManager.selectedPiece.transform.GetComponent<SpriteRenderer>().sortingOrder = 1;
        GameManager.selectedPiece.transform.GetComponentInChildren<Canvas>().overrideSorting = false;
        GameManager.selectedPiece.CreateNewCluster();
        
        _drag = false;
    }
    //Menu opening
    private void OnMouseDrag()
    {
        StartCoroutine(OpenMenuCouroutine());
    }
    private void OnMouseUp() 
    { 
        StopAllCoroutines();
    }
    IEnumerator OpenMenuCouroutine() 
    {
        if (RMenu.activeSelf)
        {
            yield return new WaitForSeconds(1.4f);
            RMenu.SetActive(false);
            _drag = false;
        }
        else
        {
            yield return new WaitForSeconds(1.4f);
            RMenu.SetActive(true);
            PositionMenu();
            _drag = false;
        }
    }


    Vector3 GetMousePos()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        
        if (collision.gameObject.TryGetComponent<HexagonPiece>(out HexagonPiece piece))
        {
            if (!_neighbours.Contains(piece))
            {
                _neighbours.Add(piece);
                transform.parent = piece.transform.parent;
            }
            if (piece.transform.parent &&  piece.transform.parent != this.transform.parent)
            {
                if(piece.transform.parent.TryGetComponent<ClusterManager>(out ClusterManager clst))
                {
                    clst.MergeClusters(this.GetClusterManager());
                }    
            }
        }
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<HexagonPiece>(out HexagonPiece piece))
        {
            _neighbours.Clear();
            this.GetClusterManager().RemoveHexagon(this);
            CreateNewCluster();
        }
    }

    //Cluster identification functions
    public ClusterManager GetClusterManager()
    {
        return transform.parent.GetComponent<ClusterManager>();
    }
    void CreateNewCluster()
    {
        if (!transform.parent)
        {
            GameObject cluster = new GameObject();
            cluster.AddComponent<ClusterManager>();
            this.transform.SetParent(cluster.transform);
            cluster.name = "Cluster";
            cluster.AddComponent<Rigidbody2D>().gravityScale = 0;
        }
        else
        {
            if(GameManager.selectedPiece.transform.parent)
            GameManager.selectedPiece.transform.parent = null;
            CreateNewCluster();
        }
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
    //Get pieces which will be used to get higher values to the hexagon
    public void SetPerson(Person p)
    {
        _person = p;
    }
    public void SetResource(Resource r) 
    { 
        _resource = r;
    }
    void PositionMenu()
    {
        if (transform.position.x >= _movementScript.maxWidth / 2 - 1)
        {
            RMenu.transform.localPosition = new Vector3(-1510, transform.localPosition.y, 0);
        }
        if (transform.position.x <= -1 * (_movementScript.maxWidth / 2 - 1))
        {
            RMenu.transform.localPosition += new Vector3(1510, 0, 0);
        }
        if (transform.position.y <= -1 * (_movementScript.maxHeight / 2 - 1))
        {
            RMenu.transform.localPosition += new Vector3(0, 1510, 0);
        }
        if(isInRange(transform.position.x, -1 * (_movementScript.maxWidth / 2 - 1), _movementScript.maxWidth / 2 - 1) && isInRange(transform.position.y, -1 * (_movementScript.maxHeight / 2 - 1), _movementScript.maxHeight / 2 - 1))
        {
            RMenu.transform.localPosition = new Vector3(0, 0, 0);
        }
    }
    public string GetPersonOccupation()
    {
        if (_person == null)
            return "";
        else
            return _person.occupation.ToString();
    }
    public int GetResourceValue()
    {
        if (_resource == null)
            return 0;
        else
            return _resource.getValue();
    }
    public String Name()
    {
        return _Name.text;
    }

    bool isInRange(float value, float left, float right)
    {
        return value > left && value < right;
    }
}
