using UnityEngine;

public class Rotator : MonoBehaviour
{
    public Vector3 rotationSpeed;

    void Update()
    {
        transform.Rotate(rotationSpeed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider target)
    {
        switch (target.tag)
        {
            case "Player":
                Destroy(this.gameObject);
                break;
            default:
                break;
        }
    }
}
