using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameProject1
{
    /// <summary>
    /// A game demonstrating the use of sprites
    /// </summary>
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        private SlimeGhostSprite slimeGhost;
        private Texture2D atlas;
        private BatSprite[] bats;
        private SpriteFont bangers;

        /// <summary>
        /// Constructs the game
        /// </summary>
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        /// <summary>
        /// Initializes the game
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            slimeGhost = new SlimeGhostSprite();
            bats = new BatSprite[]
            {
                new BatSprite() { Position = new Vector2(100,100), Direction = Direction.Down },
                new BatSprite() { Position = new Vector2(400,400), Direction = Direction.Up },
                new BatSprite() { Position = new Vector2(200,500), Direction = Direction.Left },
            };

            base.Initialize();
        }

        /// <summary>
        /// Loads game content
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            slimeGhost.LoadContent(Content);
            atlas = Content.Load<Texture2D>("colored_packed");
            foreach (var bat in bats) bat.LoadContent(Content);
            bangers = Content.Load<SpriteFont>("bangers");
        }

        /// <summary>
        /// Updates the game world
        /// </summary>
        /// <param name="gameTime">the measured game time</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            slimeGhost.Update(gameTime);
            foreach (var bat in bats) bat.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// Draws the game world
        /// </summary>
        /// <param name="gameTime">the measured game time</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            spriteBatch.Draw(atlas, new Vector2(50, 50), new Rectangle(16 * 6, 16, 16, 16), Color.White);
            foreach (var bat in bats) bat.Draw(gameTime, spriteBatch);
            slimeGhost.Draw(gameTime, spriteBatch);
            spriteBatch.DrawString(bangers, $"{gameTime.TotalGameTime:c}", new Vector2(50, 50), Color.Gold);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
