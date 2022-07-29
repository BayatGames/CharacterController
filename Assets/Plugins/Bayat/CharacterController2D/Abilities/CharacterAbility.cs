using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bayat.Games.Characters.Controller2D
{

    public class CharacterAbility : MonoBehaviour
    {

        [SerializeField]
        protected CharacterController2D controller;

        private void Reset()
        {
            this.controller = GetComponentInParent<CharacterController2D>();
        }

    }

}