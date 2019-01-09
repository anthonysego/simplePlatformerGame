using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace platformerThing
{
    public class Player : Sprite
    {
        public Vector2 Movement { get; set; }
        private Vector2 oldPosition;

        public Player(Texture2D texture, Vector2 position, SpriteBatch batch) : base(texture, position, batch)
        {
        }

        public void Update (GameTime gameTime)
        {
            CheckKeyboardAndUpdateMovement();
            AffectWithGravity();
            SimulateFriction();
            MoveAsFarAsPossible(gameTime);
            StopMovingIfBlocked();
        }

        private void CheckKeyboardAndUpdateMovement()
        {
            KeyboardState keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.A)) { Movement += new Vector2(-1, 0); }
            if (keyboardState.IsKeyDown(Keys.D)) { Movement += new Vector2(1, 0); }
            if (keyboardState.IsKeyDown(Keys.Space) && IsOnFirmGround()) { Movement = -Vector2.UnitY * 20; }
        }

        private void AffectWithGravity()
        {
            Movement += Vector2.UnitY * .65f;
        }

        private void SimulateFriction()
        {
            if (IsOnFirmGround()) { Movement -= Movement * Vector2.One * .08f; }
            else { Movement -= Movement * Vector2.One * .02f; }
        }

        private void MoveAsFarAsPossible(GameTime gameTime)
        {
            oldPosition = Position;
            UpdatePositionBasedOnMovement(gameTime);
            Position = Board.CurrentBoard.WhereCanIGetTo(oldPosition, Position, Bounds);
        }

        private void UpdatePositionBasedOnMovement(GameTime gameTime)
        {
            Position += Movement * (float)gameTime.ElapsedGameTime.TotalMilliseconds / 15;
        }

        private void StopMovingIfBlocked()
        {
            Vector2 lastMovement = Position - oldPosition;
            if(lastMovement.X == 0) { Movement *= Vector2.UnitY; }
            if(lastMovement.Y == 0) { Movement *= Vector2.UnitX; }
        }

        public bool IsOnFirmGround()
        {
            Rectangle onePixelLower = Bounds;
            onePixelLower.Offset(0, 1);
            return !Board.CurrentBoard.HasRoomForRectangle(onePixelLower);
        }
    }
}