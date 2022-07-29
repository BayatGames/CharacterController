using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bayat.Games.Characters.Controller2D
{

    public class CharacterController2D : MonoBehaviour
    {

        [SerializeField]
        protected CharacterAbilityManager abilityManager;

        [SerializeField]
        protected new Rigidbody2D rigidbody2D;
        [SerializeField]
        protected new Collider2D collider2D;

        private void Reset()
        {
            this.abilityManager = GetComponentInParent<CharacterAbilityManager>();
        }

    }

}