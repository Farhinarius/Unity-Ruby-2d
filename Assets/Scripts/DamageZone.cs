using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageZone : MonoBehaviour
{
    public GameObject InteractionObject;

    // Start is called before the first frame update
    void OnTriggerStay2D(Collider2D other)
    {
        RubyController controller = other.GetComponent<RubyController>();
        if (controller != null)
        {
            controller.ChangeHealth(-1);
        }

        if (controller.health <= 0)
        {
            Destroy(InteractionObject);
        }
    }
}
