//RenderHeads - Jeff Rusch
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RenderHeads
{
    [CreateAssetMenu(fileName = "IngredientData", menuName = "IngredientData")]
    public class IngredientData : ScriptableObject
    {
        #region Public Properties
        public IngredientType IngredientType;
        public GameObject GamePrefab;
        public AudioClip SFX;
        public Sprite Sprite;
        #endregion

        #region Private Properties

        #endregion

        #region Public Methods

        #endregion

        #region Private Methods

        #endregion
    }
}