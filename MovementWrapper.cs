using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace platformerThing
{
    public struct MovementWrapper
    {
        public Vector2 MovementToTry { get; private set; }
        public Vector2 FurthestAvailableLocationSoFar { get; set; }
        public int NumberOfStepsToBreakMovementInto { get; private set; }
        public bool IsDiagonalMove { get; private set; }
        public Vector2 OneStep { get; private set; }
        public Rectangle BoundingRectangle { get; set; }

        public MovementWrapper(Vector2 originalPosition, Vector2 destination, Rectangle boundingRectangle) : this()
        {
            MovementToTry = destination - originalPosition;
            FurthestAvailableLocationSoFar = originalPosition;
            NumberOfStepsToBreakMovementInto = (int)(MovementToTry.Length() * 2) + 1;
            IsDiagonalMove = MovementToTry.X != 0 && MovementToTry.Y != 0;
            OneStep = MovementToTry / NumberOfStepsToBreakMovementInto;
            BoundingRectangle = boundingRectangle;
        }
    }
}
