//RenderHeads - Jeff Rusch
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RenderHeads
{
    public class GameManager : MonoBehaviour
    {
        #region Public Properties
        public PromptController PromptController;
		public AudioSource AudioSource;
		public IngredientData[] IngredientDatas;
		#endregion

		#region Private Properties
		private int requestNumber = 0;
		private RequestType RequestType = RequestType.Stir;
		private StirDirection StirDirection;
		private int RequestedIngredientIndex;

		private float gameTickDuration = 5f;
		private float gameTickTimer;

		private Queue<AudioClip> audioClips = new Queue<AudioClip>();
		#endregion

		#region Public Methods
		public void Update()
		{
			if (Time.unscaledTime > gameTickTimer)
			{
				gameTickTimer = Time.unscaledTime + gameTickDuration;
				GenerateRequest();
			}
		}
		#endregion

		#region Private Methods
		private void GenerateRequest()
		{
			int requestRandom = Random.Range(0, 2);
			requestNumber = Random.Range(1, 4);

			if (requestRandom == 1)
			{
				RequestType = RequestType.Ingredient;
				RequestedIngredientIndex = Random.Range(0, IngredientDatas.Length);
				PromptController.ShowIngredientRequest(requestNumber, IngredientDatas[RequestedIngredientIndex]);
			}
			else
			{
				RequestType = RequestType.Stir;
				int stirRandom = Random.Range(0, 2);

				if (requestRandom == 0)
				{
					StirDirection = StirDirection.ClockWise;
				}
				else
				{
					StirDirection = StirDirection.AntiClockWise;
				}

				PromptController.ShowStirRequest(requestNumber, StirDirection);
			}

			
		}
		#endregion
	}
}