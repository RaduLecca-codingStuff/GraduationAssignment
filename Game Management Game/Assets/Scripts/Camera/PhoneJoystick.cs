using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PhoneJoystick : MonoBehaviour,IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public CameraMovementScript cameraMovement;
    Image _bkgImg;
    Image _joystick;
    Vector3 _inputVector;
    Vector2 _joystickPosition;
    bool _hold = false;
    // Start is called before the first frame update
    void Start()
    {
        _bkgImg = GetComponent<Image>();
        _joystick = transform.GetChild(0).GetComponent<Image>();
        if (!GameManager.isMobile)
        {
            gameObject.SetActive(false);
        }
    }
    private void Update()
    {
        if (_hold)
        cameraMovement.MoveTheCamera(new Vector3(_joystickPosition.x, _joystickPosition.y));
    }
    public void OnPointerUp(PointerEventData data)
    {
        _inputVector = Vector3.zero;
        _joystick.rectTransform.anchoredPosition = Vector3.zero;
        cameraMovement.StopMovement();
       _hold = false;
    }
    public void OnPointerDown(PointerEventData data)
    {
        OnDrag(data);
    }

    public void OnDrag(PointerEventData eventData)
    {
        _hold = true;
        Vector2 pos;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(_bkgImg.rectTransform, eventData.position, eventData.pressEventCamera, out pos))
        {
            GameManager.WasJoystickUsed = true;
            _joystickPosition.x = (pos.x / _bkgImg.rectTransform.sizeDelta.x);
            _joystickPosition.y = (pos.y / _bkgImg.rectTransform.sizeDelta.y);

            _inputVector = new Vector3(_joystickPosition.x, _joystickPosition.y, 0);
            _inputVector = (_inputVector.magnitude > 1.0f) ? _inputVector.normalized : _inputVector;
            _joystick.rectTransform.anchoredPosition = new Vector3(_inputVector.x * (_bkgImg.rectTransform.sizeDelta.x / 2.5f), _inputVector.y * (_bkgImg.rectTransform.sizeDelta.y / 2.5f));
        }
    }

}
