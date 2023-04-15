using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceObject : MonoBehaviour
{
    Resource _resource;
    Sprite _sprite;
    public Sprite v1;
    public Sprite v2;
    public Sprite v3;

    public ResourceObject(Resource r)
    {
        _resource = r;
        
    }

    private void Awake()
    {
        _sprite = GetComponent<Sprite>();
        if (_resource == null)
            _resource = new Resource(3);
        switch (_resource.getValue())
        {
            case 1:
                _sprite=v1;
                break;
            case 2:
                _sprite=v2;
                break;
            case 3:
                _sprite=v3;
                break;
            default:
                break;
        }
    }

    private void Update()
    {
        var mousepos = GetMousePos();
        
        RaycastHit2D hit = Physics2D.Raycast(mousepos, Vector2.up, .1f);
        
        if (Input.GetMouseButtonDown(0))
        {
            if (hit.collider.tag == "Resource")
            {
                TakeResource(hit.collider.transform.GetComponent<ResourceObject>());
            }
            else if(hit.collider.tag=="Slot")
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
        GameManager.currentRes=r;
        Debug.Log("got the piece");
    }
    private void PlaceResource(Transform tr)
    {
        if(tr.TryGetComponent<RSlot>( out RSlot Sl))
        {
            Vector3 mouseP = GetMousePos();
            GameManager.currentRes.transform.position = new Vector3(tr.position.x, tr.position.y, GameManager.currentRes.transform.position.z);
            GameManager.currentRes.transform.parent = tr;
            Debug.Log("set the piece");
        }
        
        
    }

    public Resource GetResource()
    {
        return _resource;
    }
    public void SetResource(Resource resource)
    {
        _resource = resource;
    }
}
