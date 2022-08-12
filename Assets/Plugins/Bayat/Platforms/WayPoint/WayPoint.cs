using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

namespace Bayat.Games.Platforms
{

    public class WayPoint : MonoBehaviour
    {

        #region Fields

        [SerializeField]
        protected float waitTimeInSeconds = 1f;
        [SerializeField]
        protected float speed = 1f;

        [SerializeField]
        protected UnityEvent reachedEvent;

        #endregion

        #region Properties

        public float WaitTimeInSeconds
        {
            get
            {
                return this.waitTimeInSeconds;
            }
        }

        public float Speed
        {
            get
            {
                return this.speed;
            }
        }

        #endregion

        #region Public Methods

        public virtual void OnWayPointReached(WayPointFollower follower)
        {
            this.reachedEvent?.Invoke();
        }

        #endregion

    }

}