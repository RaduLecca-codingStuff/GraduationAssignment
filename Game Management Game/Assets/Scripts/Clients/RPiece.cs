using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RPiece : MonoBehaviour
{
    /*
    [Header("Possible Resource Sprites")]
    public Sprite v1;
    public Sprite v2;
    public Sprite v3;
    [Header("Audio clips")]
    public AudioClip takeAudio;
    public AudioClip placeAudio;

    RSlot _prevSlot;
    RSlot _newSlot;
    Resource _resource;
    Image _img;
    */
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var mousepos = GetMousePos();
            RaycastHit2D hit = Physics2D.Raycast(mousepos, Vector2.up, .1f, 1 << 5);

            if (hit.collider != null)
            {
                if (hit.collider.tag == "RPiece")
                {
                    Debug.Log("Clicked on resource or person");
                }
                if (hit.collider.tag == "Slot")
                {
                    Debug.Log("Clicked on slot");
                }
            }
               
        }
    }
    Vector3 GetMousePos()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}
