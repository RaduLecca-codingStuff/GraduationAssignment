using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class Client 
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
    int _chancesToExtra;
    int[] _whatToOffer=new int[3];

   float Time;

    public Client(string name,string description,int pur,int sust,int exp,List<Person> list, int chances, float time)
    {
        this.clientName = name;
        this.description = description;
        persons = list;
        reqPurpose = pur;
        reqSustainability = sust;
        reqExperience = exp;
        resources = new List<Resource>();
        Time = time;
        if (Time < 0)
            Time = 0;
        if (Time > 100)
            Time = 100;
        //might require the change this later
        _whatToOffer[0] = exp * 2 / 3;
        _whatToOffer[1] = sust * 2 / 3;
        _whatToOffer[2] = pur * 2 / 3;
        _chancesToExtra = chances;
    }
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
    public int[] ReturnPossibleValue()
    {
       return _whatToOffer;
    }
    public int GetChances()
    {
        return _chancesToExtra;
    }
    public void ChanceUsed()
    {
        _chancesToExtra--;
    }
    public void IncreaseRequirements()
    {
        reqPurpose +=GameManager.difficulty*3;
        reqSustainability += GameManager.difficulty * 3;
        reqExperience += GameManager.difficulty * 3;
    }
    public void LetBorrow()
    {
        ChanceUsed();
        IncreaseRequirements();
        TryAddInBulk(_whatToOffer[0], 1);
        TryAddInBulk(_whatToOffer[1], 2);
        TryAddInBulk(_whatToOffer[2], 3);
    }

    public float GetRemainingTime()
    {
        return Time;
    }
    public void MinusTime(float min)
    {
        Time -= min;
        if (Time < 0)
            Time = 0;
    }
    public void PlusTime(float plus)
    {
        Time += plus;
        if(Time >100)
            Time = 100;
    }
}
