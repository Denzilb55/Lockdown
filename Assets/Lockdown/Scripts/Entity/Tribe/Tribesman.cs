using Lockdown.Game.Tribes;
using Photon.Pun;
using UnityEngine;

namespace Lockdown.Game.Entities
{
    public class Tribesman : Entity
    {
        [SerializeField]
        private Rigidbody2D _body;

        [SerializeField]
        private SpriteRenderer _spriteRenderer;
        
        

        public Tribe tribe
        {
            get;
            private set;
        }
        
        private void OnDestroy()
        {
            TribesmanManagerModule.Instance.DeRegisterEntity(this);
        }


        private void Awake()
        {
            TribesmanManagerModule.Instance.RegisterEntity(this);
        }

        // Update is called once per frame
        void Update()
        {
            // ensures this networked instance is only moved by owner
            if (tribe != null && tribe.IsMainTribe)
            {
                if (BaseModule.Instance.Count > 0)
                {
                    Vector2 delta = BaseModule.Instance.GetObject(0).transform.position - transform.position;
                    _body.velocity = delta.normalized * 0.8f;
                }
            }
        }

        public void SetTribe(Tribe tribe)
        {
            this.tribe = tribe;
            _spriteRenderer.color = tribe.color;
        }

    }
}
