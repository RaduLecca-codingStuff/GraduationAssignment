using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class Client : MonoBehaviour
{
    public string clientName;
    public string description;
    List<Person> persons;
    List<ResourceObject> resources;
    public int AvInvestment;
    //public int AvTime;
    //needed to determine what is needed to reach endgame
    public int reqPurpose;
    public int reqSustainability;
    public int reqExperience;

    public Client(string name,string description,int pur,int sust,int exp,List<Person> list)
    {
        this.clientName = name;
        this.description = description;
        persons = list;
        reqPurpose = pur;
        reqSustainability = sust;
        reqExperience = exp;
        
        resources = new List<ResourceObject>();

    }
    // Start is called before the first frame update

    public void SetInvestmentTypes(int t1,int t2, int t3, int i1, int i2, int i3)
    {
        TryAddInBulk(t1, 1, ResourceObject.Type.time);
        TryAddInBulk(t2, 2, ResourceObject.Type.time);
        TryAddInBulk(t3, 3, ResourceObject.Type.time);
        TryAddInBulk(i1, 1, ResourceObject.Type.time);
        TryAddInBulk(i2, 2, ResourceObject.Type.time);
        TryAddInBulk(i3, 3, ResourceObject.Type.time);
    }
    void TryAddInBulk(int e,int nr, ResourceObject.Type t)
    {
        if (e > 0)
        {
            for (int i = 0; i < e; i++)
            {
                resources.Add(new ResourceObject(nr, t));
            }
        }
    }
}
