using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotArm : ActivablePlataform
{
    private static readonly Vector3 ACTIVE_CENTER = new Vector3(-0.781f, -0.394f, -0.5f); 
    private static readonly Vector3 ACTIVE_SIZE = new Vector3(3.162f, 1.113f, 0.8f);

    private Dictionary<string, Vector3> initialColliderSize = new Dictionary<string, Vector3>();
    private Dictionary<string, Vector3> activeColliderSize = new Dictionary<string, Vector3>();
    private BoxCollider collider;
    private bool _isActive = false;

    protected override void Start()
    {
        base.Start();
        collider = GetComponent<BoxCollider>();

        initialColliderSize.Add("center", collider.center);
        initialColliderSize.Add("size", collider.size);

        activeColliderSize.Add("center", ACTIVE_CENTER);
        activeColliderSize.Add("size", ACTIVE_SIZE);
    }

    public override void OnToggleActive()
    {
        base.OnToggleActive();
        _isActive = !_isActive;
        if (_isActive)
        {
            collider.center = activeColliderSize["center"];
            collider.size = activeColliderSize["size"];
        }
        else
        {
            collider.center = initialColliderSize["center"];
            collider.size = initialColliderSize["size"];
        }
    }
}
