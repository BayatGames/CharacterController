using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Bayat.Games.Surfaces;

namespace Bayat.Games.Characters
{

    public class CharacterSurfaceController2D : MonoBehaviour
    {

        protected SurfaceDefinition previousSurface;
        protected SurfaceDefinition currentSurface;

        protected virtual void Update()
        {
            CheckSurface();
        }

        protected virtual void CheckSurface()
        {
            Physics2D.Raycast();
        }

        public virtual void ProcessSurface()
        {

        }

    }

}
