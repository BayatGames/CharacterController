using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bayat.Games.Platforms
{

    public abstract class WayPointFollower : MonoBehaviour, IWayPointFollower
    {

        #region Fields

        [Header("References")]
        [SerializeField]
        protected new Rigidbody2D rigidbody2D;
        [SerializeField]
        protected WayPointPath path;

        [Header("Parameters")]
        [SerializeField]
        protected bool ignoreWayPointSpeed = false;
        [SerializeField]
        protected float speed = 1f;
        [SerializeField]
        protected bool ignoreWayPointWaitTime = false;
        [SerializeField]
        [Tooltip("The wait time for each point before proceeding to the next one.")]
        protected float waitTimeInSeconds = 1f;

        [SerializeField]
        protected int initialWayPointIndex = 0;

        [SerializeField]
        protected bool setPositionOnStart = true;
        [SerializeField]
        protected bool autoMoveNext = true;

        [SerializeField]
        protected bool sendMessageToWayPoint = true;

        protected WayPoint currentWayPoint;

        #endregion

        #region Properties

        public virtual WayPointPath Path
        {
            get
            {
                return this.path;
            }
            set
            {
                this.path = value;
            }
        }

        public virtual float Speed
        {
            get
            {
                return this.speed;
            }
            set
            {
                this.speed = value;
            }
        }

        public virtual float WaitTimeInSeconds
        {
            get
            {
                return this.waitTimeInSeconds;
            }
            set
            {
                this.waitTimeInSeconds = value;
            }
        }

        public virtual bool IgnoreWayPointSpeed
        {
            get
            {
                return this.ignoreWayPointSpeed;
            }
            set
            {
                this.ignoreWayPointSpeed = value;
            }
        }

        public virtual bool IgnoreWayPointWaitTime
        {
            get
            {
                return this.ignoreWayPointWaitTime;
            }
            set
            {
                this.ignoreWayPointWaitTime = value;
            }
        }

        public virtual bool AutoMoveNext
        {
            get
            {
                return this.autoMoveNext;
            }
            set
            {
                this.autoMoveNext = value;
            }
        }

        public virtual WayPoint CurrentWayPoint
        {
            get
            {
                return this.currentWayPoint;
            }
        }

        #endregion

        #region Protected Methods

        protected virtual void OnWayPointReached(WayPoint wayPoint)
        {
            if (this.sendMessageToWayPoint)
            {
                wayPoint.OnWayPointReached(this);
            }
        }

        #endregion

        #region Public Methods

        public abstract void NextWayPoint();

        public abstract void SetGoalWayPoint(int index);

        public abstract void SetGoalWayPoint(WayPoint wayPoint);

        #endregion

    }

}