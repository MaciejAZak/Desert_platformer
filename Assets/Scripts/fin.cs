using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fin : MonoBehaviour
{
    [SerializeField] AudioClip boom;
    [SerializeField] GameObject explosion;
    bool done = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        LaFin();
    }

     private void LaFin()
    {
        
        if (GameObject.FindGameObjectsWithTag("banana").Length == 0)
        {
            Debug.Log("No bananas");
                AudioSource.PlayClipAtPoint(boom, FindObjectOfType<Camera>().transform.position);
                GameObject exp = Instantiate(explosion) as GameObject;
                exp.transform.position = FindObjectOfType<Player>().transform.position;
                FindObjectOfType<Player>().FinalDeath();
        }

     }

}
