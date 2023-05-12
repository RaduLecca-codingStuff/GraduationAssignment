using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceObject : MonoBehaviour
{
    [Header("Possible Sprites")]
    public Sprite v1;
    public Sprite v2;
    public Sprite v3;
    [Header("Audio clips")]
    public AudioClip takeAudio;
    public AudioClip placeAudio;

    RSlot _prevSlot;
    RSlot _newSlot;
    Resource _resource;
    Image _img;
    AudioSource _audioSource;
    public ResourceObject(Resource r)
    {
        _resource = r;
    }
    private void Awake()
    {
        _img = GetComponent<Image>();
        if (_resource == null)
            _resource = new Resource(3);
        switch (_resource.getValue())
        {
            case 1:
                _img.sprite=v1;
                break;
            case 2:
                _img.sprite=v2;
                break;
            case 3:
                _img.sprite=v3;
                break;
            default:
                break;
        }
        _prevSlot = GetComponent<RSlot>();
        _newSlot = GetComponent<RSlot>();    
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var mousepos = GetMousePos();
            RaycastHit2D hit = Physics2D.Raycast(mousepos, Vector2.up, .1f, 1 << 5);
            if (hit.collider.tag == "Resource")
            {
                TakeResource(hit.collider.transform.GetComponent<ResourceObject>());
            }
            else if (hit.collider.tag == "Slot")
            {
                PlaceResource(hit.collider.transform);
            }
        } 
    }
    Vector3 GetMousePos()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
    private void TakeResource(ResourceObject r)
    {
        _prevSlot = _newSlot;
        GameManager.currentRes=r;
        GameManager.currentRes._img.color = new Color(1, 1, 1, .5f);
    }
    private void PlaceResource(Transform tr)
    {
        GameManager.currentRes._img.color = new Color(1, 1, 1, 1);
        if (tr.TryGetComponent<RSlot>( out RSlot Sl))
        {
            _newSlot = Sl;
            if (_newSlot != _prevSlot && _newSlot.type == RSlot.Type.resource)
            {
                Vector3 mouseP = GetMousePos();
                GameManager.currentRes.transform.position = new Vector3(tr.position.x, tr.position.y, GameManager.currentRes.transform.position.z);
                GameManager.currentRes.transform.parent = tr;
                Sl.AddResource(GameManager.currentRes);
                if (_prevSlot == null)
                {
                    Debug.Log("OOF");
                }
                else
                    _prevSlot.RemoveResource();
            }
            else
            {
                if (_prevSlot == null)
                {
                    Debug.Log("OOF");
                }
                else
                {
                    GameManager.currentRes.transform.position = new Vector3(_prevSlot.transform.position.x, _prevSlot.transform.position.y, GameManager.currentRes.transform.position.z);
                    GameManager.currentRes.transform.parent = _prevSlot.transform;
                }
                
            }
        }
    }

    public Resource GetResource()
    {
        return _resource;
    }
    public void SetResource(Resource resource)
    {
        _resource = resource;
        switch (_resource.getValue())
        {
            case 1:
                _img.sprite = v1;
                break;
            case 2:
                _img.sprite = v2;
                break;
            case 3:
                _img.sprite = v3;
                break;
            default:
                break;
        }
    }

}
