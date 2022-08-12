using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bayat.Games.Characters.Abilities
{

    /// <summary>
    /// The base class for character abilities.
    /// </summary>
    public abstract class CharacterAbility : MonoBehaviour
    {

        #region Fields

        [SerializeField]
        protected CharacterController controller;
        [SerializeField]
        protected CharacterAbilityManager abilityManager;

        #endregion

        #region Unity Messages

        protected virtual void Reset()
        {
            this.controller = GetComponentInParent<CharacterController>();
            this.abilityManager = GetComponentInParent<CharacterAbilityManager>();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Called early in the Update when processing abilities.
        /// </summary>
        public virtual void EarlyProcessAbility() { }

        /// <summary>
        /// Called in Update when processing abilities.
        /// </summary>
        public virtual void ProcessAbility() { }

        /// <summary>
        /// Called late in the Update when processing abilities.
        /// </summary>
        public virtual void LateProcessAbility() { }

        /// <summary>
        /// Called in LateUpdate when updating Animator parameters.
        /// </summary>
        public virtual void UpdateAnimator() { }

        #endregion

    }

}