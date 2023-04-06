using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MethodsGenerator : MonoBehaviour
{
    public GameObject MethodPrefab;
    public GameObject ParentObject;
    public RectTransform ViewportRect;
    GameObject obj;
    
    // Start is called before the first frame update
    void Awake()
    {
        if (MethodPrefab.TryGetComponent<HexagonPiece>(out HexagonPiece p))
        {
            for(int i = 0; i < 10; i++)
            {
                obj = GameObject.Instantiate(MethodPrefab);
                obj.GetComponent<HexagonPiece>().SetUpImpact(1,2,3);
                obj.transform.SetParent(ParentObject.transform,true);
                obj.transform.localPosition = Vector3.zero;
                obj.transform.position += new Vector3((i+MethodPrefab.transform.localScale.x)*2+1,0,-1);
            }
            //the width needs to be calculated better
            ViewportRect.offsetMax = new Vector3(ParentObject.transform.childCount*-50, ViewportRect.offsetMax.y);
        }
        else
            Debug.LogError("Methods Generator requires a methodprefab which contains a functional HexagonPiece component");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
