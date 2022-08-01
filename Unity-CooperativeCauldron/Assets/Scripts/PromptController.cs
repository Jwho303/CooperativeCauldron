//RenderHeads - Jeff Rusch
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RenderHeads
{
    public class PromptController : MonoBehaviour
    {
        #region Public Properties
        public SpriteRenderer NumberSprite;
        public SpriteRenderer RequestSprite;

        public Sprite[] Numbers;
        public Sprite[] Direction;
        #endregion

        #region Private Properties

        #endregion

        #region Public Methods
        public void ShowIngredientRequest(int number, IngredientData ingredientData)
		{
            NumberSprite.sprite = Numbers[number - 1];
            RequestSprite.sprite = ingredientData.Sprite;
		}

        public void ShowStirRequest(int number, StirDirection stirDirection)
        {
            NumberSprite.sprite = Numbers[number - 1];
            RequestSprite.sprite = (stirDirection == StirDirection.ClockWise)? Direction[0]: Direction[1];
        }
        #endregion

        #region Private Methods

        #endregion
    }
}