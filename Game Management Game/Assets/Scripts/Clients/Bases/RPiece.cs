using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RPiece : MonoBehaviour, IPointerClickHandler
{
    enum Type
    {
        person,
        resource
    }
    Type type;
    Image _img;
    AudioSource _audioSource;
    bool _selected;
    RSlot _currentSlot;
    [Header("Audio clips")]
    public AudioClip takeAudio;
    public AudioClip placeAudio;

    void Start()
    {
        _img = GetComponent<Image>();
        _audioSource = GetComponent<AudioSource>();
        if (TryGetComponent<PersonObject>(out PersonObject p))
        {
            type = Type.person;
        }
        else if (TryGetComponent<ResourceObject>(out ResourceObject r))
        {
            type = Type.resource;
        }
        else
        {
            Debug.LogError("Please add either a PersonObject or a ResourceObject component for this to work. Otherwise, this won't work");
        }
    }
    void OnEnable()
    {
        if(_audioSource)
        _audioSource.volume = GameManager.sfxVolume;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && GameManager.currentPiece)
        {
            var mousepos = GetMousePos();
            RaycastHit2D hit = Physics2D.Raycast(mousepos, Vector2.up, .1f, 1 << 5);

            if (hit.collider != null)
            {
                if (hit.collider.tag == "Slot")
                {
                    this.SetNewSlot(hit.collider.gameObject);
                }
            }
        }
    }
    Vector3 GetMousePos()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    void SetNewSlot(GameObject slotP)
    {
        if (slotP.TryGetComponent<RSlot>(out RSlot slot))
        {

            if (slot.type.ToString() == this.type.ToString() && slot != _currentSlot && slot.isEmpty)
            {
                if (_currentSlot)
                    _currentSlot.isEmpty = true;
                GameManager.currentPiece.transform.SetParent(slotP.transform);
                GameManager.currentPiece.transform.position = slot.transform.position;
                _currentSlot = slot;
                _currentSlot.isEmpty = false;

                if (slot.type.ToString() == "person")
                    slot.AddPerson(GameManager.currentPiece.gameObject.GetComponent<PersonObject>());
                else if (slot.type.ToString() == "resource")
                    slot.AddResource(GameManager.currentPiece.gameObject.GetComponent<ResourceObject>());

            }
        }
        GameManager.currentPiece._img.color = new Color(1, 1, 1, 1);
        GameManager.currentPiece._selected = false;
        GameManager.currentPiece._audioSource.clip = placeAudio;

        GameManager.currentPiece._audioSource.Play();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(GameManager.currentPiece)
        {
            GameManager.currentPiece._img.color = new Color(1, 1, 1, 1);
            GameManager.currentPiece._selected = false;
        }
        GameManager.currentPiece = this;
        GameManager.currentPiece._selected = true;
        GameManager.currentPiece._img.color = new Color(1, 1, 1, .5f);
        GameManager.currentPiece._audioSource.clip = takeAudio;
        GameManager.currentPiece._audioSource.Play();
    }
}
