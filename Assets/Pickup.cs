using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] AudioClip pickupSFX;
    BoxCollider2D myCollider2D;
    Animator myAnimator;
    

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Pickup!");
        AudioSource.PlayClipAtPoint(pickupSFX, FindObjectOfType<Camera>().transform.position);
        myAnimator = GetComponent<Animator>();
        myAnimator.SetTrigger("picked");
        StartCoroutine(destroyPickup());
    }

    IEnumerator destroyPickup()
    {
        yield return new WaitForSecondsRealtime(3f);

        Destroy(gameObject);
    }
}
