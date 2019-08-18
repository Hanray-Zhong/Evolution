using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KanetoTools;

public class MoveCamera : MonoBehaviour
{
    public bool IsFollowing;
    private Vector2 MousePosition;
    private Vector2 relativePos;

    public BoxCollider2D Edge;
    private Vector3 _min;//边界最大值
    private Vector3 _max;//边界最小值
    private float edgeHeight;
    private float edgeWidth;
    private float cameraHeight;
    private float cameraWidth;
    public GameObject Target;
    [Header("Android")]
    public bool isAndroid;
    public float MoveSpeed;
    public ButtonTrigger[] Buttons;
    private Vector2 lastPos;
    
    private void Start() {
        Init();
        UpdateBounds();
    }
    private void Update() {
        foreach (var button in Buttons) {
            if (button.ClickOn == 1) {
                IsFollowing = false;
                return;
            }
        }
        IsFollowing = true;
    }
    void LateUpdate() {
        if (!IsFollowing)
            return;
        // Debug.Log(Input.mousePosition);
        MousePosition = new Vector2(Mathf.Clamp(Input.mousePosition.x, 0, Screen.width), Mathf.Clamp(Input.mousePosition.y, 0, Screen.height)) - new Vector2(Screen.width / 2, Screen.height / 2);
        // Debug.Log(MousePosition);
        if (Target != null) {
            relativePos = Target.transform.position;
        }
        else if (isAndroid) {
            if (Input.GetMouseButtonDown(0))
                lastPos = MousePosition;
            if (Input.GetMouseButton(0)) {
                Vector2 MoveDir;
                MoveDir = -(MousePosition - lastPos).normalized;
                relativePos = new Vector2(Screen.width / 2, Screen.height / 2) + MoveDir * MoveSpeed;
                relativePos = KanetoTools.World_Screen_Translation.ScreenToWorld(gameObject, relativePos);
                lastPos = MousePosition;
            }
            if (Input.GetMouseButtonUp(0)) {
                relativePos = new Vector2(Screen.width / 2, Screen.height / 2);
                relativePos = KanetoTools.World_Screen_Translation.ScreenToWorld(gameObject, relativePos);
            }
        }
        else {
            relativePos = MousePosition * ((edgeWidth - cameraWidth) / Screen.width);
        }
        // Debug.Log(cameraWidth);
        // gameObject.transform.SetPositionAndRotation(new Vector3(relativePos.x, relativePos.y, gameObject.transform.position.z), Quaternion.identity);
        // 平滑移动
        var x = transform.position.x;
        var y = transform.position.y;
        x = Mathf.Lerp(x, relativePos.x, Time.deltaTime);
        y = Mathf.Lerp(y, relativePos.y, Time.deltaTime);
        float orthographicSize = GetComponent<Camera>().orthographicSize;               //orthographicSize代表相机(或者称为游戏视窗)竖直方向一半的范围大小,且不随屏幕分辨率变化(水平方向会变)
        var cameraHalfWidth = orthographicSize * ((float)Screen.width / Screen.height); //的到视窗水平方向一半的大小
        x = Mathf.Clamp (x, _min.x + cameraHalfWidth, _max.x-cameraHalfWidth);      //限定x值
        y = Mathf.Clamp (y, _min.y + orthographicSize, _max.y-orthographicSize);    //限定y值
        transform.position = new Vector3(x, y, transform.position.z);
    }


    void Init() {
        edgeHeight = Edge.bounds.max.y - Edge.bounds.min.y;
        edgeWidth = Edge.bounds.max.x - Edge.bounds.min.x;
        // Debug.Log(edgeWidth);
        Vector3 Screen_max = new Vector3(Screen.width, Screen.height, 0);
        cameraHeight = 2 * gameObject.GetComponent<Camera>().ScreenToWorldPoint(Screen_max).y;
        cameraWidth = 2 * gameObject.GetComponent<Camera>().ScreenToWorldPoint(Screen_max).x;
    }
    public void UpdateBounds() {
        _min = Edge.bounds.min;//初始化边界最小值(边界左下角)
        _max = Edge.bounds.max;//初始化边界最大值(边界右上角)
    }
    private void OnDrawGizmosSelected() {
        Gizmos.color = new Color(1, 0, 0, 1);
        Vector3 a = new Vector3((cameraWidth) / 2, (cameraHeight) / 2, 0);
        Vector3 b = new Vector3((cameraWidth) / 2, -(cameraHeight) / 2, 0);
        Vector3 c = new Vector3(-(cameraWidth) / 2, -(cameraHeight) / 2, 0);
        Vector3 d = new Vector3(-(cameraWidth) / 2, (cameraHeight) / 2, 0);
        
        Gizmos.DrawLine(a, b);
        Gizmos.DrawLine(a, d);
        Gizmos.DrawLine(c, b);
        Gizmos.DrawLine(c, d);
    }
}
