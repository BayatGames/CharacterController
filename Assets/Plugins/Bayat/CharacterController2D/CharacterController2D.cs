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
        protected RigidbodyWrapper rigidbodyWrapper;
        [SerializeField]
        protected ColliderWrapper colliderWrapper;

        private void Reset()
        {
            this.abilityManager = GetComponentInParent<CharacterAbilityManager>();
        }

    }

}