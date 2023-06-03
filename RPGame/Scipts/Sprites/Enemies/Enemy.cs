using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGame.Scipts.Sprites.Enemies
{
    internal abstract class Enemy : Component
    {
        public virtual Color EnemyColor { get; set; }

        public virtual float HP { get; set; }

        public virtual float Damage { get; set; }

        public virtual bool HasTakenDamage { get; set; }


        public virtual void TakeDamage(float damage, GameTime gameTime)
        {
            HP -= damage;
        }
    }
}
