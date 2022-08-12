using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Bayat.Games.Surfaces;

namespace Bayat.Games.Characters
{

    public class CharacterSurfaceController2D : MonoBehaviour
    {

        [SerializeField]
        protected CharacterController2D characterController;
        [SerializeField]
        protected Transform raycastOrigin;
        [SerializeField]
        protected float raycastDistance;
        [SerializeField]
        protected LayerMask surfaceLayer;

        protected SurfaceDefinition previousSurface;
        protected SurfaceDefinition currentSurface;

        protected virtual void Update()
        {
            CheckSurface();
        }

        protected virtual void CheckSurface()
        {
            Surface surface = null;

            // Check for 3D colliders
            if (Physics.Raycast(this.raycastOrigin.position, this.characterController.Down, out var hit, this.raycastDistance, this.surfaceLayer))
            {
                surface = hit.collider.GetComponent<Surface>();
            }

            if (surface == null)
            {

                // Check for 2D colliders
                var hit2D = Physics2D.Raycast(this.raycastOrigin.position, this.characterController.Down, this.raycastDistance, this.surfaceLayer);
                if (hit2D.collider != null)
                {
                    surface = hit2D.collider.GetComponent<Surface>();
                }
            }

            if (surface != null)
            {
                if (this.currentSurface != surface.Definition)
                {
                    this.previousSurface = this.currentSurface;
                    this.currentSurface = surface.Definition;
                }
            }
        }

        public virtual void ProcessSurface()
        {

        }

    }

}
