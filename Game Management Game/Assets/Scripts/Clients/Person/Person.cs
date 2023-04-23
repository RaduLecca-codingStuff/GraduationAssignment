using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Person
{
    public enum Occupation
    {
        Stakeholder,
        Sponsor,
        EndUser,
        UXdesigner,
        ProjectManager,
        PSO,
        SME,
        ProductOwner,
        Programmer,
        ExperienceExpert,
        VisualDesigner,
        Tester

    }
    public Occupation occupation;

    int _minInvestment;
    int _minTime;
    public HexagonPiece pieceToWorkOn;
    public Person(Occupation oc)
    {
        occupation = oc;
    }
    void Start()
    {
        pieceToWorkOn = new HexagonPiece();
        //modifies the ammount of investment required at least 
        switch (occupation)
        {
            case Occupation.Stakeholder:
                _minInvestment = 0;
                _minTime=0;
                break;

            case Occupation.Sponsor:
                _minInvestment = 0;
                _minTime = 0;
                break;

            case Occupation.EndUser:
                _minInvestment = 0;
                _minTime = 0;
                break;

            case Occupation.UXdesigner:
                _minInvestment = 0;
                _minTime = 0;
                break;

            case Occupation.ProjectManager:
                _minInvestment = 0;
                _minTime = 0;
                break;

            case Occupation.ProductOwner:
                _minInvestment = 0;
                _minTime = 0;
                break;

            case Occupation.Programmer:
                _minInvestment = 0;
                _minTime = 0;
                break;

            case Occupation.ExperienceExpert:
                _minInvestment = 0;
                _minTime = 0;
                break;

            case Occupation.VisualDesigner:
                _minInvestment = 0;
                _minTime = 0;
                break;

            case Occupation.Tester:
                _minInvestment = 0;
                _minTime = 0;
                break;

            case Occupation.PSO:
                _minInvestment = 0;
                _minTime = 0;
                break;

            case Occupation.SME:
                _minInvestment = 0;
                _minTime = 0;
                break;
        }
    }
}
