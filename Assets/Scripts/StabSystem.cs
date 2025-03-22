using UnityEngine;

public class StabSystem : MonoBehaviour
{
    public float SqrStabDistance = Mathf.Pow(2f, 2);
    public Transform PlayerTransform;

    public bool IsInStabRange(Transform otherTransform)
    {
        return Vector3.SqrMagnitude(otherTransform.position - PlayerTransform.position) < SqrStabDistance;
    }
}
