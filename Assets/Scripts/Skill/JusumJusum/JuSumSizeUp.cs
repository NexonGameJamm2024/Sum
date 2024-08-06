using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class JuSumSizeUp : MonoBehaviour
{
    [SerializeField] private Transform tp;
    private PlayerMovement _pm;

    private void Start()
    {
        TryGetComponent(out _pm);
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Jelly"))
        {
            string type = other.GetComponent<Jelly>().type;

            if (type == "SizeUp")
            {
                tp.localScale = new Vector2(2f, 2f);
                _pm.ChangeJumpForce(12f);
                Destroy(other.gameObject);
            }
            else if (type == "SizeDown")
            {
                tp.localScale = new Vector2(0.5f, 0.5f);
                _pm.ChangeJumpForce(3f);
                Destroy(other.gameObject);
            }
            else if (type == "Medium")
            {
                tp.localScale = new Vector2(1f, 1f);
                _pm.ChangeJumpForce(7.5f);
                Destroy(other.gameObject);
            }
        }
    }
}
