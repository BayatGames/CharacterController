using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace Bayat.Games.Platforms
{

    public class WayPointTargetFollower : WayPointFollower
    {

        #region Fields

        [SerializeField]
        protected float checkDistance = 0.1f;

        //[SerializeField]
        //[Range(-1, 1, order = 2)]
        protected int direction = 1;

        protected Queue<WayPoint> nextWayPoints = new Queue<WayPoint>();
        protected int currentWayPointIndex = -1;
        protected int targetWayPointIndex = -1;
        protected WayPoint targetWayPoint;

        protected int lastWayPointIndex = -1;
        protected WayPoint lastWayPoint;

        protected WayPoint goalWayPoint;

        protected bool delayPassed = false;

        #endregion

        #region Unity Messages

        private void Start()
        {
            this.currentWayPoint = this.path.WayPoints[this.initialWayPointIndex];
            this.currentWayPointIndex = this.initialWayPointIndex;
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
                float moveSpeed = this.currentWayPoint.Speed;
                if (this.ignoreWayPointSpeed)
                {
                    moveSpeed = this.speed;
                }
                var newPosition = Vector2.MoveTowards(this.rigidbody2D.position, this.targetWayPoint.transform.position, moveSpeed * Time.fixedDeltaTime);
                this.rigidbody2D.MovePosition(newPosition);
                if (Vector2.Distance(this.rigidbody2D.position, this.targetWayPoint.transform.position) < this.checkDistance)
                {

                    // Raise WayPoint Reached Event
                    OnWayPointReached(this.currentWayPoint);

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

        #endregion

        #region Private Methods

        IEnumerator ProcessWayPoints()
        {
            while (true)
            {
                if (this.targetWayPoint == null && this.currentWayPoint != null && !this.delayPassed)
                {
                    float delay = this.currentWayPoint.WaitTimeInSeconds;
                    if (this.ignoreWayPointWaitTime)
                    {
                        delay = this.waitTimeInSeconds;
                    }
                    yield return new WaitForSeconds(delay);
                    this.delayPassed = true;
                }
                if (this.targetWayPoint == null && this.nextWayPoints.Count > 0)
                {
                    var nextWayPoint = this.nextWayPoints.Dequeue();
                    this.targetWayPointIndex = this.path.WayPoints.IndexOf(nextWayPoint);
                    this.targetWayPoint = nextWayPoint;
                    this.delayPassed = false;
                }
                yield return null;
            }
        }

        #endregion

        #region Public Methods

        public void RandomGoalWayPoint()
        {
            var newIndex = Random.Range(0, this.path.WayPoints.Count);
            Debug.Log(newIndex);
            SetGoalWayPoint(newIndex);
        }

        public void SetGoalWayPoint()
        {
            SetGoalWayPoint(this.initialWayPointIndex);
        }

        public override void SetGoalWayPoint(int index)
        {
            SetGoalWayPoint(this.path.WayPoints[index]);
        }

        public override void SetGoalWayPoint(WayPoint wayPoint)
        {
            this.goalWayPoint = wayPoint;
            this.nextWayPoints.Clear();
            int goalIndex = this.path.WayPoints.IndexOf(this.goalWayPoint);
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
                    this.nextWayPoints.Enqueue(this.path.WayPoints[i]);
                    Debug.Log(this.path.WayPoints[i]);
                }
            }
            else
            {
                for (int i = startIndex - skip; i >= goalIndex; i--)
                {
                    this.nextWayPoints.Enqueue(this.path.WayPoints[i]);
                    Debug.Log(this.path.WayPoints[i]);
                }
            }
            //}
        }

        public override void NextWayPoint()
        {
            int index = this.currentWayPointIndex;
            if (this.lastWayPoint != null)
            {
                index = this.lastWayPointIndex;
            }
            int nextWayPointIndex = index + this.direction;
            if (this.path.OpenEnd)
            {
                if (nextWayPointIndex < 0)
                {
                    nextWayPointIndex = this.path.WayPoints.Count - 1;
                }
                else if (nextWayPointIndex >= this.path.WayPoints.Count)
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
                else if (nextWayPointIndex >= this.path.WayPoints.Count)
                {
                    nextWayPointIndex = this.path.WayPoints.Count - 2;
                    this.direction = -1;
                }
            }
            var nextWayPoint = this.path.WayPoints[nextWayPointIndex];
            this.lastWayPoint = nextWayPoint;
            this.lastWayPointIndex = nextWayPointIndex;
            this.nextWayPoints.Enqueue(nextWayPoint);
        }

    }

    #endregion

}