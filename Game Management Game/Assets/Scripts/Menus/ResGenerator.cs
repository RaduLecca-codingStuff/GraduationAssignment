using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResGenerator : MonoBehaviour
{
    public GameObject Prefab;
    public GameObject ParentObject;
    GameObject obj;

    // Start is called before the first frame update
    void Awake()
    {
         if (Prefab.TryGetComponent<PersonObject>(out PersonObject p))
        {
            int i = 0;
            foreach (Person per in GameManager.currentClient.ReturnPersons())
            {
                obj = GameObject.Instantiate(Prefab);
                obj.GetComponent<PersonObject>().SetPerson(per);
                obj.transform.SetParent(ParentObject.transform, true);
                i++;
            }
        }
        else if (Prefab.TryGetComponent<ResourceObject>(out ResourceObject r))
        {
            int i = 0;
            foreach (Resource res in GameManager.currentClient.ReturnResource())
            {
                obj = GameObject.Instantiate(Prefab);
                obj.GetComponent<ResourceObject>().SetResource(res);
                obj.transform.SetParent(ParentObject.transform, true);
                i++;
            }
        }
        else
            Debug.LogError("Methods Generator requires a methodprefab which contains either a functional ResourceObject component or PersonObject component");
    }
}
