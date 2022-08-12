using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Bayat.Games.Physics;

using Bayat.Games.Characters.Abilities;

namespace Bayat.Games.Characters
{

    /// <summary>
    /// The character controller.
    /// </summary>
    public abstract class CharacterController : MonoBehaviour
    {

        #region Fields

        [SerializeField]
        protected CharacterAbilityManager abilityManager;

        protected RigidbodyWrapper rigidbodyWrapper;

        #endregion

        #region

        public virtual Vector3 Velocity
        {
            get
            {
                return this.rigidbodyWrapper.Velocity;
            }
            set
            {
                this.rigidbodyWrapper.Velocity = value;
            }
        }

        #endregion

        #region Unity Messages

        protected virtual void Reset()
        {
            this.abilityManager = GetComponentInChildren<CharacterAbilityManager>();
        }

        #endregion

    }

}