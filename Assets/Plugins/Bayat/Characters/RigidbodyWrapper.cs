using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bayat.Games.Characters
{

    /// <summary>
    /// A wrapper for both <see cref="Rigidbody"/> and <see cref="Rigidbody2D"/> as a unified API.
    /// </summary>
    [AddComponentMenu("Bayat/Games/Rigidbody Wrapper")]
    public class RigidbodyWrapper : MonoBehaviour
    {

        #region Fields

        [SerializeField]
        protected new Rigidbody rigidbody;
        [SerializeField]
        protected new Rigidbody2D rigidbody2D;

        #endregion

        #region Properties

        public virtual Rigidbody Rigidbody
        {
            get
            {
                return this.rigidbody;
            }
        }

        public virtual Rigidbody2D Rigidbody2D
        {
            get
            {
                return this.rigidbody2D;
            }
        }

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

        public virtual float Mass
        {
            get
            {
                if (this.rigidbody != null)
                {
                    return this.rigidbody.mass;
                }
                return this.rigidbody2D.mass;
            }
            set
            {
                if (this.rigidbody != null)
                {
                    this.rigidbody.mass = value;
                }
                else
                {
                    this.rigidbody2D.mass = value;
                }
            }
        }

        public virtual Vector3 Position
        {
            get
            {
                if (this.rigidbody != null)
                {
                    return this.rigidbody.position;
                }
                return this.rigidbody2D.position;
            }
            set
            {
                if (this.rigidbody != null)
                {
                    this.rigidbody.position = value;
                }
                else
                {
                    this.rigidbody2D.position = value;
                }
            }
        }

        public virtual float AngularDrag
        {
            get
            {
                if (this.rigidbody != null)
                {
                    return this.rigidbody.angularDrag;
                }
                return this.rigidbody2D.angularDrag;
            }
            set
            {
                if (this.rigidbody != null)
                {
                    this.rigidbody.angularDrag = value;
                }
                else
                {
                    this.rigidbody2D.angularDrag = value;
                }
            }
        }

        public virtual Vector3 CenterOfMass
        {
            get
            {
                if (this.rigidbody != null)
                {
                    return this.rigidbody.centerOfMass;
                }
                return this.rigidbody2D.centerOfMass;
            }
            set
            {
                if (this.rigidbody != null)
                {
                    this.rigidbody.centerOfMass = value;
                }
                else
                {
                    this.rigidbody2D.centerOfMass = value;
                }
            }
        }

        public virtual Vector3 WorldCenterOfMass
        {
            get
            {
                if (this.rigidbody != null)
                {
                    return this.rigidbody.worldCenterOfMass;
                }
                else
                {
                    return this.rigidbody2D.worldCenterOfMass;
                }
            }
        }

        public virtual float Drag
        {
            get
            {
                if (this.rigidbody != null)
                {
                    return this.rigidbody.drag;
                }
                else
                {
                    return this.rigidbody2D.drag;
                }
            }
            set
            {
                if (this.rigidbody != null)
                {
                    this.rigidbody.drag = value;
                }
                else
                {
                    this.rigidbody2D.drag = value;
                }
            }
        }

        public virtual CommonRigidbodyInterpolation Interpolation
        {
            get
            {
                if (this.rigidbody != null)
                {
                    return (CommonRigidbodyInterpolation)this.rigidbody.interpolation;
                }
                return (CommonRigidbodyInterpolation)this.rigidbody2D.interpolation;
            }
            set
            {
                if (this.rigidbody != null)
                {
                    this.rigidbody.interpolation = (RigidbodyInterpolation)value;
                }
                else
                {
                    this.rigidbody2D.interpolation = (RigidbodyInterpolation2D)value;
                }
            }
        }

        public virtual bool FreezeRotation
        {
            get
            {
                if (this.rigidbody != null)
                {
                    return this.rigidbody.freezeRotation;
                }
                return this.rigidbody2D.freezeRotation;
            }
            set
            {
                if (this.rigidbody != null)
                {
                    this.rigidbody.freezeRotation = value;
                }
                else
                {
                    this.rigidbody2D.freezeRotation = value;
                }
            }
        }

        #endregion

        #region Unity Messages

        protected virtual void Reset()
        {
            this.rigidbody = GetComponent<Rigidbody>();
            this.rigidbody2D = GetComponent<Rigidbody2D>();
            if (this.rigidbody == null && this.rigidbody2D == null)
            {
                Debug.LogWarning("There are no rigidbodies available on this game object.");
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Moves the rigidbody to the <paramref name="position"/>.
        /// </summary>
        /// <remarks>
        /// Calls the <see cref="Rigidbody.MovePosition(Vector3)"/> if it is available, otherwise calls the <see cref="Rigidbody2D.MovePosition(Vector2)"/>.
        /// </remarks>
        /// <param name="position">Provides the new position for the rigidbody object.</param>
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
        /// Rotates the rigidbody to the <paramref name="rotation"/>.
        /// </summary>
        /// <remarks>
        /// Calls the <see cref="Rigidbody.MoveRotation(Quaternion)"/> if it is available, otherwise calls the <see cref="Rigidbody2D.MoveRotation(Quaternion)"/>.
        /// </remarks>
        /// <param name="position">The new rotation for the rigidbody object.</param>
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
        /// Add a force to the rigidbody.
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

        public virtual void AddRelativeForce(Vector3 force)
        {
            if (this.rigidbody != null)
            {
                this.rigidbody.AddRelativeForce(force);
            }
            else
            {
                this.rigidbody2D.AddRelativeForce(force);
            }
        }

        public virtual void AddRelativeForce(Vector3 force, CommonForceMode mode)
        {
            if (this.rigidbody != null)
            {
                this.rigidbody.AddRelativeForce(force, (ForceMode)mode);
            }
            else
            {
                this.rigidbody2D.AddRelativeForce(force, (ForceMode2D)mode);
            }
        }

        public virtual void WakeUp()
        {
            if (this.rigidbody != null)
            {
                this.rigidbody.WakeUp();
            }
            else
            {
                this.rigidbody2D.WakeUp();
            }
        }

        public virtual void Sleep()
        {
            if (this.rigidbody != null)
            {
                this.rigidbody.Sleep();
            }
            else
            {
                this.rigidbody2D.Sleep();
            }
        }

        public virtual bool IsSleeping()
        {
            if (this.rigidbody != null)
            {
                return this.rigidbody.IsSleeping();
            }
            else
            {
                return this.rigidbody2D.IsSleeping();
            }
        }

        public virtual Vector3 GetPointVelocity(Vector3 point)
        {
            if (this.rigidbody != null)
            {
                return this.rigidbody.GetPointVelocity(point);
            }
            else
            {
                return this.rigidbody2D.GetPointVelocity(point);
            }
        }

        public virtual Vector3 GetRelativePointVelocity(Vector3 relativePoint)
        {
            if (this.rigidbody != null)
            {
                return this.rigidbody.GetRelativePointVelocity(relativePoint);
            }
            else
            {
                return this.rigidbody2D.GetRelativePointVelocity(relativePoint);
            }
        }

        #endregion

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
        /// Adds a continuous force to the rigidbody, using its mass.
        /// </summary>
        /// <remarks>
        /// This is equivalant to <see cref="ForceMode.Force"/> and <see cref="ForceMode2D.Force"/>
        /// </remarks>
        Force = 0,

        /// <summary>
        /// Add an instant force impulse to the rigidbody, using its mass.
        /// </summary>
        /// <remarks>
        /// This is equivalant to <see cref="ForceMode.Impulse"/> and <see cref="ForceMode2D.Impulse"/>
        /// </remarks>
        Impulse = 1
    }

    /// <summary>
    /// Common rigidbody interpolation modes.
    /// </summary>
    /// <remarks>
    /// Provides equivalents to the interpolation modes.
    /// </remarks>
    public enum CommonRigidbodyInterpolation
    {

        /// <summary>
        /// No interpolation.
        /// </summary>
        /// <remarks>
        /// This is equivalant to <see cref="RigidbodyInterpolation.None"/> and <see cref="RigidbodyInterpolation2D.None"/>
        /// </remarks>
        None = 0,

        /// <summary>
        /// Smooth movement based on the object's positions in previous frames.
        /// </summary>
        /// <remarks>
        /// This is equivalant to <see cref="RigidbodyInterpolation.Interpolate"/> and <see cref="RigidbodyInterpolation2D.Interpolate"/>
        /// </remarks>
        Interpolate = 1,

        /// <summary>
        /// Smooth an object's movement based on an estimate of its position in the next frame.
        /// </summary>
        /// <remarks>
        /// This is equivalant to <see cref="RigidbodyInterpolation.Extrapolate"/> and <see cref="RigidbodyInterpolation2D.Extrapolate"/>
        /// </remarks>
        Extrapolate = 2

    }

}