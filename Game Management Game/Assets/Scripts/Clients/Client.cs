using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class Client : MonoBehaviour
{
    public string clientName;
    public string description;
    List<Person> persons;
    List<Resource> resources;
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
        resources = new List<Resource>();
    }
    // Start is called before the first frame update

    public void SetInvestment( int i1, int i2, int i3)
    {
        TryAddInBulk(i1, 1);
        TryAddInBulk(i2, 2);
        TryAddInBulk(i3, 3);
    }
    public void SetPeople(List<Person> list)
    {
        persons = list;
    }
    void TryAddInBulk(int e,int nr)
    {
        if (e > 0)
        {
            for (int i = 0; i < e; i++)
            {
                resources.Add(new Resource(nr));
            }
        }
    }
    public List<Resource> ReturnResource()
    {
        return resources;
    }
    public List<Person> ReturnPersons()
    { 
        return persons;
    }
}
