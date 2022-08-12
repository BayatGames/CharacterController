using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bayat.Games.Surfaces
{

    public class SurfaceDefinition : ScriptableObject
    {

        #region Fields

        [SerializeField]
        protected string identifier;

        [SerializeField]
        [Tooltip("Optional")]
        protected string title;
        [SerializeField]
        [Tooltip("Optional")]
        protected string description;

        #endregion

        #region Properties

        public virtual string Identifier
        {
            get
            {
                return this.identifier;
            }
            set
            {
                this.identifier = value;
            }
        }

        public virtual string Title
        {
            get
            {
                return this.title;
            }
            set
            {
                this.description = value;
            }
        }

        public virtual string Description
        {
            get
            {
                return this.description;
            }
            set
            {
                this.description = value;
            }
        }

        #endregion

    }

}