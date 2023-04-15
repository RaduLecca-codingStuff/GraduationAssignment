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


    // Start is called before the first frame update
    void Start()
    {
        _tiles = GameObject.FindGameObjectWithTag("Grid").GetComponentInChildren<Tilemap>();
        _renderer = GetComponent<Image>();
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
    
    Vector3 GetMousePos()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
    void PlaceHexagon()
    {
        
        Vector3Int cellPoint = _tiles.WorldToCell(GetMousePos());
        Vector3 mouseP = _tiles.CellToWorld(cellPoint);
        if(HexagonPrefab.TryGetComponent<HexagonPiece>(out HexagonPiece piece))
        {
            GameObject g = Instantiate(HexagonPrefab);
            g.transform.position = new Vector3(mouseP.x, mouseP.y,0);

            //where the 3 values are added + where stuff is set up
            switch (Type)
            {
                case type.discover:
                    
                    switch (_text.text)
                    {
                        case "KPI":
                            g.GetComponent<HexagonPiece>().SetUpHexagon(HexagonPiece.type.discover,1,1,1,_text.text);
                            break;
                        case "Emphatize":
                            g.GetComponent<HexagonPiece>().SetUpHexagon(HexagonPiece.type.discover, 1, 1, 1, _text.text);
                            break;
                        case "Risk Assesment":
                            g.GetComponent<HexagonPiece>().SetUpHexagon(HexagonPiece.type.discover, 1, 1, 1, _text.text);
                            break;
                        case "Insight":
                            g.GetComponent<HexagonPiece>().SetUpHexagon(HexagonPiece.type.discover, 1, 1, 1, _text.text);
                            break;
                        case "Requirements":
                            g.GetComponent<HexagonPiece>().SetUpHexagon(HexagonPiece.type.discover, 1, 1, 1, _text.text);
                            break;
                        case "Research":
                            g.GetComponent<HexagonPiece>().SetUpHexagon(HexagonPiece.type.discover, 1, 1, 1, _text.text);
                            break;
                        case "Analyze":
                            g.GetComponent<HexagonPiece>().SetUpHexagon(HexagonPiece.type.discover, 1, 1, 1, _text.text);
                            break;
                        case "Initiation":
                            g.GetComponent<HexagonPiece>().SetUpHexagon(HexagonPiece.type.discover, 1, 1, 1, _text.text);
                            break;
                        case "Awareness":
                            g.GetComponent<HexagonPiece>().SetUpHexagon(HexagonPiece.type.discover, 1, 1, 1, _text.text);
                            break;
                        case "Design Space":
                            g.GetComponent<HexagonPiece>().SetUpHexagon(HexagonPiece.type.discover, 1, 1, 1, _text.text);
                            break;
                        default: break;
                    }
                    break;
                case type.develop:
                    switch (_text.text)
                    {
                        case "Action":
                            g.GetComponent<HexagonPiece>().SetUpHexagon(HexagonPiece.type.develop, 1, 1, 1, _text.text);
                            break;
                        case "Minimum Viable Product":
                            g.GetComponent<HexagonPiece>().SetUpHexagon(HexagonPiece.type.develop, 1, 1, 1, _text.text);
                            break;
                        case "Minimum Usable Product":
                            g.GetComponent<HexagonPiece>().SetUpHexagon(HexagonPiece.type.develop, 1, 1, 1, _text.text);
                            break;
                        case "Solution":
                            g.GetComponent<HexagonPiece>().SetUpHexagon(HexagonPiece.type.develop, 1, 1, 1, _text.text);
                            break;
                        case "Prototyping":
                            g.GetComponent<HexagonPiece>().SetUpHexagon(HexagonPiece.type.develop, 1, 1, 1, _text.text);
                            break;
                        case "Proof of Concept":
                            g.GetComponent<HexagonPiece>().SetUpHexagon(HexagonPiece.type.develop, 1, 1, 1, _text.text);
                            break;
                        case "Execution":
                            g.GetComponent<HexagonPiece>().SetUpHexagon(HexagonPiece.type.develop, 1, 1, 1, _text.text);
                            break;
                        case "Fabrication":
                            g.GetComponent<HexagonPiece>().SetUpHexagon(HexagonPiece.type.develop, 1, 1, 1, _text.text);
                            break;
                        case "Concept":
                            g.GetComponent<HexagonPiece>().SetUpHexagon(HexagonPiece.type.develop, 1, 1, 1, _text.text);
                            break;
                        case "Test":
                            g.GetComponent<HexagonPiece>().SetUpHexagon(HexagonPiece.type.develop, 1, 1, 1, _text.text);
                            break;
                        case "Iterate":
                            g.GetComponent<HexagonPiece>().SetUpHexagon(HexagonPiece.type.develop, 1, 1, 1, _text.text);
                            break;
                        case "Elaboration":
                            g.GetComponent<HexagonPiece>().SetUpHexagon(HexagonPiece.type.develop, 1, 1, 1, _text.text);
                            break;
                        case "Ideation":
                            g.GetComponent<HexagonPiece>().SetUpHexagon(HexagonPiece.type.develop, 1, 1, 1, _text.text);
                            break;
                        case "Design":
                            g.GetComponent<HexagonPiece>().SetUpHexagon(HexagonPiece.type.develop, 1, 1, 1, _text.text);
                            break;
                        case "Define":
                            g.GetComponent<HexagonPiece>().SetUpHexagon(HexagonPiece.type.develop, 1, 1, 1, _text.text);
                            break;

                    }
                    break;
                case type.deliver:
                    switch (_text.text)
                    {
                        case "Test":
                            g.GetComponent<HexagonPiece>().SetUpHexagon(HexagonPiece.type.deliver, 1, 1, 1, _text.text);
                            break;
                        case "Validate":
                            g.GetComponent<HexagonPiece>().SetUpHexagon(HexagonPiece.type.deliver, 1, 1, 1, _text.text);
                            break;
                        case "Launch":
                            g.GetComponent<HexagonPiece>().SetUpHexagon(HexagonPiece.type.deliver, 1, 1, 1, _text.text);
                            break;
                        case "Fine-Tuning":
                            g.GetComponent<HexagonPiece>().SetUpHexagon(HexagonPiece.type.deliver, 1, 1, 1, _text.text);
                            break;
                        case "Implementation":
                            g.GetComponent<HexagonPiece>().SetUpHexagon(HexagonPiece.type.deliver, 1, 1, 1, _text.text);
                            break;
                        case "Closing":
                            g.GetComponent<HexagonPiece>().SetUpHexagon(HexagonPiece.type.deliver, 1, 1, 1, _text.text);
                            break;
                        case "Business Model":
                            g.GetComponent<HexagonPiece>().SetUpHexagon(HexagonPiece.type.deliver, 1, 1, 1, _text.text);
                            break;
                        case "Production":
                            g.GetComponent<HexagonPiece>().SetUpHexagon(HexagonPiece.type.deliver, 1, 1, 1, _text.text);
                            break;
                    }
                    break;
                case type.upkeep:
                    switch (_text.text)
                    {
                        case "Monetize":
                            g.GetComponent<HexagonPiece>().SetUpHexagon(HexagonPiece.type.upkeep, 1, 1, 1, _text.text);
                            break;
                        case "Upscale":
                            g.GetComponent<HexagonPiece>().SetUpHexagon(HexagonPiece.type.upkeep, 1, 1, 1, _text.text);
                            break;
                        case "Support":
                            g.GetComponent<HexagonPiece>().SetUpHexagon(HexagonPiece.type.upkeep, 1, 1, 1, _text.text);
                            break;
                        case "Updates":
                            g.GetComponent<HexagonPiece>().SetUpHexagon(HexagonPiece.type.upkeep, 1, 1, 1, _text.text);
                            break;
                        case "Maintenance":
                            g.GetComponent<HexagonPiece>().SetUpHexagon(HexagonPiece.type.upkeep, 1, 1, 1, _text.text);
                            break;
                    }
                    break;
            }

            this.gameObject.SetActive(false);
        }
        else
        {
            Debug.LogError("Prefab does not contain a HexagonPiece component");
        }
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

    public void OnPointerClick(PointerEventData eventData)
    {
        _selected = true;
    }
}
