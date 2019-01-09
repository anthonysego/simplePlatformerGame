using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace platformerThing.MacOS
{

    public class SimplePlatformer : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Texture2D _tileTexture, _jumperTexture;
        private Player _jumper;
        private Board _board;
        private SpriteFont _debugFont;

        public SimplePlatformer()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = 960;
            _graphics.PreferredBackBufferHeight = 640;
            Content.RootDirectory = "Content";
        }


        protected override void Initialize()
        {
            base.Initialize();
        }


        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _tileTexture = Content.Load<Texture2D>("Tile");
            _jumperTexture = Content.Load<Texture2D>("Jumper");
            _jumper = new Player(_jumperTexture, Vector2.One * 80, _spriteBatch);
            _board = new Board(_spriteBatch, _tileTexture, 15, 10);
            _debugFont = Content.Load<SpriteFont>("File");

        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            _jumper.Update(gameTime);
            CheckKeyboardAndReact();
        }

        private void CheckKeyboardAndReact()
        {
            KeyboardState state = Keyboard.GetState();
            if (state.IsKeyDown(Keys.F5)) { RestartGame(); }
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
        }

        private void RestartGame()
        {
            Board.CurrentBoard.CreateNewBoard();
            ResetPlayer();
        }

        private void ResetPlayer()
        {
            _jumper.Position = Vector2.One * 80;
            _jumper.Movement = Vector2.Zero;
        }

        protected override void Draw(GameTime gameTime)
        {
            _graphics.GraphicsDevice.Clear(Color.WhiteSmoke);
            _spriteBatch.Begin();
            base.Draw(gameTime);
            _board.Draw();
            WriteDebugInformation();
            _jumper.Draw();
            _spriteBatch.End();
        }

        private void WriteDebugInformation()
        {
            string positionInText = string.Format("Position of Jumper: ({0:0.0}, {1:0.0})", _jumper.Position.X, _jumper.Position.Y);
            string movementInText = string.Format("Position of movement: ({0:0.0}, {1:0.0})", _jumper.Movement.X, _jumper.Movement.Y);
            string isOnFirmGroundText = string.Format("On firm ground?: {0}", _jumper.IsOnFirmGround());

            DrawWithShadow(positionInText, new Vector2(10, 0));
            DrawWithShadow(movementInText, new Vector2(10, 20));
            DrawWithShadow(isOnFirmGroundText, new Vector2(10, 40));
            DrawWithShadow("F5 for random board", new Vector2(70, 600));
        }

        private void DrawWithShadow(string text, Vector2 position)
        {
            _spriteBatch.DrawString(_debugFont, text, position + Vector2.One, Color.Black);
            _spriteBatch.DrawString(_debugFont, text, position, Color.LightYellow);
        }


    }
}
