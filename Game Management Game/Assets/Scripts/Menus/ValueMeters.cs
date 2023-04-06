using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ValueMeters : MonoBehaviour
{
    RawImage _bar;
    
    public enum Type
    {
        purpose,
        sustainability,
        experience,
    }
    public Type type;
    float _baseLength;
    // Start is called before the first frame update
    void Start()
    {
        _bar=transform.Find("Level").GetComponent<RawImage>();
        _baseLength=_bar.rectTransform.rect.width;
    }

    // Update is called once per frame
    void Update()
    {
        switch (type)
        {
            case Type.purpose:
                _bar.transform.localScale = new Vector3(GameManager.purpose * .01f, _bar.transform.localScale.y,1);
                break;
            case Type.sustainability:
                _bar.transform.localScale = new Vector3(GameManager.sustainability * .01f, _bar.transform.localScale.y,1);
                break;
            case Type.experience:
                _bar.transform.localScale = new Vector3(GameManager.experience * .01f, _bar.transform.localScale.y, 1);
                break;
        }
    }
}
