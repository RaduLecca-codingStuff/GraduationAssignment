using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MethodsGenerator : MonoBehaviour
{
    public GameObject Prefab;
    public GameObject ParentObject;
    public RectTransform ViewportRect;
    GameObject obj;
    public enum Type
    {
        Discover,
        Develop,
        Deliver,
        Upkeep,
    }
    public Type HexagonType;
    
    // Start is called before the first frame update
    void Awake()
    {
        if (Prefab.TryGetComponent<HexagonPiece>(out HexagonPiece h))
        {
            switch (HexagonType)
            {
                case Type.Discover:
                    for (int i = 0; i < 10; i++)
                    {
                        obj = GameObject.Instantiate(Prefab);
                        HexagonPiece pi = obj.GetComponent<HexagonPiece>();
                        pi.SetUpHexagon(HexagonPiece.type.discover, 1, 2, 3, "Text");
                        pi.GetComponentInChildren<Canvas>().worldCamera = Camera.main;
                        obj.transform.SetParent(ParentObject.transform);
                        
                        //obj.transform.localPosition = Vector3.zero;
                        //obj.transform.position += new Vector3((i + Prefab.transform.localScale.x) * 2 + 1, 0, -1);
                    }
                    break;
                case Type.Develop:
                    for (int i = 0; i < 10; i++)
                    {
                        obj = GameObject.Instantiate(Prefab);
                        HexagonPiece pi = obj.GetComponent<HexagonPiece>();
                        pi.SetUpHexagon(HexagonPiece.type.develop, 1, 2, 3, "Text");
                        pi.GetComponentInChildren<Canvas>().worldCamera = Camera.main;
                        obj.transform.SetParent(ParentObject.transform, true);
                        //obj.transform.localPosition = Vector3.zero;
                        //obj.transform.position += new Vector3((i + Prefab.transform.localScale.x) * 2 + 1, 0, -1);
                    }
                    break;
                case Type.Deliver:
                    for (int i = 0; i < 10; i++)
                    {
                        obj = GameObject.Instantiate(Prefab);
                        HexagonPiece pi = obj.GetComponent<HexagonPiece>();
                        pi.SetUpHexagon(HexagonPiece.type.deliver, 1, 2, 3, "Text");
                        pi.GetComponentInChildren<Canvas>().worldCamera = Camera.main;
                        obj.transform.SetParent(ParentObject.transform, true);
                        //obj.transform.localPosition = Vector3.zero;
                        //obj.transform.position += new Vector3((i + Prefab.transform.localScale.x) * 2 + 1, 0, -1);
                    }
                    break;
                case Type.Upkeep:
                    for (int i = 0; i < 10; i++)
                    {
                        obj = GameObject.Instantiate(Prefab);
                        HexagonPiece pi = obj.GetComponent<HexagonPiece>();
                        pi.SetUpHexagon(HexagonPiece.type.upkeep, 1, 2, 3, "Text");
                        pi.GetComponentInChildren<Canvas>().worldCamera = Camera.main;
                        obj.transform.SetParent(ParentObject.transform);
                        obj.transform.localScale = Vector2.one;
                        //obj.transform.localPosition = Vector3.zero;
                        //obj.transform.position += new Vector3((i +Prefab.transform.localScale.x) * 2 + 1, 0, -1);
                    }
                    break;
                default:
                    break;
            }
            //the width needs to be calculated better
            ViewportRect.offsetMax = new Vector3(ParentObject.transform.childCount*-50, ViewportRect.offsetMax.y);
        }
        else if (Prefab.TryGetComponent<PersonObject>(out PersonObject p))
        {

        }
        else if (Prefab.TryGetComponent<ResourceObject>(out ResourceObject r))
        {
            int i = 0;
            foreach (Resource res in GameManager.currentClient.ReturnResource())
            {
                obj = GameObject.Instantiate(Prefab);
                obj.GetComponent<ResourceObject>().SetResource(res);
                obj.transform.SetParent(ParentObject.transform, true);
                obj.transform.localPosition = Vector3.zero;
                obj.transform.position += new Vector3((i + Prefab.transform.localScale.x) * 2 + 1, 0, -1);
                i++;

            }
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
