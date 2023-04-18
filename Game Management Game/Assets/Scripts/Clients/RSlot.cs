using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RSlot : MonoBehaviour
{
    public enum Type
    {
        person,
        resource
    }
    public Type type;
    PersonObject _person;
    ResourceObject _resource;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AddResource(ResourceObject r)
    {
        _resource = r;
    }
    public void RemoveResource()
    {
        _resource=null;
    }
    public void AddPerson(PersonObject p)
    {
        _person = p;
    }
    public void RemovePerson()
    {
        _person=null;
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
