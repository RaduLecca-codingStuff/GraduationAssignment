using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CameraMovementScript : MonoBehaviour
{
    public int maxWidth;
    public int maxHeight;
    public float speed;
    bool _isMoving=false;
    bool _wasUsed = false;
    Vector2 _direction;
    Vector2 _initPos;
    float _cameraWidth;
    float _cameraHeight;
    // Start is called before the first frame update
    void Start()
    {
        _initPos = transform.position;
        Camera cam = Camera.main;
        _cameraHeight = 2 * cam.orthographicSize;
        _cameraWidth = _cameraHeight * cam.aspect;
    }
    private void Update()
    {
        if (!GameManager.isMobile && !GameManager.isInMenu)
        {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");
            SetDirection(h, v);
        }
        if (_isMoving)
        {
            MoveTheCamera(_direction);
        }
    }
    public bool CheckIfUsed()
    {
        return _wasUsed;
    }
    public void ResetUse()
    {
        _wasUsed=false;
    }
    public void SetDirection(float h, float v)
    {
        _isMoving = true;
        _direction=new Vector2(h, v);
    }
    public void StopMovement()
    {
        _direction = Vector3.zero;
        _isMoving = false;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(_initPos, new Vector3(maxWidth, maxHeight, 1));
        Gizmos.DrawWireCube(_initPos, new Vector3(_cameraWidth, _cameraHeight, 1));
    }
     bool isInRange(float numberToCheck, int bottom, int top)
    {
        return (numberToCheck >= bottom && numberToCheck <= top);
    }

    public bool CheckOutsideFunction()
    {
        Vector3 tempVect = (Vector3)_direction.normalized * speed * Time.deltaTime;
        return (isInRange(transform.position.x + tempVect.x, -(maxWidth - (int)_cameraWidth) / 2, (maxWidth - (int)_cameraWidth) / 2)
            && isInRange(transform.position.y + tempVect.y, -(maxHeight - (int)_cameraHeight) / 2, (maxHeight - (int)_cameraHeight) / 2));      
    }
    public void MoveTheCamera(Vector3 t)
    {
        Vector3 tempVect = (Vector3)t.normalized * speed * Time.deltaTime;
        transform.position = new Vector3(Mathf.Clamp(transform.position.x + tempVect.x, -(maxWidth - (int)_cameraWidth) / 2, (maxWidth - (int)_cameraWidth) / 2)
                , Mathf.Clamp(transform.position.y + tempVect.y, -(maxHeight - (int)_cameraHeight) / 2, (maxHeight - (int)_cameraHeight) / 2), transform.position.z);
        _wasUsed = true;
    }
}
