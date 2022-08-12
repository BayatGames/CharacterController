using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Bayat.Games.Characters.Abilities;

namespace Bayat.Games.Characters.Abilities
{

    /// <summary>
    /// The character ability manager.
    /// </summary>
    [AddComponentMenu("Bayat/Games/Characters/Character Ability Manager")]
    public class CharacterAbilityManager : MonoBehaviour
    {

        #region Fields

        [SerializeField]
        protected List<CharacterAbility> abilities = new List<CharacterAbility>();

        protected Dictionary<Type, CharacterAbility> abilitiesLookup = new Dictionary<Type, CharacterAbility>();

        #endregion

        #region Unity Messages

        protected virtual void Update()
        {
            EarlyProcessAbilities();
            ProcessAbilities();
            LateProcessAbilities();
        }

        protected virtual void FixedUpdate()
        {

        }

        protected virtual void LateUpdate()
        {
            UpdateAnimatorAbilities();
        }

        #endregion

        #region Protected Methods

        protected virtual void FetchNewAbilities()
        {
            var allAbilities = GetComponentsInChildren<CharacterAbility>();
            for (int i = 0; i < allAbilities.Length; i++)
            {
                var ability = allAbilities[i];
                if (this.abilities.Contains(ability))
                {
                    continue;
                }
                this.abilities.Add(ability);
            }
        }

        protected virtual void RemoveMissingAbilities()
        {
            var toRemove = new List<int>();
            for (int i = 0; i < this.abilities.Count; i++)
            {
                if (this.abilities[i] == null)
                {
                    toRemove.Add(i);
                }
            }
            foreach (var index in toRemove)
            {
                this.abilities.RemoveAt(index);
            }
        }

        protected virtual void UpdateLookup()
        {
            for (int i = 0; i < this.abilities.Count; i++)
            {
                var ability = this.abilities[i];
                if (ability != null)
                {
                    this.abilitiesLookup[ability.GetType()] = ability;
                }
            }
        }

        protected virtual void EarlyProcessAbilities()
        {
            for (int i = 0; i < this.abilities.Count; i++)
            {
                var ability = this.abilities[i];
                ability.EarlyProcessAbility();
            }
        }

        protected virtual void ProcessAbilities()
        {
            for (int i = 0; i < this.abilities.Count; i++)
            {
                var ability = this.abilities[i];
                ability.ProcessAbility();
            }
        }

        protected virtual void LateProcessAbilities()
        {
            for (int i = 0; i < this.abilities.Count; i++)
            {
                var ability = this.abilities[i];
                ability.LateProcessAbility();
            }
        }

        protected virtual void UpdateAnimatorAbilities()
        {
            for (int i = 0; i < this.abilities.Count; i++)
            {
                var ability = this.abilities[i];
                ability.UpdateAnimator();
            }
        }

        #endregion

        #region Public Methods

        public virtual void UpdateAbilities()
        {
            FetchNewAbilities();
            RemoveMissingAbilities();
            UpdateLookup();
        }

        #endregion

    }

}