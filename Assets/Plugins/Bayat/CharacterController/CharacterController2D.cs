using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bayat.Games.Characters
{

    [AddComponentMenu("Bayat/Games/Characters/Character Controller 2D")]
    public class CharacterController2D : MonoBehaviour
    {

        #region Fields

        [SerializeField]
        protected new Rigidbody2D rigidbody2D;
        [SerializeField]
        protected Vector2 down = Vector2.down;

        protected Vector2 velocity;

        #endregion

        #region Properties

        public virtual Vector2 Velocity
        {
            get
            {
                return this.velocity;
            }
            set
            {
                this.velocity = value;
            }
        }

        public virtual Vector2 Down
        {
            get
            {
                return this.down;
            }
            set
            {
                this.down = value;
            }
        }

        #endregion

        #region Unity Messages

        protected virtual void FixedUpdate()
        {
            this.rigidbody2D.velocity = velocity;
        }

        #endregion

    }

}