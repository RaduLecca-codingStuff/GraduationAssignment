using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MethodsGenerator : MonoBehaviour
{
    public GameObject MethodPrefab;
    public GameObject ParentObject;
    public RectTransform ViewportRect;
    GameObject obj;
    public enum Type
    {
        Discover,
        Develop,
        Deliver,
        Upkeep,
        Persons,
        Resources
    }
    public Type type;
    
    // Start is called before the first frame update
    void Awake()
    {
        if (MethodPrefab.TryGetComponent<HexagonPiece>(out HexagonPiece p))
        {
            switch (type)
            {
                case Type.Discover:
                    for (int i = 0; i < 10; i++)
                    {
                        obj = GameObject.Instantiate(MethodPrefab);
                        HexagonPiece pi = obj.GetComponent<HexagonPiece>();
                        pi.SetUpHexagon(HexagonPiece.type.discover, 1, 2, 3, "Text");
                        pi.GetComponentInChildren<Canvas>().worldCamera = Camera.main;
                        obj.transform.SetParent(ParentObject.transform, true);
                        obj.transform.localPosition = Vector3.zero;
                        obj.transform.position += new Vector3((i + MethodPrefab.transform.localScale.x) * 2 + 1, 0, -1);
                    }
                    break;
                case Type.Develop:
                    for (int i = 0; i < 10; i++)
                    {
                        obj = GameObject.Instantiate(MethodPrefab);
                        HexagonPiece pi = obj.GetComponent<HexagonPiece>();
                        pi.SetUpHexagon(HexagonPiece.type.develop, 1, 2, 3, "Text");
                        pi.GetComponentInChildren<Canvas>().worldCamera = Camera.main;
                        obj.transform.SetParent(ParentObject.transform, true);
                        obj.transform.localPosition = Vector3.zero;
                        obj.transform.position += new Vector3((i + MethodPrefab.transform.localScale.x) * 2 + 1, 0, -1);
                    }
                    break;
                case Type.Deliver:
                    for (int i = 0; i < 10; i++)
                    {
                        obj = GameObject.Instantiate(MethodPrefab);
                        HexagonPiece pi = obj.GetComponent<HexagonPiece>();
                        pi.SetUpHexagon(HexagonPiece.type.deliver, 1, 2, 3, "Text");
                        pi.GetComponentInChildren<Canvas>().worldCamera = Camera.main;
                        obj.transform.SetParent(ParentObject.transform, true);
                        obj.transform.localPosition = Vector3.zero;
                        obj.transform.position += new Vector3((i + MethodPrefab.transform.localScale.x) * 2 + 1, 0, -1);
                    }
                    break;
                case Type.Upkeep:
                    for (int i = 0; i < 10; i++)
                    {
                        obj = GameObject.Instantiate(MethodPrefab);
                        HexagonPiece pi = obj.GetComponent<HexagonPiece>();
                        pi.SetUpHexagon(HexagonPiece.type.upkeep, 1, 2, 3, "Text");
                        pi.GetComponentInChildren<Canvas>().worldCamera = Camera.main;
                        obj.transform.SetParent(ParentObject.transform, true);
                        obj.transform.localPosition = Vector3.zero;
                        obj.transform.position += new Vector3((i + MethodPrefab.transform.localScale.x) * 2 + 1, 0, -1);
                    }
                    break;
                case Type.Persons:
                    break;
                case Type.Resources:
                    break;
                default:
                    break;
            }
            //the width needs to be calculated better
            ViewportRect.offsetMax = new Vector3(ParentObject.transform.childCount*-50, ViewportRect.offsetMax.y);
        }
        else
            Debug.LogError("Methods Generator requires a methodprefab which contains a functional HexagonPiece component");
    }
    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
