using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;
using Nightfall.Dynamic;
using System.Collections.Generic;

namespace Nightfall;

public class MainGame : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private Vector2 _cameraPosition;
    private Matrix _viewMatrix;

    private Ship _ship;

    private List<Enemy> _enemies = new();

    //private List<Asteroid> _asteroids = new();

    public MainGame()
    {
        _graphics = new GraphicsDeviceManager(this);

        Content.RootDirectory = "Content";
    }

    protected override void Initialize()
    {
        _graphics.IsFullScreen = true;
        _graphics.SupportedOrientations = DisplayOrientation.Portrait;
        _graphics.PreferredBackBufferWidth = GraphicsDevice.DisplayMode.Width;
        _graphics.PreferredBackBufferHeight = GraphicsDevice.DisplayMode.Height;
        _graphics.ApplyChanges();

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        _ship = new Ship(Content.Load<Texture2D>("Default/ship_E"),
                         new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2), 100);

        _enemies.Add(new Enemy(Content.Load<Texture2D>("Default/station_B"),
                               new Vector2(0, 0), 10,
                               rotation: 5));

        _ship.Spells.Add();
    }

    protected override void Update(GameTime gameTime)
    {
        UpdateCameraPosition();

        MoveShip();

        CheckCollision();

        base.Update(gameTime);
    }

    private void UpdateCameraPosition()
    {
        _cameraPosition = _ship.Position - new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2);
        _viewMatrix = Matrix.CreateTranslation(-_cameraPosition.X, -_cameraPosition.Y, 0f);
    }

    private void MoveShip()
    {
        TouchCollection touches = TouchPanel.GetState();
        if (touches.Count > 0)
        {
            TouchLocation touch = touches[0]; // Consider the first touch

            if (touch.State == TouchLocationState.Pressed)
            {
                _ship.FingerStartPosition = touch.Position;
                _ship._isMoving = true;
            }
            else if (touch.State == TouchLocationState.Moved && _ship._isMoving)
            {
                Vector2 touchPosition = touch.Position;
                Vector2 direction = touchPosition - _ship.FingerStartPosition;

                if (direction.X != 0 && direction.Y != 0)
                {
                    direction.Normalize();
                    _ship.Position += direction * 10;
                }
            }
            else if (touch.State == TouchLocationState.Released)
            {
                _ship._isMoving = false;
            }
        }
    }

    private void CheckCollision()
    {
        for (int j = 0; j < _enemies.Count; j++)
        {
            Enemy enemy = _enemies[j];
            for (int i = 0; i < _ship.Projectiles.Count; i++)
            {
                Projectile projectile = _ship.Projectiles[i];

                if (CheckCollision(projectile, enemy))
                {
                    HandleCollision(projectile, enemy);
                }
            }
        }
    }
    private bool CheckCollision(DynamicTexture texture1, DynamicTexture texture2)
    {
        // Possible optimization
        Vector2 texture1Center = texture1.Position + new Vector2(texture1.Texture.Width / 2, texture1.Texture.Height / 2) * texture1.Scale;
        Vector2 texture2Center = texture2.Position + new Vector2(texture2.Texture.Width / 2, texture2.Texture.Height / 2) * texture2.Scale;

        float texture1Radius = (texture1.Texture.Width / 2 - 10) * texture1.Scale;
        float texture2Radius = (texture2.Texture.Height / 2 - 10) * texture2.Scale;

        return CheckCollision(texture2Center, texture1Radius, texture1Center, texture2Radius);
    }
    private bool CheckCollision(Vector2 center1, float radius1, Vector2 center2, float radius2)
    {
        float distance = Vector2.Distance(center1, center2);
        return distance < radius1 + radius2;
    }

    private void HandleCollision(Projectile projectile, Enemy enemy)
    {
        _enemies.Remove(enemy);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin(transformMatrix: _viewMatrix);
        _spriteBatch.Draw(_ship.Texture, _ship.Position, null, Color.White, 0, Vector2.Zero, _ship.Scale, SpriteEffects.None, 0f);

        DrawEnemies();
        DrawProjectiles();

        //var asteroids = new List<Asteroid>(_asteroids);
        //foreach (Asteroid asteroid in asteroids)
        //{
        //    _spriteBatch.Draw(asteroid.Texture, asteroid.Position, null, Color.Red, 0, Vector2.Zero, 0.1f, SpriteEffects.None, 0f);
        //}

        _spriteBatch.End();

        base.Draw(gameTime);
    }

    private void DrawProjectiles()
    {
        var projectiles = new List<Projectile>(_ship.Projectiles);
        foreach (Projectile projectile in projectiles)
        {
            _spriteBatch.Draw(projectile.Texture,
                              projectile.Position + new Vector2(projectile.Texture.Width / 2f, projectile.Texture.Height / 2f),
                              null,
                              Color.Pink,
                              projectile.Rotation,
                              new Vector2(projectile.Texture.Width / 2f, projectile.Texture.Height / 2f),
                              projectile.Scale,
                              SpriteEffects.None,
                              0f);
        }
    }

    private void DrawEnemies()
    {
        var enemies = new List<Enemy>(_enemies);
        foreach (Enemy enemy in enemies)
        {
            _spriteBatch.Draw(enemy.Texture,
                              enemy.Position + new Vector2(enemy.Texture.Width / 2f, enemy.Texture.Height / 2f),
                              null,
                              Color.Black,
                              0,
                              new Vector2(enemy.Texture.Width / 2f, enemy.Texture.Height / 2f),
                              enemy.Scale,
                              SpriteEffects.None,
                              0f);
        }
    }
}