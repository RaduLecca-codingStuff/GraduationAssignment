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
    bool _selected=false;
    Vector3Int _prevTile;
    AudioSource _audioSource;

    // Start is called before the first frame update
    void Start()
    {
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

            if (Input.GetMouseButtonDown(0))
            {
                PlaceHexagon();
            }
        }

    }

    Vector3 GetMousePos()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
    void PlaceHexagon()
    {

        Vector3Int cellPoint = _tiles.WorldToCell(GetMousePos());
        Vector3 mouseP = _tiles.CellToWorld(cellPoint);
        if (HexagonPrefab.TryGetComponent<HexagonPiece>(out HexagonPiece piece))
        {
            GameObject g = Instantiate(HexagonPrefab);
            g.transform.position = new Vector3(mouseP.x, mouseP.y, 0);

            //where the 3 values are added + where stuff is set up
            switch (Type)
            {
                case type.discover:

                    switch (_text.text)
                    {
                        case "KPI":
                            g.GetComponent<HexagonPiece>().SetUpHexagon(HexagonPiece.type.discover, 3, 3, 3, _text.text);
                            break;
                        case "Emphatise":
                            g.GetComponent<HexagonPiece>().SetUpHexagon(HexagonPiece.type.discover, 1, 3, 4, _text.text);
                            break;
                        case "Risk Assesment":
                            g.GetComponent<HexagonPiece>().SetUpHexagon(HexagonPiece.type.discover, 1, 4, 1, _text.text);
                            break;
                        case "Insight":
                            g.GetComponent<HexagonPiece>().SetUpHexagon(HexagonPiece.type.discover, 3, 1, 3, _text.text);
                            break;
                        case "Requirements":
                            g.GetComponent<HexagonPiece>().SetUpHexagon(HexagonPiece.type.discover, 2, 4, 2, _text.text);
                            break;
                        case "Research":
                            g.GetComponent<HexagonPiece>().SetUpHexagon(HexagonPiece.type.discover, 3, 3, 3, _text.text);
                            break;
                        case "Analyse":
                            g.GetComponent<HexagonPiece>().SetUpHexagon(HexagonPiece.type.discover, 3, 3, 3, _text.text);
                            break;
                        case "Initiation":
                            g.GetComponent<HexagonPiece>().SetUpHexagon(HexagonPiece.type.discover, 1, 1, 2, _text.text);
                            break;
                        case "Awareness":
                            g.GetComponent<HexagonPiece>().SetUpHexagon(HexagonPiece.type.discover, 4, 2, 1, _text.text);
                            break;
                        case "Design Space Analysis":
                            g.GetComponent<HexagonPiece>().SetUpHexagon(HexagonPiece.type.discover, 2, 5, 4, _text.text);
                            break;
                        case "Player Investigation":
                            g.GetComponent<HexagonPiece>().SetUpHexagon(HexagonPiece.type.discover, 5, 4, 3, _text.text);
                            break;
                        case "Journey Investigation":
                            g.GetComponent<HexagonPiece>().SetUpHexagon(HexagonPiece.type.discover, 1, 1, 4, _text.text);
                            break;
                        case "Game Storm":
                            g.GetComponent<HexagonPiece>().SetUpHexagon(HexagonPiece.type.discover, 4, 4, 5, _text.text);
                            break;
                        case "Subject Matter Expert Investigation":
                            g.GetComponent<HexagonPiece>().SetUpHexagon(HexagonPiece.type.discover, 2, 5, 4, _text.text);
                            break;
                        case "Game Concept":
                            g.GetComponent<HexagonPiece>().SetUpHexagon(HexagonPiece.type.discover, 5, 5, 5, _text.text);
                            break;
                        case "Identify Game Loop":
                            g.GetComponent<HexagonPiece>().SetUpHexagon(HexagonPiece.type.discover, 5, 3, 5, _text.text);
                            break;
                        case "Feature Roadmap":
                            g.GetComponent<HexagonPiece>().SetUpHexagon(HexagonPiece.type.discover, 3, 5, 3, _text.text);
                            break;
                        default: break;
                    }
                    break;
                case type.develop:
                    switch (_text.text)
                    {
                        case "Action":
                            g.GetComponent<HexagonPiece>().SetUpHexagon(HexagonPiece.type.develop, 3, 1, 2, _text.text);
                            break;
                        case "Minimum Viable Product":
                            g.GetComponent<HexagonPiece>().SetUpHexagon(HexagonPiece.type.develop, 3, 4, 1, _text.text);
                            break;
                        case "Minimum Usable Product":
                            g.GetComponent<HexagonPiece>().SetUpHexagon(HexagonPiece.type.develop, 3, 4, 2, _text.text);
                            break;
                        case "Solution":
                            g.GetComponent<HexagonPiece>().SetUpHexagon(HexagonPiece.type.develop, 3, 3, 3, _text.text);
                            break;
                        case "Prototyping":
                            g.GetComponent<HexagonPiece>().SetUpHexagon(HexagonPiece.type.develop, 3, 3, 3, _text.text);
                            break;
                        case "Proof of Concept":
                            g.GetComponent<HexagonPiece>().SetUpHexagon(HexagonPiece.type.develop, 4, 3, 4, _text.text);
                            break;
                        case "Execution":
                            g.GetComponent<HexagonPiece>().SetUpHexagon(HexagonPiece.type.develop, 1, 3, 3, _text.text);
                            break;
                        case "Fabrication":
                            g.GetComponent<HexagonPiece>().SetUpHexagon(HexagonPiece.type.develop, 1, 1, 1, _text.text);
                            break;
                        case "Concept":
                            g.GetComponent<HexagonPiece>().SetUpHexagon(HexagonPiece.type.develop, 2, 1, 3, _text.text);
                            break;
                        case "Playtest":
                            g.GetComponent<HexagonPiece>().SetUpHexagon(HexagonPiece.type.develop, 5, 4, 1, _text.text);
                            break;
                        case "Iterate":
                            g.GetComponent<HexagonPiece>().SetUpHexagon(HexagonPiece.type.develop, 3, 3, 2, _text.text);
                            break;
                        case "Elaboration":
                            g.GetComponent<HexagonPiece>().SetUpHexagon(HexagonPiece.type.develop, 1, 4, 3, _text.text);
                            break;
                        case "Ideation":
                            g.GetComponent<HexagonPiece>().SetUpHexagon(HexagonPiece.type.develop, 3, 1, 1, _text.text);
                            break;
                        case "Design":
                            g.GetComponent<HexagonPiece>().SetUpHexagon(HexagonPiece.type.develop, 2, 4, 1, _text.text);
                            break;
                        case "Define":
                            g.GetComponent<HexagonPiece>().SetUpHexagon(HexagonPiece.type.develop, 1, 4, 2, _text.text);
                            break;
                        case "Design Documentation":
                            g.GetComponent<HexagonPiece>().SetUpHexagon(HexagonPiece.type.develop, 3, 5, 4, _text.text);
                            break;
                        case "Wireframes":
                            g.GetComponent<HexagonPiece>().SetUpHexagon(HexagonPiece.type.develop, 4, 4, 5, _text.text);
                            break;
                        case "Paper Prototype":
                            g.GetComponent<HexagonPiece>().SetUpHexagon(HexagonPiece.type.develop, 3, 5, 5, _text.text);
                            break;
                        case "Click-thru":
                            g.GetComponent<HexagonPiece>().SetUpHexagon(HexagonPiece.type.develop, 3, 5, 5, _text.text);
                            break;
                        case "Feature Roadmap":
                            g.GetComponent<HexagonPiece>().SetUpHexagon(HexagonPiece.type.develop, 3, 5, 3, _text.text);
                            break;
                        case "Design Elaborations":
                            g.GetComponent<HexagonPiece>().SetUpHexagon(HexagonPiece.type.develop, 2, 5, 2, _text.text);
                            break;
                    }
                    break;
                case type.deliver:
                    switch (_text.text)
                    {
                        case "Playtest":
                            g.GetComponent<HexagonPiece>().SetUpHexagon(HexagonPiece.type.deliver, 3, 5, 4, _text.text);
                            break;
                        case "Validate":
                            g.GetComponent<HexagonPiece>().SetUpHexagon(HexagonPiece.type.deliver, 2, 5, 2, _text.text);
                            break;
                        case "Launch":
                            g.GetComponent<HexagonPiece>().SetUpHexagon(HexagonPiece.type.deliver, 1, 1, 4, _text.text);
                            break;
                        case "Design Tuning":
                            g.GetComponent<HexagonPiece>().SetUpHexagon(HexagonPiece.type.deliver, 5, 3, 3, _text.text);
                            break;
                        case "Implementation":
                            g.GetComponent<HexagonPiece>().SetUpHexagon(HexagonPiece.type.deliver, 3, 2, 5, _text.text);
                            break;
                        case "Closing":
                            g.GetComponent<HexagonPiece>().SetUpHexagon(HexagonPiece.type.deliver, 1, 3, 1, _text.text);
                            break;
                        case "Business Model":
                            g.GetComponent<HexagonPiece>().SetUpHexagon(HexagonPiece.type.deliver, 4, 4, 2, _text.text);
                            break;
                        case "Production":
                            g.GetComponent<HexagonPiece>().SetUpHexagon(HexagonPiece.type.deliver, 1, 3, 1, _text.text);
                            break;
                        case "User Stories":
                            g.GetComponent<HexagonPiece>().SetUpHexagon(HexagonPiece.type.deliver, 5, 4, 3, _text.text);
                            break;
                        case "Minimum Usable Product":
                            g.GetComponent<HexagonPiece>().SetUpHexagon(HexagonPiece.type.deliver, 2, 4, 2, _text.text);
                            break;
                        case "Minimum Viable Product":
                            g.GetComponent<HexagonPiece>().SetUpHexagon(HexagonPiece.type.deliver, 2, 4, 1, _text.text);
                            break;
                        case "Feature Roadmap":
                            g.GetComponent<HexagonPiece>().SetUpHexagon(HexagonPiece.type.deliver, 3, 5, 3, _text.text);
                            break;

                    }
                    break;
                case type.upkeep:
                    switch (_text.text)
                    {
                        case "Monetise":
                            g.GetComponent<HexagonPiece>().SetUpHexagon(HexagonPiece.type.upkeep, 2, 5, 2, _text.text);
                            break;
                        case "Upscale":
                            g.GetComponent<HexagonPiece>().SetUpHexagon(HexagonPiece.type.upkeep, 5, 2, 3, _text.text);
                            break;
                        case "Support":
                            g.GetComponent<HexagonPiece>().SetUpHexagon(HexagonPiece.type.upkeep, 4, 4, 4, _text.text);
                            break;
                        case "Updates":
                            g.GetComponent<HexagonPiece>().SetUpHexagon(HexagonPiece.type.upkeep, 5, 5, 5, _text.text);
                            break;
                        case "Maintenance":
                            g.GetComponent<HexagonPiece>().SetUpHexagon(HexagonPiece.type.upkeep, 5, 4, 2, _text.text);
                            break;
                        case "Feature Roadmap":
                            g.GetComponent<HexagonPiece>().SetUpHexagon(HexagonPiece.type.upkeep, 3, 5, 3, _text.text);
                            break;
                    }
                    break;
            }
            _renderer.color = new Color(_renderer.color.r, _renderer.color.g, _renderer.color.b, 1);
            this.gameObject.SetActive(false);
        }
        else
        {
            Debug.LogError("Prefab does not contain a HexagonPiece component");
        }
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        _selected = true;
        _audioSource.Play();


    }
}
