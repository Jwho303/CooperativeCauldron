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
        public Transform[] SpawnPoints;
        public Transform SpawnParent;
        public List<GameObject> IngredientsInPlay = new List<GameObject>();
        public IngredientData[] IngredientDatas;
        public int IngredientIndex = 0;
		#endregion

		#region Private Properties

		#endregion

		#region Public Methods
		public void Start()
		{
            IngredientIndex = UnityEngine.Random.Range(0, IngredientDatas.Length - 1);
            UpdateJar();
		}

		private void UpdateJar()
		{
			for (int i = 0; i < IngredientsInPlay.Count; i++)
			{
                Destroy(IngredientsInPlay[i]);
			}
            IngredientsInPlay.Clear();
            RotateTarget.rotation = Quaternion.identity;
            TargetSpeed = 0;

            for (int i = 0; i < SpawnPoints.Length; i++)
            {
                GameObject go = Instantiate(IngredientDatas[IngredientIndex].GamePrefab, SpawnPoints[i].transform.position, Quaternion.identity, SpawnParent);
                IngredientsInPlay.Add(go);

            }
        }

		public void Update()
		{
            JoystickDirection direction = JoystickDirection.None;

            if (Input.GetKeyDown(Up))
            {
                IngredientIndex++;
                IngredientIndex = Mathf.Abs(IngredientIndex % IngredientDatas.Length);

                UpdateJar();
            }
            else if (Input.GetKeyDown(Down))
            {
                IngredientIndex--;
                if (IngredientIndex<0)
				{
                    IngredientIndex = IngredientDatas.Length - 1;
                }
                UpdateJar();
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