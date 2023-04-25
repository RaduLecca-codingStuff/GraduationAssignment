using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PersonObject : MonoBehaviour
{
    Person _person;
    Image _img;
    public Sprite Stakeholder;
    public Sprite Sponsor;
    public Sprite EndUser;
    public Sprite UXdesigner;
    public Sprite ProjectManager;
    public Sprite PSO;
    public Sprite SME;
    public Sprite ProductOwner;
    public Sprite Programmer;
    public Sprite ExperienceExpert;
    public Sprite VisualDesigner;
    public Sprite Tester;

    RSlot _prevSlot;
    RSlot _newSlot;
    private void Awake()
    {
        _prevSlot = GetComponent<RSlot>();
        _newSlot = GetComponent<RSlot>();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var mousepos = GetMousePos();
            RaycastHit2D hit = Physics2D.Raycast(mousepos, Vector2.up, .1f);
            if (hit.collider.tag == "Person")
            {
                TakePerson(hit.transform.GetComponent<PersonObject>());
            }
            else if (hit.collider.tag == "Slot")
            {
                PlacePerson(hit.collider.transform);
            }
        }
    }

    Vector3 GetMousePos()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
    void TakePerson(PersonObject p)
    {
        _prevSlot.RemovePerson();
        _prevSlot = _newSlot;
       GameManager.currentPer = p;
    }

    void PlacePerson(Transform tr)
    {
        if (tr.TryGetComponent<RSlot>(out RSlot Sl))
        {
            _newSlot = Sl;
            if (_newSlot != _prevSlot)
            {
                Vector3 mouseP = GetMousePos();
                GameManager.currentPer.transform.position = new Vector3(tr.position.x, tr.position.y, GameManager.currentPer.transform.position.z);
                GameManager.currentPer.transform.parent = tr;
                Sl.AddPerson(GameManager.currentPer);
            }
            else
            {
                GameManager.currentPer.transform.position = new Vector3(_prevSlot.transform.position.x, _prevSlot.transform.position.y, GameManager.currentRes.transform.position.z);
                GameManager.currentPer.transform.parent = _prevSlot.transform;
            }
        }
    }
    public void SetPerson(Person p)
    {
        _img = GetComponent<Image>();
        _person = p;
        switch (_person.occupation)
        {
            case Person.Occupation.Stakeholder: break;
            case Person.Occupation.Sponsor: break;
            case Person.Occupation.EndUser: break;
            case Person.Occupation.UXdesigner: break;
            case Person.Occupation.ProjectManager: break;
            case Person.Occupation.PSO:break;
            case Person.Occupation.SME: break;
            case Person.Occupation.ProductOwner: break;
            case Person.Occupation.Programmer: break;
            case Person.Occupation.ExperienceExpert: break;
            case Person.Occupation.VisualDesigner: break;
            case Person.Occupation.Tester: break;
            default:
                break;
        }
    }

    public Person GetPerson() 
    {
        return _person;
    }

}
