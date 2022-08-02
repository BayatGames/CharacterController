using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bayat.Games.Characters.Controller
{

    [AddComponentMenu("Bayat/Games/Characters/Character Controller")]
    public class CharacterController : MonoBehaviour
    {

        [SerializeField]
        protected CharacterAbilityManager abilityManager;

        [SerializeField]
        protected RigidbodyWrapper rigidbodyWrapper;
        [SerializeField]
        protected ColliderWrapper colliderWrapper;

        private void Reset()
        {
            this.rigidbodyWrapper = GetComponent<RigidbodyWrapper>();
            this.colliderWrapper = GetComponent<ColliderWrapper>();
            this.abilityManager = GetComponentInChildren<CharacterAbilityManager>();
        }

    }

}