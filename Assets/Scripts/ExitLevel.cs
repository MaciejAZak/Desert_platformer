using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitLevel : MonoBehaviour
{

    BoxCollider2D doorCollider;
    [SerializeField] float LoadLevelDelay = 3f;
    GameObject label;

    private void Start()
    {
        doorCollider = GetComponent<BoxCollider2D>();
        // label = GetComponent<>//Get children
    }

    private void Update()
    {
        if (doorCollider.IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            //SHOW WHICH BUTTON TO CLICK
            Debug.Log("Touching door");
            if (Input.GetKeyDown("return"))
            {
                StartCoroutine(LoadNextLevel());
                //TODO: Fireworks + music + "Level complete"
            }
        }
    }

    IEnumerator LoadNextLevel()
    {
        yield return new WaitForSecondsRealtime(LoadLevelDelay);
        var currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentLevelIndex + 1);
    }
}
