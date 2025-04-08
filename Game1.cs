using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Monogame_3._5___Dragging_and_Dropping
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Texture2D asteroidTexture, carTexture, rocketTexture;
        Rectangle asteroidRect, carRect, rocketRect;

        MouseState currentMouseState, prevMouseState;

        bool isDraggingAsteroid, isDraggingCar, isDraggingRocket;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            asteroidRect = new Rectangle(10,10,50,50); 
            isDraggingAsteroid = false;

            carRect = new Rectangle(200,200,75,25);
            isDraggingCar = false;

            rocketRect = new Rectangle(400, 100, 40, 75);
            isDraggingRocket = false;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            asteroidTexture = Content.Load<Texture2D>("Images/asteroid");
            carTexture = Content.Load<Texture2D>("Images/fast_car");
            rocketTexture = Content.Load<Texture2D>("Images/rocket");
        }

        protected override void Update(GameTime gameTime)
        {
            currentMouseState = Mouse.GetState();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            //Dragging Asteriod - All Directions
            if (NewClick() && asteroidRect.Contains(currentMouseState.Position))
            {
                isDraggingAsteroid = true;
            }
            else if (isDraggingAsteroid && currentMouseState.LeftButton == ButtonState.Released)
            {
                isDraggingAsteroid = false;
            }
            else if (isDraggingAsteroid)
            {
                asteroidRect.Offset(currentMouseState.X - prevMouseState.X, currentMouseState.Y - prevMouseState.Y);
            }

            //Dragging Car - Horizontal only
            if (NewClick() && carRect.Contains(currentMouseState.Position))
            {
                isDraggingCar = true;
            }
            else if (isDraggingCar && currentMouseState.LeftButton == ButtonState.Released)
            {
                isDraggingCar = false;
            }
            else if (isDraggingCar)
            {
                carRect.Offset(currentMouseState.X - prevMouseState.X, 0);
            }

            //Dragging Rocket - Vertical only
            if (NewClick() && rocketRect.Contains(currentMouseState.Position))
            {
                isDraggingRocket = true;
            }
            else if (isDraggingRocket && currentMouseState.LeftButton == ButtonState.Released)
            {
                isDraggingRocket = false;
            }
            else if (isDraggingRocket)
            {
                rocketRect.Offset(0, currentMouseState.Y - prevMouseState.Y);
            }

            prevMouseState = currentMouseState;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            _spriteBatch.Draw(asteroidTexture, asteroidRect, Color.White);
            _spriteBatch.Draw(carTexture, carRect, Color.White);
            _spriteBatch.Draw(rocketTexture, rocketRect, Color.White);

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        protected bool NewClick()
        {
            return currentMouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released;
        }
    }
}
