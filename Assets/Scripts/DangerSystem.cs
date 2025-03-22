using UnityEngine;

public class DangerSystem : MonoBehaviour
{
    public static DangerSystem Main;
    public float MaxDangerDistance;
    public float MinDangerDistance;

    private float SqrMaxDangerDistance;
    private float SqrMinDangerDistance;

    public void Awake()
    {
        if (Main)
        {
            Debug.LogError($"DangerSystem.Main already exists, deleting the current one on {name}");
            Destroy(this);
            return;
        }

        Main = this;

        SqrMaxDangerDistance = Mathf.Pow(MaxDangerDistance, 2);
        SqrMinDangerDistance = Mathf.Pow(MinDangerDistance, 2);
    }

    /// <summary>
    /// From 0 to 1
    /// </summary>
    /// <returns></returns>
    public float CalculateDanger(Vector3 position)
    {
        float sqrDistance = (transform.position - position).sqrMagnitude;
        if (sqrDistance < SqrMaxDangerDistance)
        {
            return 1;
        }

        if (sqrDistance > SqrMinDangerDistance)
        {
            return 0;
        }

        return (sqrDistance - SqrMaxDangerDistance) / (SqrMinDangerDistance - SqrMaxDangerDistance);
    }
}
