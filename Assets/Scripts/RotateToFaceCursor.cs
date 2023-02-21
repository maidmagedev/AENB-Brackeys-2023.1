using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToFaceCursor : MonoBehaviour
{
    [SerializeField] Camera mainCam;
    private Vector3 mousePos;
    [SerializeField] bool flipEnabled = true;

    // Start is called before the first frame update
    void Start()
    {
        if (mainCam == null) {
            mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        /*mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 rotation = mousePos - transform.position;
        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotZ);*/
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePosition - transform.position;
        float angle = Vector2.SignedAngle(Vector2.right, direction);
        //print("angle: " + angle);
        
        // uncomment this to rotate sprite based off mouse position
        transform.eulerAngles = new Vector3(0, 0, angle);
        if (flipEnabled) {
            print(angle);
            if (Mathf.Abs(angle) > 100)
            {
                // facing left
                gameObject.transform.localScale = new Vector2(1f, -1f);
                //print("facing left");
            }
            else if (Mathf.Abs(angle) > -100)
            {
                // facing right
                gameObject.transform.localScale = new Vector2(-1f, 1f);
                //print("facing right");
            }
        }
    }
}
