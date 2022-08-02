using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace Bayat.Games.Platforms
{

    public class WayPointTargetFollower : MonoBehaviour
    {

        [Header("References")]
        [SerializeField]
        protected new Rigidbody2D rigidbody2D;
        [SerializeField]
        protected WayPointController wayPointController;

        [Header("Parameters")]
        [SerializeField]
        protected bool useWayPointSpeed = true;
        [SerializeField]
        protected float speed = 1f;
        [SerializeField]
        protected bool useWayPointDelay = true;
        [SerializeField]
        protected float delayInSeconds = 1f;

        [SerializeField]
        protected float checkDistance = 0.1f;

        [SerializeField]
        protected bool setPositionOnStart = true;
        [SerializeField]
        protected bool activateOnStart = true;
        [SerializeField]
        protected bool autoMoveNext = true;
        [SerializeField]
        protected int firstWayPointIndex = 0;

        //[SerializeField]
        //[Range(-1, 1, order = 2)]
        protected int direction = 1;

        protected Queue<WayPoint> nextWayPoints = new Queue<WayPoint>();
        protected int currentWayPointIndex = -1;
        protected WayPoint currentWayPoint;
        protected int targetWayPointIndex = -1;
        protected WayPoint targetWayPoint;

        protected int lastWayPointIndex = -1;
        protected WayPoint lastWayPoint;

        protected WayPoint goalWayPoint;

        protected bool delayPassed = false;

        private void Start()
        {
            this.currentWayPoint = this.wayPointController.WayPoints[this.firstWayPointIndex];
            this.currentWayPointIndex = this.firstWayPointIndex;
            if (this.autoMoveNext)
            {
                NextWayPoint();
            }
            if (this.setPositionOnStart)
            {
                transform.position = this.currentWayPoint.transform.position;
            }
            StartCoroutine("ProcessWayPoints");
        }

        private void FixedUpdate()
        {
            if (this.targetWayPoint != null)
            {
                float moveSpeed = this.speed;
                if (this.useWayPointSpeed)
                {
                    moveSpeed = this.currentWayPoint.Speed;
                }
                var newPosition = Vector2.MoveTowards(this.rigidbody2D.position, this.targetWayPoint.transform.position, moveSpeed * Time.fixedDeltaTime);
                this.rigidbody2D.MovePosition(newPosition);
                if (Vector2.Distance(this.rigidbody2D.position, this.targetWayPoint.transform.position) < this.checkDistance)
                {
                    this.currentWayPoint = this.targetWayPoint;
                    this.currentWayPointIndex = this.targetWayPointIndex;
                    this.targetWayPoint = null;
                    if (this.autoMoveNext)
                    {
                        NextWayPoint();
                    }
                    Debug.LogWarning("Reached");
                }
                //var distanceSquared = (transform.position - this.targetWayPoint.transform.position).sqrMagnitude;
                //if (distanceSquared < this.checkDistance * this.checkDistance)
                //{
                //    Debug.Log("Reached");
                //    this.currentWayPoint = this.targetWayPoint;
                //    this.targetWayPoint = null;
                //    if (this.autoMoveNext)
                //    {
                //        NextWayPoint();
                //    }
                //}
            }
        }

        IEnumerator ProcessWayPoints()
        {
            while (true)
            {
                if (this.targetWayPoint == null && this.currentWayPoint != null && !this.delayPassed)
                {
                    float delay = this.delayInSeconds;
                    if (this.useWayPointDelay)
                    {
                        delay = this.currentWayPoint.DelayInSeconds;
                    }
                    yield return new WaitForSeconds(delay);
                    this.delayPassed = true;
                }
                if (this.targetWayPoint == null && this.nextWayPoints.Count > 0)
                {
                    var nextWayPoint = this.nextWayPoints.Dequeue();
                    this.targetWayPointIndex = this.wayPointController.WayPoints.IndexOf(nextWayPoint);
                    this.targetWayPoint = nextWayPoint;
                    this.delayPassed = false;
                }
                yield return null;
            }
        }

        public void RandomGoalWayPoint()
        {
            var newIndex = Random.Range(0, this.wayPointController.WayPoints.Count);
            Debug.Log(newIndex);
            SetGoalWayPoint(newIndex);
        }

        public void SetGoalWayPoint()
        {
            SetGoalWayPoint(this.firstWayPointIndex);
        }

        public void SetGoalWayPoint(int index)
        {
            SetGoalWayPoint(this.wayPointController.WayPoints[index]);
        }

        public void SetGoalWayPoint(WayPoint wayPoint)
        {
            this.goalWayPoint = wayPoint;
            this.nextWayPoints.Clear();
            int goalIndex = this.wayPointController.WayPoints.IndexOf(this.goalWayPoint);
            int startIndex = this.currentWayPointIndex;
            if (this.targetWayPointIndex >= 0)
            {
                startIndex = this.targetWayPointIndex;
            }
            Debug.LogFormat("Current waypoint index: {0}", this.currentWayPointIndex);
            Debug.LogFormat("Target waypoint index: {0}", this.targetWayPointIndex);
            Debug.LogFormat("Goal waypoint index: {0}", goalIndex);
            Debug.LogFormat("Current waypoint: {0}", this.currentWayPoint);
            Debug.LogFormat("Target waypoint: {0}", this.targetWayPoint);
            //if (this.goalWayPoint == this.currentWayPoint)
            //{
            //    this.targetWayPoint = this.currentWayPoint;
            //    this.targetWayPointIndex = this.currentWayPointIndex;
            //    this.delayPassed = true;
            //    Debug.LogWarning("Going back to current");
            //}
            //else
            //{
            this.delayPassed = true;
            int skip = 1;
            if ((this.targetWayPointIndex > this.currentWayPointIndex && goalIndex > this.currentWayPointIndex) ||
                (this.targetWayPointIndex < this.currentWayPointIndex && goalIndex < this.currentWayPointIndex))
            {
                startIndex = this.targetWayPointIndex;
                skip = 0;
            }
            else if (this.targetWayPoint != null)
            {
                if (this.currentWayPointIndex == this.targetWayPointIndex)
                {
                    if (goalIndex > this.currentWayPointIndex)
                    {
                        skip = 1;
                    }
                    else
                    {
                        skip = 0;
                    }
                }
                else
                {
                    skip = 0;
                }
                //if (this.targetWayPoint == this.goalWayPoint)
                //{
                //    startIndex = this.targetWayPointIndex;
                //}
                //else
                //{
                startIndex = this.currentWayPointIndex;
                //}
                //if (this.targetWayPointIndex < this.currentWayPointIndex && goalIndex <= this.targetWayPointIndex)
                //{
                //    skip = 1;
                //}
                //else
                //{
                //    skip = 0;
                //}
            }
            this.targetWayPoint = null;
            if (goalIndex > startIndex)
            {
                for (int i = startIndex + skip; i <= goalIndex; i++)
                {
                    this.nextWayPoints.Enqueue(this.wayPointController.WayPoints[i]);
                    Debug.Log(this.wayPointController.WayPoints[i]);
                }
            }
            else
            {
                for (int i = startIndex - skip; i >= goalIndex; i--)
                {
                    this.nextWayPoints.Enqueue(this.wayPointController.WayPoints[i]);
                    Debug.Log(this.wayPointController.WayPoints[i]);
                }
            }
            //}
        }

        public void NextWayPoint()
        {
            int index = this.currentWayPointIndex;
            if (this.lastWayPoint != null)
            {
                index = this.lastWayPointIndex;
            }
            int nextWayPointIndex = index + this.direction;
            if (this.wayPointController.OpenEnd)
            {
                if (nextWayPointIndex < 0)
                {
                    nextWayPointIndex = this.wayPointController.WayPoints.Count - 1;
                }
                else if (nextWayPointIndex >= this.wayPointController.WayPoints.Count)
                {
                    nextWayPointIndex = 0;
                }
            }
            else
            {
                if (nextWayPointIndex < 0)
                {
                    nextWayPointIndex = index + 1;
                    this.direction = 1;
                }
                else if (nextWayPointIndex >= this.wayPointController.WayPoints.Count)
                {
                    nextWayPointIndex = this.wayPointController.WayPoints.Count - 2;
                    this.direction = -1;
                }
            }
            var nextWayPoint = this.wayPointController.WayPoints[nextWayPointIndex];
            this.lastWayPoint = nextWayPoint;
            this.lastWayPointIndex = nextWayPointIndex;
            this.nextWayPoints.Enqueue(nextWayPoint);
        }

    }

}