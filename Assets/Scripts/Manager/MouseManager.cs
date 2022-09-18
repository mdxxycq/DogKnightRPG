using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class MouseManager : MonoBehaviour
{

    private RaycastHit hit;
    public event Action<Vector3> ClickGround;
    public event Action<GameObject> ClickMonster;
    public Texture2D attack, normal;
    public static MouseManager Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
       // ClickGround += MovePlayer;
    }



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SetCursorTexture();
        MouseClick();
    }

    private void MouseClick()
    {

        if (Input.GetMouseButtonDown(0) &&  hit.collider != null  &&   hit.collider.tag == "Ground")
        {
            //TODO:移动到点击的位置
            ClickGround?.Invoke(hit.point);
        }
        else if(Input.GetMouseButtonDown(0) && hit.collider != null && hit.collider.tag == "Monster")
        {
            ClickMonster?.Invoke(hit.collider.gameObject);
            Debug.Log("click monster");
        }
    }
    private void MouseClickMonster()
    {

    }
    private void MovePlayer(Vector3 target)
    {
        
    }
    private void SetCursorTexture()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.CompareTag("Ground"))
            {
                Cursor.SetCursor(normal,hit.point,CursorMode.Auto);
            }
            else
            {
                Cursor.SetCursor(attack, hit.point, CursorMode.Auto);
            }
        }
        
    }

}
