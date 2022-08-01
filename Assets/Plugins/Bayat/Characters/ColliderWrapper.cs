using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bayat.Games.Characters
{

    /// <summary>
    /// A wrapper for both 3D and 2D collider that provides unified API and events.
    /// </summary>
    /// <remarks>
    /// The events are called as Any because they're called if any collision occurs in 2D or 3D through their distinctive events, and then calls the corresponding Any event.
    /// </remarks>
    public class ColliderWrapper : MonoBehaviour
    {

        #region Fields

        [SerializeField]
        protected new Collider collider;
        [SerializeField]
        protected new Collider2D collider2D;

        [SerializeField]
        protected bool sendCollisionMessages = true;

        protected IColliderWrapperListener[] listeners = new IColliderWrapperListener[0];

        #endregion

        #region Properties

        /// <summary>
        /// Gets the 3D collider.
        /// </summary>
        public virtual Collider Collider
        {
            get
            {
                return this.collider;
            }
        }

        /// <summary>
        /// Gets the 2D collider.
        /// </summary>
        public virtual Collider2D Collider2D
        {
            get
            {
                return this.collider2D;
            }
        }

        /// <summary>
        /// Gets or sets whether to send collision messages using <see cref="GameObject.SendMessage(string, object, SendMessageOptions)"/>.
        /// </summary>
        /// <remarks>
        /// If this is set to false, it'll use the <see cref="IColliderWrapperListener"/> to send events.
        /// </remarks>
        public virtual bool SendCollisionMessages
        {
            get
            {
                return this.sendCollisionMessages;
            }
            set
            {
                this.sendCollisionMessages = value;
            }
        }

        /// <summary>
        /// Is the collider a trigger?
        /// </summary>
        public virtual bool IsTrigger
        {
            get
            {
                if (this.collider != null)
                {
                    return this.collider.isTrigger;
                }
                return this.collider2D.isTrigger;
            }
            set
            {
                if (this.collider != null)
                {
                    this.collider.isTrigger = value;
                }
                else
                {
                    this.collider2D.isTrigger = value;
                }
            }
        }

        /// <summary>
        /// The world space bounding area/volume of the collider (Read Only).
        /// </summary>
        public virtual Bounds Bounds
        {
            get
            {
                if (this.collider != null)
                {
                    return this.collider.bounds;
                }
                return this.collider2D.bounds;
            }
        }

        #endregion

        #region Unity Messages

        protected virtual void Reset()
        {
            this.collider = GetComponent<Collider>();
            this.collider2D = GetComponent<Collider2D>();
        }

        #endregion

        #region Trigger Messages

        protected virtual void OnTriggerEnter(Collider other)
        {
            TriggerEnterAny(other.gameObject);
        }

        protected virtual void OnTriggerEnter2D(Collider2D collision)
        {
            TriggerEnterAny(collision.gameObject);
        }

        protected virtual void TriggerEnterAny(GameObject other)
        {
            if (this.sendCollisionMessages)
            {
                SendMessage("OnTriggerEnterAny", other, SendMessageOptions.DontRequireReceiver);
            }
            else
            {
                for (int i = 0; i < this.listeners.Length; i++)
                {
                    var listener = this.listeners[i];
                    listener.OnTriggerEnterAny(other);
                }
            }
        }

        protected virtual void OnTriggerStay(Collider other)
        {
            TriggerStayAny(other.gameObject);
        }

        protected virtual void OnTriggerStay2D(Collider2D collision)
        {
            TriggerStayAny(collision.gameObject);
        }

        protected virtual void TriggerStayAny(GameObject other)
        {
            if (this.sendCollisionMessages)
            {
                SendMessage("OnTriggerStayAny", other, SendMessageOptions.DontRequireReceiver);
            }
            else
            {
                for (int i = 0; i < this.listeners.Length; i++)
                {
                    var listener = this.listeners[i];
                    listener.OnTriggerStayAny(other);
                }
            }
        }

        protected virtual void OnTriggerExit(Collider other)
        {
            TriggerExitAny(other.gameObject);
        }

        protected virtual void OnTriggerExit2D(Collider2D collision)
        {
            TriggerExitAny(collision.gameObject);
        }

        protected virtual void TriggerExitAny(GameObject other)
        {
            if (this.sendCollisionMessages)
            {
                SendMessage("OnTriggerExitAny", other, SendMessageOptions.DontRequireReceiver);
            }
            else
            {
                for (int i = 0; i < this.listeners.Length; i++)
                {
                    var listener = this.listeners[i];
                    listener.OnTriggerExitAny(other);
                }
            }
        }

        #endregion

        #region Collision Messages

        protected virtual void OnCollisionEnter(Collision collision)
        {
            CollisionEnterAny(collision.gameObject, collision, null);
        }

        protected virtual void OnCollisionEnter2D(Collision2D collision)
        {
            CollisionEnterAny(collision.gameObject, null, collision);
        }

        protected virtual void CollisionEnterAny(GameObject other, Collision collision, Collision2D collision2D)
        {
            if (this.sendCollisionMessages)
            {
                SendColliderMessage("OnCollisionEnterAny", other);
            }
            else
            {
                for (int i = 0; i < this.listeners.Length; i++)
                {
                    var listener = this.listeners[i];
                    listener.OnCollisionEnterAny(other, collision, collision2D);
                }
            }
        }

        protected virtual void OnCollisionStay(Collision collision)
        {
            CollisionStayAny(collision.gameObject, collision, null);
        }

        protected virtual void OnCollisionStay2D(Collision2D collision)
        {
            CollisionStayAny(collision.gameObject, null, collision);
        }

        protected virtual void CollisionStayAny(GameObject other, Collision collision, Collision2D collision2D)
        {
            if (this.sendCollisionMessages)
            {
                SendColliderMessage("OnCollisionStayAny", other);
            }
            else
            {
                for (int i = 0; i < this.listeners.Length; i++)
                {
                    var listener = this.listeners[i];
                    listener.OnCollisionStayAny(other, collision, collision2D);
                }
            }
        }

        protected virtual void OnCollisionExit(Collision collision)
        {
            CollisionExitAny(collision.gameObject, collision, null);
        }

        protected virtual void OnCollisionExit2D(Collision2D collision)
        {
            CollisionExitAny(collision.gameObject, null, collision);
        }

        protected virtual void CollisionExitAny(GameObject other, Collision collision, Collision2D collision2D)
        {
            if (this.sendCollisionMessages)
            {
                SendColliderMessage("OnCollisionExitAny", collision.gameObject);
            }
            else
            {
                for (int i = 0; i < this.listeners.Length; i++)
                {
                    var listener = this.listeners[i];
                    listener.OnCollisionExitAny(collision.gameObject, collision, collision2D);
                }
            }
        }

        #endregion

        #region Methods

        protected virtual void SendColliderMessage(string methodName, GameObject other)
        {
            SendMessage(methodName, other, SendMessageOptions.DontRequireReceiver);
        }

        /// <summary>
        /// Fetches the listeners for the collider wrapper on this game object.
        /// </summary>
        /// <remarks>
        /// This gets all the components that implement the <see cref="IColliderWrapperListener"/> interface and re-initializes the internal array of listeners.
        ///
        /// You can use this to update the array of listeners if you have added a new component at runtime and would like to receive events through the interface, otherwise you can juse use the <see cref="sendCollisionMessages"/>.
        /// </remarks>
        public virtual void FetchListeners()
        {
            this.listeners = GetComponents<IColliderWrapperListener>();
        }

        /// <summary>
        /// Returns a point on the collider that is closest to the specified <paramref name="position"/>.
        /// </summary>
        /// <param name="position">Position you want to find the closest point to.</param>
        /// <returns>The point on the collider that is closest to the specified <paramref name="position"/></returns>
        public virtual Vector3 ClosestPoint(Vector3 position)
        {
            if (this.collider != null)
            {
                return this.collider.ClosestPoint(position);
            }
            return this.collider2D.ClosestPoint(position);
        }

        #endregion

    }

    /// <summary>
    /// A listener interface to receive the events from the collider wrapper for both Trigger and Collision event types.
    /// </summary>
    /// <remarks>
    /// To receive calls to these interfaces, make sure to set the <see cref="ColliderWrapper.sendCollisionMessages"/> to false.
    /// </remarks>
    public interface IColliderWrapperListener
    {

        /// <summary>
        /// This is called when either a 3D or 2D trigger enter occurs.
        /// </summary>
        /// <param name="other">The colliding game object.</param>
        void OnTriggerEnterAny(GameObject other);

        /// <summary>
        /// This is called when either a 3D or 2D trigger stay occurs.
        /// </summary>
        /// <param name="other">The colliding game object.</param>
        void OnTriggerStayAny(GameObject other);

        /// <summary>
        /// This is called when either a 3D or 2D trigger exit occurs.
        /// </summary>
        /// <param name="other">The colliding game object.</param>
        void OnTriggerExitAny(GameObject other);

        /// <summary>
        /// This is called when either a 3D or 2D collision enter occurs.
        /// </summary>
        /// <param name="other">The colliding game object.</param>
        /// <param name="collision">The 3D collision info, it'll be null if it is a 2D collision.</param>
        /// <param name="collision2D">The 2D collision info, it'll be null if it is a 3D collision.</param>
        void OnCollisionEnterAny(GameObject other, Collision collision, Collision2D collision2D);

        /// <summary>
        /// This is called when either a 3D or 2D collision stay occurs.
        /// </summary>
        /// <param name="other">The colliding game object.</param>
        /// <param name="collision">The 3D collision info, it'll be null if it is a 2D collision.</param>
        /// <param name="collision2D">The 2D collision info, it'll be null if it is a 3D collision.</param>
        void OnCollisionStayAny(GameObject other, Collision collision, Collision2D collision2D);

        /// <summary>
        /// This is called when either a 3D or 2D collision exit occurs.
        /// </summary>
        /// <param name="other">The colliding game object.</param>
        /// <param name="collision">The 3D collision info, it'll be null if it is a 2D collision.</param>
        /// <param name="collision2D">The 2D collision info, it'll be null if it is a 3D collision.</param>
        void OnCollisionExitAny(GameObject other, Collision collision, Collision2D collision2D);

    }

}