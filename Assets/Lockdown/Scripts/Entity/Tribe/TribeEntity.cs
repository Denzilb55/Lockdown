using Lockdown.Game.Tribes;
using UnityEngine;

namespace Lockdown.Game.Entities
{
    public class TribeEntity : Entity
    {
        public Tribe tribe
        {
            get;
            private set;
        }
        
        [SerializeField]
        private SpriteRenderer _spriteRenderer;

        public SpriteRenderer spriteRenderer
        {
            get
            {
                if (_spriteRenderer == null)
                {
                    _spriteRenderer = GetComponent<SpriteRenderer>();
                }
                return _spriteRenderer;
            }
        }
        
        public void SetTribe(Tribe tribe)
        {
            this.tribe = tribe;
            spriteRenderer.color = tribe.color;
        }
    }
}