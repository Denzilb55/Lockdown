using Lockdown.Game.Tribes;
using Photon.Pun;
using UnityEngine;

namespace Lockdown.Game.Entities
{
    public class Tribesman : TribeEntity
    {
        [SerializeField]
        private Rigidbody2D _body;

        


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
                foreach (var baseBuilding in BaseModule.Instance)
                {
                    if (baseBuilding.tribe != TribeManagerModule.Instance.MainTribe)
                    {
                        Vector2 delta = BaseModule.Instance.GetObject(0).transform.position - transform.position;
                        _body.velocity = delta.normalized * 0.8f;
                        break;
                    }
                }
            }
        }

        

    }
}
