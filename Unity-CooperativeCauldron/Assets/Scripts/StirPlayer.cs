//RenderHeads - Jeff Rusch
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RenderHeads
{
    public class StirPlayer : MonoBehaviour
    {
        #region Public Properties
        public KeyCode Up;
        public KeyCode Down;
        public KeyCode Left;
        public KeyCode Right;

        public List<JoystickDirection> DirectionBuffer = new List<JoystickDirection>();
        public StirDirection Stir;

        public float TimeoutDuration = 0.25f;
        public float Timer = 0f;
        public SpriteRenderer Visual;
        public Sprite[] Sprites;

        public GameObject[] StateHeads;
        #endregion

        #region Private Properties

        #endregion

        #region Public Methods
        public void Update()
		{
            if (Input.GetKeyDown(KeyCode.Alpha1))
			{
				for (int i = 0; i < StateHeads.Length; i++)
				{
                    StateHeads[i].SetActive(false);
				}

                StateHeads[0].SetActive(true);
			}

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                for (int i = 0; i < StateHeads.Length; i++)
                {
                    StateHeads[i].SetActive(false);
                }

                StateHeads[1].SetActive(true);
            }

            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                for (int i = 0; i < StateHeads.Length; i++)
                {
                    StateHeads[i].SetActive(false);
                }

                StateHeads[2].SetActive(true);
            }


            JoystickDirection direction = JoystickDirection.None;
            Vector3 targetPosition = Vector3.zero;

            if (Input.GetKey(Up))
			{
               // Debug.Log("Up");
                direction = JoystickDirection.Up;
                targetPosition = new Vector3(0f, 1f, 0f);
            }
            else if (Input.GetKey(Down))
            {
               // Debug.Log("Down");
                direction = JoystickDirection.Down;
                targetPosition = new Vector3(0f, -1f, 0f);
            }
            if (Input.GetKey(Left))
            {
                //Debug.Log("Left");
                direction = JoystickDirection.Left;
                targetPosition = new Vector3(-1, targetPosition.y, 0f);
            }
            else if (Input.GetKey(Right))
            {
               // Debug.Log("Right");
                direction = JoystickDirection.Right;
                targetPosition = new Vector3(1, targetPosition.y, 0f);
            }

            if (targetPosition == Vector3.zero)
			{
                Visual.sprite = Sprites[0];
			}
            else if (targetPosition == new Vector3(1f,-1f,0))
			{
                Visual.sprite = Sprites[1];
			}
            else if (targetPosition == new Vector3(0f, -1f, 0))
            {
                Visual.sprite = Sprites[2];
            }
            else if (targetPosition == new Vector3(-1f, -1f, 0))
            {
                Visual.sprite = Sprites[3];
            }
            else if (targetPosition == new Vector3(-1f, 0f, 0))
            {
                Visual.sprite = Sprites[4];
            }
            else if (targetPosition == new Vector3(-1f, 1f, 0))
            {
                Visual.sprite = Sprites[5];
            }
            else if (targetPosition == new Vector3(0f, 1f, 0))
            {
                Visual.sprite = Sprites[6];
            }
            else if (targetPosition == new Vector3(1f, 1f, 0))
            {
                Visual.sprite = Sprites[7];
            }
            else if (targetPosition == new Vector3(1f, 0f, 0))
            {
                Visual.sprite = Sprites[8];
            }

            if (direction != JoystickDirection.None)
			{
                if (!DirectionBuffer.Contains(direction))
				{
                    DirectionBuffer.Add(direction);
                    UpdateTimer();
                }

                if (Time.unscaledTime >= Timer)
				{
                    FailStir();
				}

                if (DirectionBuffer.Contains(JoystickDirection.Up) && DirectionBuffer.Contains(JoystickDirection.Down) && DirectionBuffer.Contains(JoystickDirection.Left) && DirectionBuffer.Contains(JoystickDirection.Right))
				{
					switch (DirectionBuffer[0])
					{
						case JoystickDirection.Up:
                            if (DirectionBuffer[1] == JoystickDirection.Right)
							{
                                PassStir(StirDirection.ClockWise);
                            }
                            else if (DirectionBuffer[1] == JoystickDirection.Left)
                            {
                                PassStir(StirDirection.AntiClockWise);
                            }
                            else
							{
                                FailStir();
                            }
							break;
						case JoystickDirection.Right:
                            if (DirectionBuffer[1] == JoystickDirection.Down)
                            {
                                PassStir(StirDirection.ClockWise);
                            }
                            else if (DirectionBuffer[1] == JoystickDirection.Up)
                            {
                                PassStir(StirDirection.AntiClockWise);
                            }
                            else
                            {
                                FailStir();
                            }
                            break;
						case JoystickDirection.Down:
                            if (DirectionBuffer[1] == JoystickDirection.Right)
                            {
                                PassStir(StirDirection.AntiClockWise);
                            }
                            else if (DirectionBuffer[1] == JoystickDirection.Left)
                            {
                                PassStir(StirDirection.ClockWise);
                            }
                            else
                            {
                                FailStir();
                            }
                            break;
						case JoystickDirection.Left:
                            if (DirectionBuffer[1] == JoystickDirection.Up)
                            {
                                PassStir(StirDirection.ClockWise);
                            }
                            else if (DirectionBuffer[1] == JoystickDirection.Down)
                            {
                                PassStir(StirDirection.AntiClockWise);
                            }
                            else
                            {
                                FailStir();
                            }
                            break;
						default:
							break;
					}



					DirectionBuffer.Clear();
				}
			}
			else
			{
                if (DirectionBuffer.Count > 0)
                {
                    FailStir();
                }
            }



        }
		#endregion

		#region Private Methods
		private void FailStir()
		{
            Debug.Log("Fail Stir");
            Stir = StirDirection.None;
            DirectionBuffer.Clear();
            ClearTimer();
        }

        private void PassStir(StirDirection stirDirection)
		{
            Debug.Log("Passed Stir " + stirDirection);
            Stir = stirDirection;
            DirectionBuffer.Clear();
            ClearTimer();
        }

        private void UpdateTimer()
		{
            Timer = Time.unscaledTime + TimeoutDuration;
		}

        private void ClearTimer()
		{
            Timer = -1f;
		}

		#endregion
	}
}