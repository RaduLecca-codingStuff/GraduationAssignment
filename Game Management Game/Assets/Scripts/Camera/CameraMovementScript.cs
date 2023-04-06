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
    Vector2 _direction;
    Vector2 _initPos;
    // Start is called before the first frame update
    void Start()
    {
        _initPos = transform.position;
    }
    private void Update()
    {
        if (!GameManager.isMobile)
        {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");
            MoveCamera(h, v);
        }
        if (_isMoving)
        {
            Vector3 tempVect = (Vector3)_direction.normalized * speed * Time.deltaTime;
            if (isInRange(transform.position.x + tempVect.x, -maxWidth / 2, maxWidth / 2)
                && isInRange(transform.position.y + tempVect.y, -maxHeight / 2, maxHeight / 2))
            {
                transform.position += tempVect;
            }

        }

    }
    public void MoveCamera(float h, float v)
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
    }
    bool isInRange(float numberToCheck, int bottom, int top)
    {
        return (numberToCheck >= bottom && numberToCheck <= top);
    }
}
