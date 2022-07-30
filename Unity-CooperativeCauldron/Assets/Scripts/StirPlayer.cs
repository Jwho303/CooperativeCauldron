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

        public enum Direction
        {
            None = 0,
            Up = 1,
            Right = 2,
            Down = 3,
            Left = 4,
        }

        public enum StirDirection
		{
            ClockWise,
            AntiClockWise,
            None
		}


        public List<Direction> DirectionBuffer = new List<Direction>();
        public StirDirection Stir;

        public float TimeoutDuration = 0.25f;
        public float Timer = 0f;
        #endregion

        #region Private Properties

        #endregion

        #region Public Methods
        public void Update()
		{
            Direction direction = Direction.None;

            if (Input.GetKey(Up))
			{
               // Debug.Log("Up");
                direction = Direction.Up;

            }
            else if (Input.GetKey(Down))
            {
               // Debug.Log("Down");
                direction = Direction.Down;
            }
            if (Input.GetKey(Left))
            {
                //Debug.Log("Left");
                direction = Direction.Left;
            }
            else if (Input.GetKey(Right))
            {
               // Debug.Log("Right");
                direction = Direction.Right;
            }

            if (direction != Direction.None)
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

                if (DirectionBuffer.Contains(Direction.Up) && DirectionBuffer.Contains(Direction.Down) && DirectionBuffer.Contains(Direction.Left) && DirectionBuffer.Contains(Direction.Right))
				{
					switch (DirectionBuffer[0])
					{
						case Direction.Up:
                            if (DirectionBuffer[1] == Direction.Right)
							{
                                PassStir(StirDirection.ClockWise);
                            }
                            else if (DirectionBuffer[1] == Direction.Left)
                            {
                                PassStir(StirDirection.AntiClockWise);
                            }
                            else
							{
                                FailStir();
                            }
							break;
						case Direction.Right:
                            if (DirectionBuffer[1] == Direction.Down)
                            {
                                PassStir(StirDirection.ClockWise);
                            }
                            else if (DirectionBuffer[1] == Direction.Up)
                            {
                                PassStir(StirDirection.AntiClockWise);
                            }
                            else
                            {
                                FailStir();
                            }
                            break;
						case Direction.Down:
                            if (DirectionBuffer[1] == Direction.Right)
                            {
                                PassStir(StirDirection.AntiClockWise);
                            }
                            else if (DirectionBuffer[1] == Direction.Left)
                            {
                                PassStir(StirDirection.ClockWise);
                            }
                            else
                            {
                                FailStir();
                            }
                            break;
						case Direction.Left:
                            if (DirectionBuffer[1] == Direction.Up)
                            {
                                PassStir(StirDirection.ClockWise);
                            }
                            else if (DirectionBuffer[1] == Direction.Down)
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