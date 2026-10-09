using UnityEngine;

public class GroundSensor : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayer;
    private int triggerCount = 0;

    public bool IsGrounded => triggerCount > 0;

    private void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto detectado pertenece a la capa del suelo
        if (((1 << other.gameObject.layer) & groundLayer) != 0)
        {
            triggerCount++;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (((1 << other.gameObject.layer) & groundLayer) != 0)
        {
            triggerCount = Mathf.Max(0, triggerCount - 1);
        }
    }
}
