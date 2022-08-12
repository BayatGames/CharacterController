using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace Bayat.Games.Platforms
{

    public class WayPointQueueFollower : MonoBehaviour
    {

        #region Fields

        [Header("References")]
        [SerializeField]
        protected new Rigidbody2D rigidbody2D;
        [SerializeField]
        protected WayPointPath path;

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
        protected bool autoMoveNext = true;
        [SerializeField]
        protected int firstWayPointIndex = 0;

        [SerializeField]
        protected bool sendMessageToWayPoint = true;

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

        protected bool delayPassed = false;

        #endregion

        #region Unity Messages

        private void Start()
        {
            this.currentWayPoint = this.path.WayPoints[this.firstWayPointIndex];
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
                    if (this.sendMessageToWayPoint)
                    {
                        this.targetWayPoint.SendMessage("OnWayPointReached", this, SendMessageOptions.DontRequireReceiver);
                    }
                    if (this.autoMoveNext)
                    {
                        NextWayPoint();
                    }
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
                    float delay = this.delayInSeconds;
                    if (this.useWayPointDelay)
                    {
                        delay = this.currentWayPoint.WaitTimeInSeconds;
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

        public void NextWayPoint()
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

        #endregion

    }

}