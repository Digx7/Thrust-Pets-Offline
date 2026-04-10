using UnityEngine;

namespace Digx7.Zygote
{
    [RequireComponent(typeof(Movement3D))]
    public class PlayerCharacter3D : PlayerCharacter
    {
        #region Variables ================================

        private Movement3D movement3D;

        #endregion

        #region Setup ================================

        protected override void Awake()
        {
            movement3D = gameObject.GetComponent<Movement3D>();
        }

        #endregion

        #region Main Functions ================================

        public override void UpdateDesiredMoveDirection(Vector2 newDesiredDirection)
        {
            Debug.Log("PlayerCharacter3D: UpdateDesiredMoveDirection( " + newDesiredDirection + ")");
            base.UpdateDesiredMoveDirection(newDesiredDirection);
            movement3D.setDesiredMoveDirection(desiredMoveDirection);
        }

        public override void Jump()
        {
            Debug.Log("PlayerCharacter3D: Jump()");
            base.Jump();
            movement3D.tryToJump();
        }

        #endregion
    }
}