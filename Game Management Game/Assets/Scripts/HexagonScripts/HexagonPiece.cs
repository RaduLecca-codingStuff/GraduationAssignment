using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;
using UnityEngine.Events;
using TMPro;



public class HexagonPiece : MonoBehaviour
{
    //this is to avoid errors resulting from the hexagon deletion triggering OnTriggerExit 
    bool _IsToBeDestroyed;

    //this is to stop the coroutine from working after it cycled once
    bool _coroutineRegulator = false;

    bool _drag = false;
    [SerializeField]
    TMP_Text _Name;
    [SerializeField]
    GameObject RMenu;
    List<HexagonPiece> _neighbours = new List<HexagonPiece>();
    CameraMovementScript _movementScript;
    //Person and resource
    Person _person;
    Resource _resource;
    AudioSource _audioSource;
    SpriteRenderer _animationSprite;
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
    [Header("Audio clips")]
    public AudioClip takeAudio;
    public AudioClip placeAudio;
    [Header("Miscellaneous")]
    public type Type;
    public float timeNeeded;

    private void Awake()
    {
        _tiles = GameObject.FindGameObjectWithTag("Grid").GetComponentInChildren<Tilemap>();
        _movementScript = Camera.main.gameObject.GetComponent<CameraMovementScript>();
        this.SetHexColor();
        _audioSource = gameObject.AddComponent<AudioSource>();
        _audioSource.volume = GameManager.sfxVolume;
        _animationSprite = transform.Find("Animation").GetComponent<SpriteRenderer>();
        _animationSprite.gameObject.SetActive(false);
    }
    private void Start()
    {
        GameManager.WasPlaced = true;
        this.SetHexColor();
        CreateNewCluster();
        _audioSource.PlayOneShot(placeAudio);
    }
    void OnEnable()
    {
        _audioSource.volume = GameManager.sfxVolume;
    }
    void Update()
    {

        var mousepos = GetMousePos();
        RaycastHit2D hit = Physics2D.Raycast(mousepos, Vector2.up, .1f, 1 << 6);
        if (Input.GetMouseButtonDown(0) && !RMenu.activeSelf && !GameManager.IsPointerOverUIElement())
        {
            if (hit.collider != null)
            {
                if (!GameManager.selectedPiece._drag)
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
        else if (Input.GetMouseButtonDown(0) && GameManager.IsPointerOverUIElement())
        {
            GameManager.selectedPiece._drag = false;
            if (GameManager.selectedPiece)
                GameManager.selectedPiece._renderer.color = new UnityEngine.Color(1, 1, 1, 1);
        }
        //changes the color of the hexagon that will be where the hexagon is placed
        if (GameManager.selectedPiece._drag)
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
        else
        {
            UnityEngine.Color color = new UnityEngine.Color(1, 1, 1);
            _tiles.SetTileFlags(_prevTile, TileFlags.None);
            _tiles.SetColor(_prevTile, color);
        }


    }

    //Setting hexagons when initiated by other scripts
    public void SetUpHexagon(type T, int e, int s, int p, float time, string name)
    {
        Type = T;
        Purpose = p;
        Sustainability = s;
        Experience = e;
        _Name.text = name;
        timeNeeded = time;
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

    ////////////////////////////////////
    //  * Moving hexagons around *    //
    ////////////////////////////////////
    private void TakeHexagon(HexagonPiece p)
    {
        GameManager.selectedPiece = p;
        GameManager.InfoPiece = p;
        _drag = true;
        _neighbours.Clear();
        _audioSource.clip = takeAudio;
        _audioSource.Play();
        GameManager.selectedPiece._renderer.color = new UnityEngine.Color(1, 1, 1, .5f);
    }
    private void PlaceHexagon()
    {
        if (GameManager.selectedPiece._drag)
        {
            Vector3Int cellPoint = _tiles.WorldToCell(GetMousePos());
            Vector3 mouseP = _tiles.CellToWorld(cellPoint);
            GameManager.selectedPiece.transform.position = new Vector3(mouseP.x, mouseP.y, GameManager.selectedPiece.transform.position.z);
            GameManager.selectedPiece.transform.GetComponent<SpriteRenderer>().sortingOrder = 1;
            //GameManager.selectedPiece.CreateNewCluster();
            GameManager.selectedPiece._renderer.color = new UnityEngine.Color(1, 1, 1, 1);
            _audioSource.clip = placeAudio;
            _audioSource.Play();
        }
        GameManager.selectedPiece._drag = false;
    }


    //////////////////////////////
    //  * Opening the menu *    //
    //////////////////////////////
    private void OnMouseDrag()
    {
        if (!_coroutineRegulator)
            StartCoroutine(OpenMenuCouroutine());
    }
    private void OnMouseUp()
    {
        _animationSprite.gameObject.SetActive(false);
        StopAllCoroutines();
        _coroutineRegulator = false;
    }
    IEnumerator OpenMenuCouroutine()
    {
        _coroutineRegulator = true;
        yield return new WaitForSeconds(0.4f);
        _animationSprite.gameObject.SetActive(true);
        if (RMenu.activeSelf)
        {
            _animationSprite.GetComponent<Animator>().SetBool("IsOpeningMenu", false);
            yield return new WaitForSeconds(1.4f);
            _animationSprite.gameObject.SetActive(false);
            if (GameManager.selectedPiece)
                GameManager.selectedPiece._renderer.color = new UnityEngine.Color(1, 1, 1, 1);
            RMenu.SetActive(false);
            DeselectHexagon();
            StopAllCoroutines();
        }
        else
        {
            _animationSprite.GetComponent<Animator>().SetBool("IsOpeningMenu", true);
            yield return new WaitForSeconds(1.4f);
            _animationSprite.gameObject.SetActive(false);
            if (GameManager.selectedPiece)
                GameManager.selectedPiece._renderer.color = new UnityEngine.Color(1, 1, 1, 1);
            RMenu.SetActive(true);
            GameManager.WasMenuOpened = true;
            PositionMenu();
            DeselectHexagon();
            StopAllCoroutines();
        }

    }
    Vector3 GetMousePos()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    //Triggers for neighbour and cluster management
    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.gameObject.TryGetComponent<HexagonPiece>(out HexagonPiece piece))
        {
            if (!_neighbours.Contains(piece))
            {
                _neighbours.Add(piece);
                transform.parent = piece.transform.parent;
            }
            if (piece.transform.parent && piece.transform.parent != this.transform.parent)
            {
                if (piece.transform.parent.TryGetComponent<ClusterManager>(out ClusterManager clst))
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
            if (!piece._IsToBeDestroyed && !_coroutineRegulator)
                this.CreateNewCluster();
        }
    }

    //////////////////////////////////////////////
    //  * Cluster identification functions *    //
    //////////////////////////////////////////////
    public ClusterManager GetClusterManager()
    {
        if (transform.parent)
            if (transform.parent.TryGetComponent<ClusterManager>(out ClusterManager cl))
                return cl;
            else return null;
        else
            return new ClusterManager();
    }
    void CreateNewCluster()
    {
        if (!_IsToBeDestroyed)
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
                if (GameManager.selectedPiece.transform.parent)
                {
                    GameManager.selectedPiece.transform.parent = null;
                    if (!_coroutineRegulator)
                        CreateNewCluster();
                }

            }
        }
    }
    public ClusterManager GetOutlierCluster()
    {
        HexagonPiece highest = new HexagonPiece();
        foreach (HexagonPiece piece in _neighbours)
        {
            if (piece.GetClusterManager().NrOfHexagons() > highest.GetClusterManager().NrOfHexagons())
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
        if (isInRange(transform.position.x, -1 * (_movementScript.maxWidth / 2 - 1), _movementScript.maxWidth / 2 - 1) && isInRange(transform.position.y, -1 * (_movementScript.maxHeight / 2 - 1), _movementScript.maxHeight / 2 - 1))
        {
            RMenu.transform.localPosition = new Vector3(0, 0, 0);
        }
    }
    public IEnumerator WarningCoroutine()
    {
        _renderer.color = UnityEngine.Color.red;
        while (_renderer.color != UnityEngine.Color.white)
        {
            _renderer.color += new UnityEngine.Color(0, 0.1f, 0.1f);
            yield return new WaitForSeconds(0.1f);
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
    public List<HexagonPiece> GetHexagonNeighbours()
    {
        return _neighbours;
    }
    public String Name()
    {
        return _Name.text;
    }
    bool isInRange(float value, float left, float right)
    {
        return value > left && value < right;
    }
    public void DestroyHexagon()
    {
        Vector3Int cellPoint = _tiles.WorldToCell(this.transform.position);
        _tiles.SetTileFlags(cellPoint, TileFlags.None);
        _tiles.SetColor(cellPoint, new UnityEngine.Color(1, 1, 1));

        Debug.Log(cellPoint);

        _IsToBeDestroyed = true;
        Destroy(gameObject);
    }
    public void DeselectHexagon()
    {
        if (GameManager.selectedPiece)
        {
            GameManager.selectedPiece._drag = false;
            GameManager.selectedPiece._renderer.color = new UnityEngine.Color(1, 1, 1, 1);
        }

    }
}
