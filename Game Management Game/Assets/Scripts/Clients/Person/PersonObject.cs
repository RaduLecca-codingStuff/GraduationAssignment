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
    // Start is called before the first frame update
    void Awake()
    {
    }
}
