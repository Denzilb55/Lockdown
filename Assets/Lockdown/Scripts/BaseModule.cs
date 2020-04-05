using Lockdown.Game.Entities;
using Photon.Pun;


namespace Lockdown.Game
{
    public class BaseModule : EntityManagementModule<BaseModule, BaseBuilding>
    {
        
        protected override void OnInit()
        {
            if (PhotonNetwork.IsMasterClient)
            {
                CreateManagedObject();
            }
        }
        
        
    }
}