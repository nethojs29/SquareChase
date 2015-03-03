using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace SuareChase
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game //Hereda de esa onda después del :
    {
        //Englobar como yo quiera

        #region Declarations
        GraphicsDeviceManager graphics; //Accede al GPU
        SpriteBatch spriteBatch; //Maneja los gráficos en pantalla

        Random rand = new Random(); //Random... dah e_e
        Texture2D squareTexture; //La textura del cuadrado, color o patrón.
        Rectangle currentSquare; //Es donde se puede darle "click" al cuadrado
        int playerScore = 0; //Marcador del jugador
        float timeRemaining = 0.0f; //Contador que irá de un cierto inicio hacia la constante TimePerSquare para cambiar de lugar
        float TimePerSquare = 60.0f; //Valor en el cual se cambiará de lugar el cuadrado.
        Color[] colors = new Color[4] {Color.Purple,Color.Green,Color.Violet,Color.Orange}; //Arreglo de colores que tendrá el cuadrado.
        int squareWidth = 30;

        MouseState currentMouseState, lastMouseState; //Estado actual y anterior del mouse
        int penalty = 0; //Para cambiar el tamaño del cuadro.

        #endregion

        public Game1() //Constructor del juego
        {
            graphics = new GraphicsDeviceManager(this); //Se inicializa la variable que maneja el GPU
            Content.RootDirectory = "Content"; //Sabe, no dijo D=
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize() //inicializa lo wapo, mouse, controles, pantalla, etc.
        {
            // TODO: Add your initialization logic here

            base.Initialize();
            this.IsMouseVisible = true;
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent() //Se cargan todas las weas
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice); //ES lo que maneja el que pinta las cosas en pantalla, alguna funcion que lo hace

            squareTexture = Content.Load<Texture2D>(@"spriteSquare"); //Cargando el cuadrito wapo
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime) //Se cargará en putiza cada frame alav, 1/60 segundos
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape)) //Si le aplastan back en el control o ESC en el teclado, se sale
                this.Exit();

            if (timeRemaining == 0.0f)
            {
                currentSquare = new Rectangle(
                    rand.Next(0, this.Window.ClientBounds.Width - squareWidth),
                    rand.Next(0, this.Window.ClientBounds.Height - squareWidth),
                    squareWidth-penalty,
                    squareWidth-penalty
                );

                timeRemaining = TimePerSquare;     
            }



            lastMouseState = currentMouseState;
            currentMouseState = Mouse.GetState();

            if ((lastMouseState.LeftButton == ButtonState.Pressed) &&
                (currentMouseState.LeftButton == ButtonState.Released) &&
                (currentSquare.Contains(currentMouseState.X,currentMouseState.Y)) &&
                (currentSquare.Contains(lastMouseState.X,lastMouseState.Y))){
                    playerScore++;
                    if(penalty<15) penalty++;
                    TimePerSquare -= 1.0f;
                    timeRemaining = 0.0f;
            }

            this.Window.Title = "Score: " + playerScore;
            
            if(timeRemaining!=0.0f) timeRemaining -= 0.5f;
            
            
            // TODO: Add your update logic here

            base.Update(gameTime);
            //A ver si ya funciona xD
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Silver);

            spriteBatch.Begin();
            spriteBatch.Draw(
                    squareTexture,
                    currentSquare,
                    colors[playerScore % 4]);
            spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
