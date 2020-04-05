using Lockdown.Game.Tribes;
using Photon.Pun;
using UnityEngine;

namespace Lockdown.Game.Entities
{
    public abstract class TribeEntity : Entity
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
            PhotonView photonView = PhotonView.Get(this);
            photonView.RPC(nameof(SetTribeById), RpcTarget.AllBufferedViaServer, tribe.tribeId);
        }
        
        private void _setTribe(Tribe tribe)
        {
            if (this.tribe != tribe)
            {
                this.tribe = tribe;
                spriteRenderer.color = tribe.color;
                OnSetTribe();
            }
        }

        protected abstract void OnSetTribe();
        
        [PunRPC]
        public void SetTribeById(int tribeId)
        {
            _setTribe(TribeManagerModule.Instance.GetObject(tribeId));
        }
    }
}