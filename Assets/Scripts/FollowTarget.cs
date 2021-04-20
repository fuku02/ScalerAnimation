using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class FollowTarget : MonoBehaviour
{
    public GameObject targetObj;
    public Vector3 cameraTargetPos = new Vector3(0.0f, 10f, -8f);
    public Vector2 mouseMoveSpeed = new Vector2(0.2f, 0.2f);
    public Vector2 wheelMoveSpeed = new Vector2(3f, 3f);
    public bool reverse;

    private Camera mainCamera;
    private Vector2 mousePosBuffer;

    private static FollowTarget _instance;
    public static FollowTarget Instance
    {
        get
        {
            return _instance;
        }
    }

    void Start()
    {
        Application.targetFrameRate = 60;
        _instance = this;
        mainCamera = GetComponent<Camera>();
    }

    void LateUpdate()
    {
        if (targetObj == null) return;

        //カメラをターゲットに移動
        if (targetObj.transform.InverseTransformPoint(mainCamera.transform.position) != cameraTargetPos)
        {
            mainCamera.transform.position = targetObj.transform.TransformPoint(cameraTargetPos);
            mainCamera.transform.LookAt(targetObj.transform.position);
        }

        //カメラのアングル
        if (Input.GetMouseButton(0) && Input.mouseScrollDelta.y == 0)
        {
            Vector2 moveVector = new Vector2();
            moveVector.x = reverse ? (mousePosBuffer.x - Input.mousePosition.x) : (Input.mousePosition.x - mousePosBuffer.x);
            moveVector.y = reverse ? (Input.mousePosition.y - mousePosBuffer.y) : (mousePosBuffer.y - Input.mousePosition.y);

            if (Mathf.Abs(moveVector.x) < Mathf.Abs(moveVector.y))
                moveVector.x = 0;
            else
                moveVector.y = 0;
            var rot = new Vector3();
            rot.x = moveVector.x * mouseMoveSpeed.x;
            rot.y = moveVector.y * mouseMoveSpeed.y;

            mainCamera.transform.RotateAround(targetObj.transform.position, Vector3.up, rot.x);
            mainCamera.transform.RotateAround(targetObj.transform.position, transform.right, rot.y);
        }
        else
        {
            //カメラの距離
            Vector3 cameraForward = Vector3.Scale(mainCamera.transform.forward, new Vector3(1, 1, 1)).normalized;
            Vector3 moveForward = mainCamera.transform.forward * (Input.mouseScrollDelta.y * wheelMoveSpeed.y);
            var dis = Vector3.Distance(targetObj.transform.position, mainCamera.transform.position + moveForward);
            if (dis >= 1.0f)
            {
                mainCamera.transform.position += moveForward;
                //mainCamera.transform.LookAt(targetObj.transform.position);
            }
        }
        cameraTargetPos = targetObj.transform.InverseTransformPoint(mainCamera.transform.position);
        mousePosBuffer = Input.mousePosition;
    }
}