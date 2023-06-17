using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    TMP_Text _text;
    
    bool _isSelected = false;
    private void Awake()
    {
        _img = GetComponent<Image>();
        _text = GetComponentInChildren<TMP_Text>();
    }
    public void SetPerson(Person p)
    {
        _img = GetComponent<Image>();
        _person = p;
        switch (_person.occupation)
        {
            case Person.Occupation.Stakeholder:
                _img.sprite = Stakeholder;
                _text.text = "Stakeholder";
                break;
            case Person.Occupation.Sponsor:
                _img.sprite = Sponsor;
                _text.text = "Sponsor";
                break;
            case Person.Occupation.EndUser:
                _img.sprite = EndUser;
                _text.text = "End User";
                break;
            case Person.Occupation.UXdesigner:
                _img.sprite=UXdesigner;
                _text.text = "UX Designer";
                break;
            case Person.Occupation.ProjectManager:
                _img.sprite=ProjectManager;
                _text.text = "Project Manager";
                break;
            case Person.Occupation.PSO:
                _img.sprite=PSO;
                _text.text = "P.S.O.";
                break;
            case Person.Occupation.SME:
                _img.sprite=SME;
                _text.text = "S.M.E.";
                break;
            case Person.Occupation.ProductOwner:
                _img.sprite=ProductOwner;
                _text.text = "Product Owner";
                break;
            case Person.Occupation.Programmer:
                _img.sprite=Programmer;
                _text.text = "Programmer";
                break;
            case Person.Occupation.ExperienceExpert:
                _img.sprite=ExperienceExpert;
                _text.text = "Experience Expert";
                break;
            case Person.Occupation.VisualDesigner:
                _img.sprite=VisualDesigner;
                _text.text = "Visual Designer";
                break;
            case Person.Occupation.Tester:
                _img.sprite=Tester;
                _text.text = "Tester";
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
