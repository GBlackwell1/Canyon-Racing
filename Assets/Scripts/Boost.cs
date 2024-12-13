using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boost : MonoBehaviour
{
    public float boostDuration = 2f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Increase the player's speed
            other.GetComponent<PlayerController>().ApplyExternalBoost(boostDuration);
            var animator = gameObject.GetComponent<Animator>();
            animator.SetBool("Used", true);
            StartCoroutine(DestroyBoost());
        }
    }

    private IEnumerator DestroyBoost()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            if (gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
            {
                break;
            }
        }
        Destroy(gameObject.transform.parent.gameObject);
    }
}
