using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class OpenMenu : MonoBehaviour
{
    public Button _button;
    public RawImage _menu;
    bool _open = false;
    bool _isCoroutineRunning = false;
    public int distance;
    Vector3 _closedPos;
    Vector3 _openedPos;

    public enum MenuType 
    { 
        up,
        down,
        left,
        right
    }
    ;
    public MenuType type;
    // Start is called before the first frame update
    void Awake()
    {
        _button.onClick.AddListener(()=>ButtonClick());
        _openedPos=transform.position;
        switch (type)
        {
            case MenuType.up:
                _closedPos=transform.position+new Vector3(0,distance);
                break;
            case MenuType.down:
                _closedPos = transform.position + new Vector3(0, -distance);
                break;
            case MenuType.left:
                _closedPos = transform.position + new Vector3( distance,0);
                break;
            case MenuType.right:
                _closedPos = transform.position + new Vector3(distance, 0);
                break;
        }
        _menu.color = _menu.color - new Color(0, 0, 0, 1);
        transform.position = _closedPos;
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
        while (transform.position !=_openedPos)
        {
            _menu.color+=new Color(0,0,0,.02f);
            transform.position=Vector3.MoveTowards(transform.position,_openedPos,distance*.01f);
            
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
        while (transform.position != _closedPos)
        {
            _menu.color -= new Color(0, 0, 0, .02f);
            transform.position = Vector3.MoveTowards(transform.position, _closedPos, distance * .01f);
            
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
