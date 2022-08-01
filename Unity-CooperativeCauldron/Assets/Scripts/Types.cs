//RenderHeads - Jeff Rusch
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RenderHeads
{
    public enum JoystickDirection
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

    public enum IngredientType
    {
        Tail,
        Eyeball,
        Skull,
        Claw
    }

    public enum RequestType
        {
        Stir,
        Ingredient,
        }
}