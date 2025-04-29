using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : Singleton<MouseController>
{

    [field: SerializeField] public Camera Camera { get; private set; }
    public Action<RaycastHit> OnLeftMouseClick;
    public Action<RaycastHit> OnRightMouseClick;
    public Action<RaycastHit> OnMiddleMouseClick;

    protected override void Init()
    {
        if (Camera == null)
        {
            Debug.LogError("no camera set for MouseController");
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CheckMouseClick(0);
        }
        if (Input.GetMouseButtonDown(1))
        {
            CheckMouseClick(1);
        }
        if (Input.GetMouseButtonDown(2))
        {
            CheckMouseClick(2);
        }
    }

    void CheckMouseClick(int mouseButton)
    {
        Ray ray = Camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if (mouseButton == 0)
            {
                OnLeftMouseClick?.Invoke(hit);
            }
            else if (mouseButton == 1)
            {
                OnRightMouseClick?.Invoke(hit);
            }
            else if (mouseButton == 2)
            {
                OnMiddleMouseClick?.Invoke(hit);
            }
        }
    }
}
