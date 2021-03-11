// MultiMediaTechnology / FHS
// MultiMediaProjekt 1
// Programming, Art & Music - Hofer Thomas
// Co-Music Producer - Veltman Bob

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SG_01
{
    public class Player
    {
        // player
        public Texture2D texture;
        public Vector2 position;
        public Rectangle animationRect = new Rectangle(0, 0, 32, 32);
        public Rectangle rectCollider;
        public bool isDead = false;
        public Vector2 animationTilling = new Vector2(2, 4);
        public float playerSpeed = 200;
        public int animationType = 0;
        public float animationIndex = 0;

        // death
        public Rectangle deathAnimationRect = new Rectangle(0, 0, 32, 32);
        public Vector2 deathAnimationTilling = new Vector2(4, 1);
        public float animationIndexDeath = 0;
        public bool playDeathSound = false;

        // points
        public bool inGoal = false;
        public int points = 0;

        // saves Coordinates of current Quicksand Interaction
        public int slowerX;
        public int slowerY;
    }
}
