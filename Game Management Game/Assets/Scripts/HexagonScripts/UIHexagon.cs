using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class UIHexagon : MonoBehaviour, IPointerClickHandler
{
    public GameObject HexagonPrefab;
    public enum type
    {
        discover,
        develop,
        deliver,
        upkeep
    }
    [Header("Hexagon type")]
    public type Type;

    [Header("Sprites")]
    [SerializeField]
    private Sprite spr1;
    [SerializeField]
    private Sprite spr2;
    [SerializeField]
    private Sprite spr3;
    [SerializeField]
    private Sprite spr4;

    TMP_Text _text;

    Tilemap _tiles;
    private Image _renderer;
    bool _selected = false;
    Vector3Int _prevTile;
    AudioSource _audioSource;
    HexagonPiece _pieceRef;
    bool isOverUI;

    // Start is called before the first frame update
    void Awake()
    {
        _pieceRef = new HexagonPiece();
        _tiles = GameObject.FindGameObjectWithTag("Grid").GetComponentInChildren<Tilemap>();
        _renderer = GetComponent<Image>();
        _audioSource = GetComponent<AudioSource>();
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
        _text = GetComponentInChildren<TMP_Text>();
    }

    void OnEnable()
    {
        _audioSource.volume = GameManager.sfxVolume;
    }
    // Update is called once per frame
    void Update()
    {
        if (_selected)
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

            //check if the player clicks on an ui element or not
            if (Input.GetMouseButtonDown(0) )
            {
                if (!GameManager.IsPointerOverUIElement())
                PlaceHexagon();
                else
                {
                    _renderer.color = new Color(_renderer.color.r, _renderer.color.g, _renderer.color.b, 1);
                    _selected = false;
                }
            }
        }
    }

    Vector3 GetMousePos()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
    void PlaceHexagon()
    {
        if (GameManager.currentTimeLeft > 0)
        {
            Vector3Int cellPoint = _tiles.WorldToCell(GetMousePos());
            Vector3 mouseP = _tiles.CellToWorld(cellPoint);
            if (HexagonPrefab.TryGetComponent<HexagonPiece>(out HexagonPiece piece))
            {
                GameObject g = Instantiate(HexagonPrefab);
                g.transform.position = new Vector3(mouseP.x, mouseP.y, 0);
                _renderer.color = new Color(_renderer.color.r, _renderer.color.g, _renderer.color.b, 1);
                this.gameObject.SetActive(false);
            }
            else
            {
                Debug.LogError("Prefab does not contain a HexagonPiece component");
            }
        }
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if(GameManager.currentTimeLeft > 0)
        {
            _selected = true;
            _audioSource.Play();
            _renderer.color = new Color(_renderer.color.r, _renderer.color.g, _renderer.color.b, .5f);
            if (HexagonPrefab.TryGetComponent<HexagonPiece>(out HexagonPiece piece))
            {
                //where the 3 values are added + where stuff is set up
                switch (Type)
                {
                    case type.discover:
                        switch (_text.text)
                        {
                            case "KPI":
                                piece.SetUpHexagon(HexagonPiece.type.discover, 1, 1, 1, 5, _text.text);
                                break;
                            case "Emphatise":
                                piece.SetUpHexagon(HexagonPiece.type.discover, 0, 1, 1, 5, _text.text);
                                break;
                            case "Risk Assesment":
                                piece.SetUpHexagon(HexagonPiece.type.discover, 0, 1, 0, 5, _text.text);
                                break;
                            case "Insight":
                                piece.SetUpHexagon(HexagonPiece.type.discover, 1, 0, 1, 5, _text.text);
                                break;
                            case "Requirements":
                                piece.SetUpHexagon(HexagonPiece.type.discover, 0, 1, 0, 6, _text.text);
                                break;
                            case "Research":
                                piece.SetUpHexagon(HexagonPiece.type.discover, 1, 1, 1, 7, _text.text);
                                break;
                            case "Analyse":
                                piece.SetUpHexagon(HexagonPiece.type.discover, 1, 1, 1, 6, _text.text);
                                break;
                            case "Initiation":
                                piece.SetUpHexagon(HexagonPiece.type.discover, 0, 0, 1, 5, _text.text);
                                break;
                            case "Awareness":
                                piece.SetUpHexagon(HexagonPiece.type.discover, 1, 0, 0, 5, _text.text);
                                break;
                            case "Design Space Analysis":
                                piece.SetUpHexagon(HexagonPiece.type.discover, 0, 3, 1, 2, _text.text);
                                break;
                            case "Player Investigation":
                                piece.SetUpHexagon(HexagonPiece.type.discover, 2, 1, 0, 3, _text.text);
                                break;
                            case "Journey Investigation":
                                piece.SetUpHexagon(HexagonPiece.type.discover, 0, 0, 1, 5, _text.text);
                                break;
                            case "Game Storm":
                                piece.SetUpHexagon(HexagonPiece.type.discover, 1, 1, 2, 3, _text.text);
                                break;
                            case "SME Investigation":
                                piece.SetUpHexagon(HexagonPiece.type.discover, 0, 2, 1, 2, _text.text);
                                break;
                            case "Game Concept":
                                piece.SetUpHexagon(HexagonPiece.type.discover, 2, 2, 2, 2, _text.text);
                                break;
                            case "Identify Game Loop":
                                piece.SetUpHexagon(HexagonPiece.type.discover, 2, 0, 2, 2, _text.text);
                                break;
                            case "Feature Roadmap":
                                piece.SetUpHexagon(HexagonPiece.type.discover, 1, 2, 1, 2, _text.text);
                                break;
                            default: break;
                        }
                        break;
                    case type.develop:
                        switch (_text.text)
                        {
                            case "Action":
                                piece.SetUpHexagon(HexagonPiece.type.develop, 1, 0, 0, 5, _text.text);
                                break;
                            case "Minimum Viable Product":
                                piece.SetUpHexagon(HexagonPiece.type.develop, 0, 1, 1, 5, _text.text);
                                break;
                            case "Minimum Usable Product":
                                piece.SetUpHexagon(HexagonPiece.type.develop, 0, 1, 1, 7, _text.text);
                                break;
                            case "Solution":
                                piece.SetUpHexagon(HexagonPiece.type.develop, 1, 1, 1, 3, _text.text);
                                break;
                            case "Prototyping":
                                piece.SetUpHexagon(HexagonPiece.type.develop, 1, 1, 1, 3, _text.text);
                                break;
                            case "Proof of Concept":
                                piece.SetUpHexagon(HexagonPiece.type.develop, 1, 0, 1, 5, _text.text);
                                break;
                            case "Execution":
                                piece.SetUpHexagon(HexagonPiece.type.develop, 0, 1, 1, 6, _text.text);
                                break;
                            case "Fabrication":
                                piece.SetUpHexagon(HexagonPiece.type.develop, 1, 1, 1, 5, _text.text);
                                break;
                            case "Concept":
                                piece.SetUpHexagon(HexagonPiece.type.develop, 0, 0, 1, 5, _text.text);
                                break;
                            case "Playtest":
                                piece.SetUpHexagon(HexagonPiece.type.develop, 2, 1, 0, 3, _text.text);
                                break;
                            case "Iterate":
                                piece.SetUpHexagon(HexagonPiece.type.develop, 1, 1, 1, 6, _text.text);
                                break;
                            case "Elaboration":
                                piece.SetUpHexagon(HexagonPiece.type.develop, 0, 1, 0, 5, _text.text);
                                break;
                            case "Ideation":
                                piece.SetUpHexagon(HexagonPiece.type.develop, 1, 0, 0, 5, _text.text);
                                break;
                            case "Design":
                                piece.SetUpHexagon(HexagonPiece.type.develop, 0, 1, 1, 6, _text.text);
                                break;
                            case "Define":
                                piece.SetUpHexagon(HexagonPiece.type.develop, 0, 1, 0, 7, _text.text);
                                break;
                            case "Design Documentation":
                                piece.SetUpHexagon(HexagonPiece.type.develop, 0, 2, 2, 2, _text.text);
                                break;
                            case "Wireframes":
                                piece.SetUpHexagon(HexagonPiece.type.develop, 1, 1, 2, 2, _text.text);
                                break;
                            case "Paper Prototype":
                                piece.SetUpHexagon(HexagonPiece.type.develop, 0, 2, 2, 2, _text.text);
                                break;
                            case "Click-thru":
                                piece.SetUpHexagon(HexagonPiece.type.develop, 0, 2, 2, 2, _text.text);
                                break;
                            case "Feature Roadmap":
                                piece.SetUpHexagon(HexagonPiece.type.develop, 1, 2, 1, 2, _text.text);
                                break;
                            case "Design Elaborations":
                                piece.SetUpHexagon(HexagonPiece.type.develop, 0, 2, 0, 2, _text.text);
                                break;
                        }
                        break;
                    case type.deliver:
                        switch (_text.text)
                        {
                            case "Playtest":
                                piece.SetUpHexagon(HexagonPiece.type.deliver, 0, 2, 1, 3, _text.text);
                                break;
                            case "Validate":
                                piece.SetUpHexagon(HexagonPiece.type.deliver, 0, 2, 0, 3, _text.text);
                                break;
                            case "Launch":
                                piece.SetUpHexagon(HexagonPiece.type.deliver, 0, 0, 1, 5, _text.text);
                                break;
                            case "Design Tuning":
                                piece.SetUpHexagon(HexagonPiece.type.deliver, 2, 0, 0, 3, _text.text);
                                break;
                            case "Implementation":
                                piece.SetUpHexagon(HexagonPiece.type.deliver, 1, 0, 2, 3, _text.text);
                                break;
                            case "Closing":
                                piece.SetUpHexagon(HexagonPiece.type.deliver, 0, 1, 0, 5, _text.text);
                                break;
                            case "Business Model":
                                piece.SetUpHexagon(HexagonPiece.type.deliver, 1, 1, 0, 7, _text.text);
                                break;
                            case "Production":
                                piece.SetUpHexagon(HexagonPiece.type.deliver, 0, 1, 0, 6, _text.text);
                                break;
                            case "User Stories":
                                piece.SetUpHexagon(HexagonPiece.type.deliver, 2, 1, 0, 3, _text.text);
                                break;
                            case "Minimum Usable Product":
                                piece.SetUpHexagon(HexagonPiece.type.deliver, 0, 1, 1, 5, _text.text);
                                break;
                            case "Minimum Viable Product":
                                piece.SetUpHexagon(HexagonPiece.type.deliver, 0, 1, 1, 6, _text.text);
                                break;
                            case "Feature Roadmap":
                                piece.SetUpHexagon(HexagonPiece.type.deliver, 1, 2, 1, 2, _text.text);
                                break;

                        }
                        break;
                    case type.upkeep:
                        switch (_text.text)
                        {
                            case "Monetise":
                                piece.SetUpHexagon(HexagonPiece.type.upkeep, 0, 2, 0, 2, _text.text);
                                break;
                            case "Upscale":
                                piece.SetUpHexagon(HexagonPiece.type.upkeep, 2, 0, 1, 2, _text.text);
                                break;
                            case "Support":
                                piece.SetUpHexagon(HexagonPiece.type.upkeep, 1, 1, 1, 2, _text.text);
                                break;
                            case "Updates":
                                piece.SetUpHexagon(HexagonPiece.type.upkeep, 2, 2, 2, 2, _text.text);
                                break;
                            case "Maintenance":
                                piece.SetUpHexagon(HexagonPiece.type.upkeep, 2, 1, 0, 3, _text.text);
                                break;
                            case "Feature Roadmap":
                                piece.SetUpHexagon(HexagonPiece.type.upkeep, 1, 2, 1, 2, _text.text);
                                break;
                        }
                        break;
                }
                GameManager.InfoPiece = piece;
            }
            else
            {
                Debug.LogError("Prefab does not contain a HexagonPiece component");
            }
        }
        else
        {
            GameManager.mainClusterM.RefreshRemainingTime();
        }

    }


}
