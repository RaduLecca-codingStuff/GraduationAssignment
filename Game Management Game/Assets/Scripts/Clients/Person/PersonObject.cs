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


    Person _person;
    Image _img;
    
    bool _isSelected = false;
    private void Awake()
    {
        _img = GetComponent<Image>();
    }
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
