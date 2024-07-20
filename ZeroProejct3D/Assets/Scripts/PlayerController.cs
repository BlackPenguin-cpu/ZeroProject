using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.Properties;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float mouseRotateSpeed = 5f;
    public float interactiveDistance = 2;

    public GameObject nowInteractingObj;

    void Update()
    {
        InputFunc();
        MouseRotate();

        GrabObj();
    }
    private void GrabObj()
    {
        if (nowInteractingObj != null)
        {
            nowInteractingObj.transform.position = transform.position + transform.forward * 2f;
        }
    }
    private void InteractableRaycast()
    {
        int layerMask = 1 << LayerMask.NameToLayer("Interactable");
        bool isCheck = Physics.Raycast(transform.position, transform.forward, out RaycastHit hitInfo, interactiveDistance, layerMask);

        if (isCheck&& nowInteractingObj != null)
        {
            nowInteractingObj = hitInfo.collider.gameObject;
        }
    }
    private void InputFunc()
    {
        Move();
        if (Input.GetMouseButtonDown(0))
        {
            if (nowInteractingObj != null)
            {
                Throw();
            }
            else
            {
                InteractableRaycast();
            }
        }
    }

    private void Throw()
    {
        if (nowInteractingObj != null)
        {
            nowInteractingObj.GetComponent<Rigidbody>().AddForce(transform.forward * 10, ForceMode.Impulse);

            nowInteractingObj = null;
        }
    }
    private void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        Vector3 moveDir = transform.forward * z + transform.right * x;

        Vector3 vec = moveDir * moveSpeed * Time.deltaTime;
        vec.y = 0;
        transform.position += vec;
    }
    private void MouseRotate()
    {
        //È¸Àü
        float mouseX = Input.GetAxis("Mouse X") * mouseRotateSpeed;
        float mouseY = Input.GetAxis("Mouse Y") * mouseRotateSpeed;
        this.transform.localEulerAngles += new Vector3(-mouseY, mouseX, 0);
    }

}
