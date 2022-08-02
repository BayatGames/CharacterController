using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bayat.Games.Characters
{

    /// <summary>
    /// The base class for character abilities.
    /// </summary>
    public abstract class CharacterAbility : MonoBehaviour
    {

        [SerializeField]
        protected CharacterController controller;
        [SerializeField]
        protected CharacterAbilityManager abilityManager;

        protected virtual void Reset()
        {
            this.controller = GetComponentInParent<CharacterController>();
            this.abilityManager = GetComponentInParent<CharacterAbilityManager>();
        }

        /// <summary>
        /// Called early in the Update when processing abilities.
        /// </summary>
        public abstract void EarlyProcessAbility();

        /// <summary>
        /// Called in Update when processing abilities.
        /// </summary>
        public abstract void ProcessAbility();

        /// <summary>
        /// Called late in the Update when processing abilities.
        /// </summary>
        public abstract void LateProcessAbility();

        /// <summary>
        /// Called in LateUpdate when updating Animator parameters.
        /// </summary>
        public abstract void UpdateAnimator();

    }

}