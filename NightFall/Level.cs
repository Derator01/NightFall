using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;
using Nightfall.Dynamic;
using System.Collections.Generic;

namespace NightFall;

public sealed class Level
{
    public Ship Ship { get; private set; }

    public readonly List<Enemy> Enemies = new();

    public delegate void OnShipDied(Enemy killer);
    public event OnShipDied ShipDied;

    public delegate void OnPause(GameTime gameTime);
    public event OnPause Paused;

    public void LoadContent(Microsoft.Xna.Framework.Content.ContentManager content, GraphicsDevice graphicsDevice)
    {
        Ship = new Ship(content.Load<Texture2D>("Default/ship_E"),
                         new Vector2(graphicsDevice.Viewport.Width / 2, graphicsDevice.Viewport.Height / 2));

        Enemies.Add(new Enemy(content.Load<Texture2D>("Default/station_B"),
                               new Vector2(0, 0),
                               rotation: 5));
        Ship.AddSpell(new Projectile(Projectile.Spell.InterferenceCircle,
                                      content.Load<Texture2D>("Default/icon_plusLarge"),
                                      Ship.Position,
                                      1,
                                      Vector2.Zero,
                                      1000,
                                      1000,
                                      0.05f,
                                      0f));
    }

    public void Update(GameTime gameTime)
    {
        MoveShip();

        CheckCollision();
    }

    private void MoveShip()
    {
        TouchCollection touches = TouchPanel.GetState();
        if (touches.Count > 0)
        {
            TouchLocation touch = touches[0]; // Consider the first touch

            if (touch.State == TouchLocationState.Pressed)
            {
                Ship.FingerStartPosition = touch.Position;
                Ship._isMoving = true;
            }
            else if (touch.State == TouchLocationState.Moved && Ship._isMoving)
            {
                Vector2 touchPosition = touch.Position;
                Vector2 direction = touchPosition - Ship.FingerStartPosition;

                if (direction.X != 0 && direction.Y != 0)
                {
                    direction.Normalize();
                    Ship.Position += direction * 10;
                }
            }
            else if (touch.State == TouchLocationState.Released)
            {
                Ship._isMoving = false;
            }
        }
    }

    private void CheckCollision()
    {
        for (int j = 0; j < Enemies.Count; j++)
        {
            Enemy enemy = Enemies[j];
            for (int i = 0; i < Ship.Projectiles.Count; i++)
            {
                Projectile projectile = Ship.Projectiles[i];

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
        Enemies.Remove(enemy);
    }
}
