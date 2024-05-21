using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FlipAnimation : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            gameObject.transform.localScale = new Vector3(1, 1, 1);
        }

        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            gameObject.transform.localScale = new Vector3(-1, 1, 1);
        }
        /*
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        */
    }
}
