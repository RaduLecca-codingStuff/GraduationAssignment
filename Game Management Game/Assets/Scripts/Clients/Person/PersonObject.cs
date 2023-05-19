using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PersonObject : MonoBehaviour
{
    [Header("Possible Sprites")]
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

    /*
    [Header("Audio clips")]
    public AudioClip takeAudio;
    public AudioClip placeAudio;
    AudioSource _audioSource;

    RSlot _prevSlot;
    RSlot _newSlot;
    */

    Person _person;
    Image _img;
    
    bool _isSelected = false;
    private void Awake()
    {
        /*
        _prevSlot = GetComponent<RSlot>();
        _newSlot = GetComponent<RSlot>();
        */
        _img = GetComponent<Image>();
    }
    private void Update()
    {
        /*
        if (Input.GetMouseButtonDown(0))
        {
            var mousepos = GetMousePos();
            RaycastHit2D hit = Physics2D.Raycast(mousepos, Vector2.up, .1f, 1 << 5);
            if (hit.collider && hit.collider.tag == "Slot")
            {
                PlacePerson(hit.collider.transform);
            }
        }
        */
    }
    /*
    Vector3 GetMousePos()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
    void TakePerson(PersonObject p)
    {
        _prevSlot = _newSlot;
        if (GameManager.currentPer)
        {
            GameManager.currentPer._isSelected = false;
            GameManager.currentPer._img.color = new Color(1, 1, 1, 1);
        }
        
        GameManager.currentPer = p;
        GameManager.currentPer._img.color = new Color(1, 1, 1, .5f);
        GameManager.currentPer._isSelected = true;
    }
    void PlacePerson(Transform tr)
    {
        GameManager.currentPer._img.color = new Color(1, 1, 1, 1);
        if (tr.TryGetComponent<RSlot>(out RSlot Sl))
        {
            _newSlot = Sl;
            if (_newSlot != _prevSlot && _newSlot.type==RSlot.Type.person && _isSelected)
            {
                Vector3 mouseP = GetMousePos();
                GameManager.currentPer.transform.position = new Vector3(tr.position.x, tr.position.y, GameManager.currentPer.transform.position.z);
                GameManager.currentPer.transform.parent = tr;
                Sl.AddPerson(GameManager.currentPer);
                if (_prevSlot == null)
                {
                    Debug.Log("OOF");
                }
                else
                    _prevSlot.RemovePerson();
                GameManager.currentPer._isSelected = false;
            }
            else
            {
                if (_prevSlot == null)
                {
                    Debug.Log("OOF");
                }
                else
                {
                    GameManager.currentPer.transform.position = new Vector3(_prevSlot.transform.position.x, _prevSlot.transform.position.y, GameManager.currentRes.transform.position.z);
                    GameManager.currentPer.transform.parent = _prevSlot.transform;
                }
            }
        }
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        TakePerson(this);
    }

    */
    public void SetPerson(Person p)
    {
        _img = GetComponent<Image>();
        _person = p;
        switch (_person.occupation)
        {
            case Person.Occupation.Stakeholder:
                _img.sprite = Stakeholder;
                break;
            case Person.Occupation.Sponsor:
                _img.sprite = Sponsor;
                break;
            case Person.Occupation.EndUser:
                _img.sprite = EndUser;
                break;
            case Person.Occupation.UXdesigner:
                _img.sprite=UXdesigner;
                break;
            case Person.Occupation.ProjectManager:
                _img.sprite=ProjectManager;
                break;
            case Person.Occupation.PSO:
                _img.sprite=PSO;
                break;
            case Person.Occupation.SME:
                _img.sprite=SME;
                break;
            case Person.Occupation.ProductOwner:
                _img.sprite=ProductOwner;
                break;
            case Person.Occupation.Programmer:
                _img.sprite=Programmer;
                break;
            case Person.Occupation.ExperienceExpert:
                _img.sprite=ExperienceExpert;
                break;
            case Person.Occupation.VisualDesigner:
                _img.sprite=VisualDesigner;
                break;
            case Person.Occupation.Tester:
                _img.sprite=Tester;
                break;
            default:
                break;
        }
    }

    public Person GetPerson() 
    {
        return _person;
    }

   
}
