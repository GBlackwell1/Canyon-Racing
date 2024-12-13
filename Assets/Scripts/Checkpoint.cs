using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private CheckpointManager checkpointManager;
    // Start is called before the first frame update
    void Start()
    {
        checkpointManager = GetComponentInParent<CheckpointManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && other.GetType() == typeof(BoxCollider))
        {
            checkpointManager.ColliderEvent(gameObject);
            if (!gameObject.CompareTag("Finish"))
            {
                var animator = transform.Find("Model").gameObject.GetComponent<Animator>();
                animator.SetBool("Completed", true);
            }
        }  
    }
}
