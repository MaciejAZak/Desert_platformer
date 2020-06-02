using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] AudioClip pickupSFX;
    [SerializeField] int Points = 10;
    BoxCollider2D myCollider2D;
    Animator myAnimator;
    bool AlreadyTaken = false;
    

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (!AlreadyTaken)
        {
            AlreadyTaken = true;
            Debug.Log("Pickup!");
            AudioSource.PlayClipAtPoint(pickupSFX, FindObjectOfType<Camera>().transform.position);
            myAnimator = GetComponent<Animator>();
            myAnimator.SetTrigger("picked");
            FindObjectOfType<GameSession>().AddToScore(Points);
            StartCoroutine(destroyPickup());
        }

    }

    IEnumerator destroyPickup()
    {
        yield return new WaitForSecondsRealtime(3f);

        Destroy(gameObject);
    }
}
