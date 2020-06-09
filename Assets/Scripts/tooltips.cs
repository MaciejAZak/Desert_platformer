using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tooltips : MonoBehaviour
{
    bool activated = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (activated == true)
        {
            transform.Translate(new Vector2(0f, 1 * Time.deltaTime));
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        {
            Debug.Log("ToolTip activated!");
            activated = true;
        }

    }
}
