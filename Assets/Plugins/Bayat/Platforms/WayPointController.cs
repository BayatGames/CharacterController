using System.Collections;
using System.Collections.Generic;

using UnityEngine;

[ExecuteInEditMode]
public class WayPointController : MonoBehaviour
{

    [SerializeField]
    protected bool openEnd = false;
    [SerializeField]
    protected List<WayPoint> wayPoints = new List<WayPoint>();

    public bool OpenEnd
    {
        get
        {
            return this.openEnd;
        }
    }

    public List<WayPoint> WayPoints
    {
        get
        {
            return this.wayPoints;
        }
    }

#if UNITY_EDITOR
    void Update()
    {
        if (transform.childCount != this.wayPoints.Count)
        {
            this.wayPoints.Clear();
            for (int i = 0; i < transform.childCount; i++)
            {
                var child = transform.GetChild(i);
                var wayPoint = child.GetComponent<WayPoint>();
                if (wayPoint != null)
                {
                    this.wayPoints.Add(wayPoint);
                }
            }
        }
    }
#endif

    public void OnDrawGizmos()
    {
        if (this.wayPoints == null || this.wayPoints.Count < 2)
            return;

        for (var i = 1; i < this.wayPoints.Count; i++)
        {
            Gizmos.DrawLine(this.wayPoints[i - 1].transform.position, this.wayPoints[i].transform.position);
        }
    }

}
