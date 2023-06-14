using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeMeter : MonoBehaviour
{
    RawImage _bar;
    float _baseLength;
    [SerializeField]
    Gradient _gradient;
    // Start is called before the first frame update
    void Start()
    {
        _bar = transform.Find("Level").GetComponent<RawImage>();
        _baseLength = _bar.rectTransform.localScale.x;
        _bar.transform.localScale = new Vector3((GameManager.currentTimeLeft * _baseLength) / 100, _bar.transform.localScale.y, 1);
    }

    // Update is called once per frame
    void Update()
    {
        _bar.transform.localScale = new Vector3((GameManager.currentTimeLeft * _baseLength) / 100, _bar.transform.localScale.y, 1);
        _bar.color = _gradient.Evaluate(_bar.transform.localScale.x);
        
    }
}
