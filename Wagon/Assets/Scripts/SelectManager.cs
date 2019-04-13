using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectManager : MonoBehaviour {

    Ray ray;
    public GameObject selectedEnemy;

    // Bit shift of layer mask to get Layer 9;
    private int layerMask = 1 << 9;

    // Distance of how far touch and click will take the ray
    private float rayDistance = 500;

    void Start()
    {
        DebugMobileManager.Create();
    }

    // Update is called once per frame
    void Update () {
        
		if((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
        {
            Touch touch = Input.GetTouch(0);
            DebugMobileManager.Log("x: " + touch.position.x.ToString() + " y:" + touch.position.y.ToString());
            CastRayForward(touch.position);
        }
        else if (Input.GetKeyDown(KeyCode.Mouse0) == true)
        {
            CastRayForward(Input.mousePosition);
        }
	}

    void CastRayForward(Vector3 _inputPosition)
    {
        ray = Camera.main.ScreenPointToRay(_inputPosition);
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity, layerMask);

        if (hit.collider != null)
        {
            SetSelected(hit.collider.gameObject);
            Debug.DrawRay(ray.origin, Camera.main.transform.forward * hit.distance, Color.red);
            Debug.Log("Did Hit");
            DebugMobileManager.Log("Did Hit: " + selectedEnemy.name);
        }
        else
        {
            Debug.DrawRay(ray.origin, Camera.main.transform.forward * 500, Color.white);
            Debug.Log("Did not Hit");
            DebugMobileManager.Log("Did not Hit");
        }
    }

    public void SetSelected(GameObject _go)
    {
        selectedEnemy = _go;
    }
}
