using UnityEngine;

namespace Bayat.Games.Physics
{

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