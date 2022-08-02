using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bayat.Games.Surfaces
{

    public class SurfaceDefinition : ScriptableObject
    {

        [SerializeField]
        [Tooltip("Optional")]
        protected string title;
        [SerializeField]
        [Tooltip("Optional")]
        protected string description;

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

    }

}