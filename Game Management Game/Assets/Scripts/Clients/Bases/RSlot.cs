using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RSlot : MonoBehaviour
{
    HexagonPiece _parentHexagon;
    public enum Type
    {
        person,
        resource
    }
    public Type type;
    PersonObject _person;
    ResourceObject _resource;
    public bool isEmpty=true;
    [Header("Visual Indicator")]
    public Image Indicator;
    BoxCollider2D _boxCollider;
    // Start is called before the first frame update
    void Start()
    {
        Indicator.gameObject.SetActive(false);
        _parentHexagon =transform.parent.parent.GetComponentInParent<HexagonPiece>();
        _boxCollider = GetComponent<BoxCollider2D>();
    }

    public void AddResource(ResourceObject r)
    {
        _resource = r;
        _parentHexagon.SetResource(_resource.GetResource());
       
            GameManager.mainClusterM.RefreshValues();
        
        Indicator.gameObject.SetActive(true);
    }

    public void RemoveResource()
    {
        _resource=null;
        _parentHexagon.SetResource(_resource.GetResource());
        
            GameManager.mainClusterM.RefreshValues();
        
        Indicator.gameObject.SetActive(false);
        
    }
    public void AddPerson(PersonObject p)
    {
        _person = p;
        _parentHexagon.SetPerson(_person.GetPerson());
        
            GameManager.mainClusterM.RefreshValues();
        
        Indicator.gameObject.SetActive(true);
    }
    public void RemovePerson()
    {
        _person=null;
        _parentHexagon.SetPerson(_person.GetPerson());

            GameManager.mainClusterM.RefreshValues();
        
        Indicator.gameObject.SetActive(false);
        
    }
    public void TrySendResources()
    {
        if(transform.parent.TryGetComponent<HexagonPiece>(out HexagonPiece hxp))
        {
            if(_resource == null)
            {
                hxp.SetResource(null);
            }
            else
            {
                hxp.SetResource(_resource.GetResource());
            }
            if (_person == null)
            {
                hxp.SetPerson(null);
            }
            else
            {
                hxp.SetPerson(_person.GetPerson());
            }
        }
    }

}
