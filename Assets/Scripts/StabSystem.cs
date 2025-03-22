using UnityEngine;

public class StabSystem : MonoBehaviour
{
    public static StabSystem Main;
    
    public float StabDistance = 5f;
    public Transform CameraTransform;

    private float SqrStabDistance;

    public void Awake()
    {
        if (Main)
        {
            Debug.LogError($"StabSystem.Main already exists, deleting the current one on {name}");
            Destroy(this);
            return;
        }

        Main = this;

        SqrStabDistance = Mathf.Pow(StabDistance, 2);
    }

    public bool IsInStabRange(Transform otherTransform)
    {
        return Vector3.SqrMagnitude(otherTransform.position - transform.position) < SqrStabDistance;
    }

    public void Update()
    {
        bool touched = Physics.Raycast(CameraTransform.position, CameraTransform.forward, out RaycastHit hit, StabDistance, LayerMask.NameToLayer("Person"));

    }
}
