using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bayat.Games.Surfaces
{

    public class Surface : MonoBehaviour
    {

        #region Fields

        [SerializeField]
        protected SurfaceDefinition definition;

        #endregion

        #region Properties

        public virtual SurfaceDefinition Definition
        {
            get
            {
                return this.definition;
            }
        }

        #endregion

    }

}