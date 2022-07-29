using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bayat.Games.Characters
{

    /// <summary>
    /// A wrapper for both <see cref="Rigidbody"/> and <see cref="Rigidbody2D"/> as a unified API.
    /// </summary>
    public class RigidbodyWrapper : MonoBehaviour
    {

        [SerializeField]
        protected new Rigidbody rigidbody;
        [SerializeField]
        protected new Rigidbody2D rigidbody2D;

        public virtual bool Is2D
        {
            get
            {
                return this.rigidbody == null;
            }
        }

        /// <summary>
        /// Gets or sets the velocity property of the corresponding rigidbody.
        /// </summary>
        /// <remarks>
        /// Gets or sets the <see cref="Rigidbody.velocity"/> if it is available, otherwise gets or sets the <see cref="Rigidbody2D.velocity"/>.
        /// </remarks>
        public virtual Vector3 Velocity
        {
            get
            {
                if (this.rigidbody != null)
                {
                    return this.rigidbody.velocity;
                }
                return this.rigidbody2D.velocity;
            }
            set
            {
                if (this.rigidbody != null)
                {
                    this.rigidbody.velocity = value;
                }
                else
                {
                    this.rigidbody2D.velocity = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the is kinematic property of the corresponding rigidbody.
        /// </summary>
        /// <remarks>
        /// Gets or sets the <see cref="Rigidbody.isKinematic"/> if it is available, otherwise gets or sets the <see cref="Rigidbody2D.isKinematic"/>.
        /// </remarks>
        public virtual bool IsKinematic
        {
            get
            {
                if (this.rigidbody != null)
                {
                    return this.rigidbody.isKinematic;
                }
                return this.rigidbody2D.isKinematic;
            }
            set
            {
                if (this.rigidbody != null)
                {
                    this.rigidbody.isKinematic = value;
                }
                else
                {
                    this.rigidbody2D.isKinematic = value;
                }
            }
        }

        protected virtual void Reset()
        {
            this.rigidbody = GetComponent<Rigidbody>();
            this.rigidbody2D = GetComponent<Rigidbody2D>();
        }

        /// <summary>
        /// Calls the MovePosition of the corresponding rigidbody.
        /// </summary>
        /// <remarks>
        /// Calls the <see cref="Rigidbody.MovePosition(Vector3)"/> if it is available, otherwise calls the <see cref="Rigidbody2D.MovePosition(Vector2)"/>.
        /// </remarks>
        /// <param name="position">Provides the new position for the rigidbody object</param>
        public virtual void MovePosition(Vector3 position)
        {
            if (this.rigidbody != null)
            {
                this.rigidbody.MovePosition(position);
            }
            else
            {
                this.rigidbody2D.MovePosition(position);
            }
        }

        /// <summary>
        /// Calls the MoveRotation of the corresponding rigidbody.
        /// </summary>
        /// <remarks>
        /// Calls the <see cref="Rigidbody.MoveRotation(Quaternion)"/> if it is available, otherwise calls the <see cref="Rigidbody2D.MoveRotation(Quaternion)"/>.
        /// </remarks>
        /// <param name="position">The new rotation for the rigidbody object</param>
        public virtual void MoveRotation(Quaternion rotation)
        {
            if (this.rigidbody != null)
            {
                this.rigidbody.MoveRotation(rotation);
            }
            else
            {
                this.rigidbody2D.MoveRotation(rotation);
            }
        }

        /// <summary>
        /// Calls the AddForce of the corresponding rigidbody.
        /// </summary>
        /// <remarks>
        /// Calls the <see cref="Rigidbody.AddForce(Vector3)"/> if it is available, otherwise calls the <see cref="Rigidbody2D.AddForce(Vector2)"/>.
        /// </remarks>
        /// <param name="position">Force vector in world coordinates.</param>
        public virtual void AddForce(Vector3 force)
        {
            if (this.rigidbody != null)
            {
                this.rigidbody.AddForce(force);
            }
            else
            {
                this.rigidbody2D.AddForce(force);
            }
        }

        public virtual void AddForce(Vector3 force, CommonForceMode mode)
        {
            if (this.rigidbody != null)
            {
                this.rigidbody.AddForce(force, (ForceMode)mode);
            }
            else
            {
                this.rigidbody2D.AddForce(force, (ForceMode2D)mode);
            }
        }

        public virtual void AddForce2D(Vector3 force, ForceMode2D mode)
        {
            this.rigidbody2D.AddForce(force, mode);
        }

        public virtual void AddForce3D(Vector3 force, ForceMode mode)
        {
            this.rigidbody.AddForce(force, mode);
        }

    }

    /// <summary>
    /// Common force modes.
    /// </summary>
    /// <remarks>
    /// Provides equivalents to the force modes.
    /// </remarks>
    public enum CommonForceMode
    {

        /// <summary>
        /// This is equivalant to <see cref="ForceMode.Force"/> and <see cref="ForceMode2D.Force"/>
        /// </summary>
        Force = 0,

        /// <summary>
        /// This is equivalant to <see cref="ForceMode.Impulse"/> and <see cref="ForceMode2D.Impulse"/>
        /// </summary>
        Impulse = 1
    }

}