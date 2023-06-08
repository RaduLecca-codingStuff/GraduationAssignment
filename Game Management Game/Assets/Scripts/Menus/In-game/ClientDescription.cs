using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClientDescription : MonoBehaviour
{
    public Text Name;
    public Text Description;
    public Text fundsAvailable;
    public Text ResChances;
    // Start is called before the first frame update

    private void OnEnable()
    {
        Name.text=GameManager.currentClient.clientName;
        Description.text = GameManager.currentClient.description;
        fundsAvailable.text= "Funds Available : "+ GameManager.currentClient.AvInvestment.ToString();
        ResChances.text = GameManager.currentClient.GetChances().ToString();
    }
    private void Update()
    {
        ResChances.text = GameManager.currentClient.GetChances().ToString();
    }
}
