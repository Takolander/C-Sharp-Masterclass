using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacketPlayer2 : MonoBehaviour
{
    public float movementSpeed;

    public void Start()
    {
        ToogleAI toogleAI = gameObject.AddComponent(typeof(ToogleAI)) as ToogleAI;
        if (toogleAI.isAiActive())
        {
            this.enabled = false;
        }
    }

    private void FixedUpdate()
    {
        float v = Input.GetAxisRaw("Vertical2");

        GetComponent<Rigidbody2D>().velocity = new Vector2(0, v) * movementSpeed;
    }
}
