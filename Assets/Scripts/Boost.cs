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
            gameObject.SetActive(false);
        }
    }
}
