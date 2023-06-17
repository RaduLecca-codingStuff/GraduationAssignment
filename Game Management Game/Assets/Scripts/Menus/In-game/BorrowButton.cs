using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorrowButton : MonoBehaviour
{
    public Transform Parent;
    public GameObject Prefab;
    public GameObject NCMenu;
    public void GiveMoreResources()
    {
        int[] values = GameManager.currentClient.ReturnPossibleValue();
        if (Prefab.TryGetComponent<ResourceObject>(out ResourceObject r))
        {
            if (GameManager.currentClient.GetChances() > 0)
            {
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < values[i]; j++)
                    {
                        Prefab.GetComponent<ResourceObject>().SetResource(new Resource(i));
                        GameObject pr = GameObject.Instantiate(Prefab);
                        pr.transform.SetParent(Parent);
                    }
                }
                GameManager.currentClient.ChanceUsed();
                GameManager.currentClient.IncreaseRequirements();
            }
            else
            {
                NCMenu.SetActive(true);
            }
            
        }
        else
        {

            Debug.LogError("Prefab must contain a ResourceObject component");
        }
            
    }
}
