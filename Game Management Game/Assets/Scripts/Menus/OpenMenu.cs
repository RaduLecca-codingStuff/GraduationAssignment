using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class OpenMenu : MonoBehaviour
{
    [Header("Sprites")]
    public Button _button;
    public RawImage _menu;
    public int distance;
    public enum MenuType
    {
        up,
        down,
        left,
        right
    }
    ;
    public MenuType type;
    [Header("Will it open when the game starts?")]
    public bool OpenOnBegin=false;

    bool _open = false;
    bool _isCoroutineRunning = false;
    
    Vector3 _closedPos;
    Vector3 _openedPos;

    
    // Start is called before the first frame update
    void Awake()
    {
        _open = OpenOnBegin;
        _button.onClick.AddListener(()=>ButtonClick());
        _openedPos=transform.localPosition;
        switch (type)
        {
            case MenuType.up:
                _closedPos=transform.localPosition + new Vector3(0,distance);
                break;
            case MenuType.down:
                _closedPos = transform.localPosition + new Vector3(0, -distance);
                break;
            case MenuType.left:
                _closedPos = transform.localPosition + new Vector3( -distance,0);
                break;
            case MenuType.right:
                _closedPos = transform.localPosition + new Vector3(distance, 0);
                break;
        }
        _menu.color = _menu.color - new Color(0, 0, 0, 1);
        transform.localPosition = _closedPos;
        //need to fix the attempt at getting the renderers
        foreach (Image image in _menu.transform.GetComponentsInChildren<Image>())
        {
            image.color -= new Color(0, 0, 0, 1f);
        }
        foreach (SpriteRenderer sprite in _menu.transform.GetComponentsInChildren<SpriteRenderer>())
        {
            sprite.color -= new Color(0, 0, 0, 1f);
        }
        foreach (RawImage raw in _menu.transform.GetComponentsInChildren<RawImage>())
        {
            raw.color -= new Color(0, 0, 0, 1f);
        }
        foreach (Text text in _menu.transform.GetComponentsInChildren<Text>())
        {
            text.color -= new Color(0, 0, 0, 1f);
        }
        if (_open)
        {
            _isCoroutineRunning = true;
            StartCoroutine(OpenMenuCoroutine());
        }
    }
    void ButtonClick()
    {
        if(!_isCoroutineRunning)
        {
            if (!_open)
            {
                _open = true;
                _isCoroutineRunning= true;
                StartCoroutine(OpenMenuCoroutine());
            }
            else
            {
                _open = false;
                _isCoroutineRunning = true;
                StartCoroutine(CloseMenuCoroutine());
            }
        }
        
    }
    IEnumerator OpenMenuCoroutine()
    {
        while (transform.localPosition != _openedPos)
        {
            _menu.color+=new Color(0,0,0,.02f);
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, _openedPos,distance*.01f);
            
            foreach (Image image in _menu.transform.GetComponentsInChildren<Image>())
            {
                image.color += new Color(0, 0, 0, .02f);
            }
            foreach (RawImage raw in _menu.transform.GetComponentsInChildren<RawImage>())
            {
                raw.color += new Color(0, 0, 0, .02f);
            }
            foreach (SpriteRenderer sprite in _menu.transform.GetComponentsInChildren<SpriteRenderer>())
            {
                sprite.color += new Color(0, 0, 0, .02f);
            }
            foreach (Text text in _menu.transform.GetComponentsInChildren<Text>())
            {
                text.color += new Color(0, 0, 0, .02f);
            }
            yield return new WaitForSeconds(0.01f);
        }
        _isCoroutineRunning = false;
    }
    IEnumerator CloseMenuCoroutine()
    {
        while (transform.localPosition != _closedPos)
        {
            _menu.color -= new Color(0, 0, 0, .02f);
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, _closedPos, distance * .01f);
            
            foreach(Image image in _menu.transform.GetComponentsInChildren<Image>())
            {
                image.color -= new Color(0, 0, 0, .02f);
            }
            foreach (RawImage raw in _menu.transform.GetComponentsInChildren<RawImage>())
            {
                raw.color -= new Color(0, 0, 0, .02f);
            }
            foreach (SpriteRenderer sprite in _menu.transform.GetComponentsInChildren<SpriteRenderer>())
            {
                sprite.color-= new Color(0, 0, 0, .02f);
            }
            foreach (Text text in _menu.transform.GetComponentsInChildren<Text>())
            {
                text.color -= new Color(0, 0, 0, .02f);
            }
            yield return new WaitForSeconds(0.01f);
        } 
        _isCoroutineRunning = false;
    }

}
