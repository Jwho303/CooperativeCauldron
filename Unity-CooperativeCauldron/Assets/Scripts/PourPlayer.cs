//RenderHeads - Jeff Rusch
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RenderHeads
{
    public class PourPlayer : MonoBehaviour
    {
        #region Public Properties
        public KeyCode Up;
        public KeyCode Down;
        public KeyCode Left;
        public KeyCode Right;

        public Transform RotateTarget;
        public float TargetSpeed = 0f;
        public float MaxRotateSpeed = 100f;
        public float SpeedChange = 1f;
        public float DampenSpeed = 10f;
		#endregion

		#region Private Properties

		#endregion

		#region Public Methods
		public void Update()
		{
            JoystickDirection direction = JoystickDirection.None;

            if (Input.GetKey(Up))
            {
                // Debug.Log("Up");
                direction = JoystickDirection.Up;

            }
            else if (Input.GetKey(Down))
            {
                // Debug.Log("Down");
                direction = JoystickDirection.Down;
            }
            if (Input.GetKey(Left))
            {
                //Debug.Log("Left");
                direction = JoystickDirection.Left;
            }
            else if (Input.GetKey(Right))
            {
                // Debug.Log("Right");
                direction = JoystickDirection.Right;
            }

            if (direction == JoystickDirection.Right || direction == JoystickDirection.Left)
			{
                float dir = (direction == JoystickDirection.Left) ? 1f : -1f;

                Rotate(dir, SpeedChange);
            }
            else if (direction == JoystickDirection.Up || direction == JoystickDirection.Down)
			{

			}
            else
			{
                DampenRotation();
			}

            float angle = Time.deltaTime * TargetSpeed;
            RotateTarget.Rotate(Vector3.forward, angle);
        }




		#endregion

		#region Private Methods
		private void Rotate(float dir, float speed)
        {
            TargetSpeed = Mathf.Clamp(TargetSpeed + (speed * dir), -MaxRotateSpeed, MaxRotateSpeed);
        }

        private void DampenRotation()
        {
            TargetSpeed = Mathf.Lerp(TargetSpeed, 0f, Time.deltaTime * DampenSpeed);
        }
        #endregion
    }
}