using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace Bayat.Games.Platforms
{

    public class WayPoint : MonoBehaviour
    {

        [SerializeField]
        protected float delayInSeconds = 1f;
        [SerializeField]
        protected float speed = 1f;

        public float DelayInSeconds
        {
            get
            {
                return this.delayInSeconds;
            }
        }

        public float Speed
        {
            get
            {
                return this.speed;
            }
        }

    }

}