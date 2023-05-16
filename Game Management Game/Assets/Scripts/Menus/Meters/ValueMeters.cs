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
        _bar =transform.Find("Level").GetComponent<RawImage>();
        _baseLength = _bar.rectTransform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        switch (type)
        {
            case Type.purpose:
                _bar.transform.localScale = new Vector3((GameManager.purpose *_baseLength) /100, _bar.transform.localScale.y,1);
                if(GameManager.purpose>=GameManager.currentClient.reqPurpose)
                {
                    _bar.color = Color.green;
                }
                else
                    _bar.color = Color.white;
                break;
            case Type.sustainability:
                _bar.transform.localScale = new Vector3((GameManager.sustainability  * _baseLength) / 100, _bar.transform.localScale.y,1);
                if (GameManager.sustainability >= GameManager.currentClient.reqSustainability)
                {
                    _bar.color = Color.green;
                }
                else
                    _bar.color = Color.white;
                break;
            case Type.experience:
                _bar.transform.localScale = new Vector3((GameManager.experience  * _baseLength) / 100, _bar.transform.localScale.y, 1);
                if (GameManager.experience >= GameManager.currentClient.reqExperience)
                {
                    _bar.color = Color.green;
                }
                else
                    _bar.color = Color.white;
                break;
        }
    }
}
