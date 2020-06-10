using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class musicPlayer : MonoBehaviour
{

    AudioSource audioSource;

    // Start is called before the first frame update
    private void Awake()
    {

        {
            int numScenePersist = FindObjectsOfType<musicPlayer>().Length;
            if (numScenePersist > 1)
            {
                Destroy(gameObject);
                Debug.Log("ScenePersist has been destoyed due to Singleton");
            }
            else
            {
                DontDestroyOnLoad(gameObject);
            }
        }

    }
    void Start()
    {
        DontDestroyOnLoad(this);
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }

    }
}
