using Lockdown.Game.Tribes;
using Photon.Pun;
using UnityEngine;

namespace Lockdown.Game.Entities
{
    public class Tribesman : TribeEntity
    {
        [SerializeField]
        private Rigidbody2D _body;

        private Transform _target;


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
            if (tribe == null || !tribe.IsMainTribe)
            {
                enabled = false;
                return;
            }
            if (_target == null)
            {
                
                /*if (tribe != null && tribe.IsMainTribe)
                {
                    foreach (var baseBuilding in BaseModule.Instance)
                    {
                        if (baseBuilding.tribe != TribeManagerModule.Instance.MainTribe)
                        {
                            _target = baseBuilding.transform;
                            break;
                        }
                    }
                }*/


                Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, 12);

                foreach (var col in cols)
                {
                    
                    Food food = col.transform.GetComponent<Food>();
                    if (food != null)
                    {
                        // consume food
                        _target = col.transform;
                    }
                    else
                    {
                        Tribesman tribesman = col.transform.GetComponent<Tribesman>();
                        // check if collided object is enemy, and destroy
                        if (tribesman!= null && tribesman.tribe != tribe)
                        {
                            _target = tribesman.transform;
                        }
                    }
                }
                

            }
            else
            {

                // Unity objects can pretend to be null after they are destroyed internally, even though the reference is not really null
                if (_target == null || _target.Equals(null))
                {
                    _target = null;
                    return;
                }
                
                Vector2 delta = _target.position - transform.position;
                _body.velocity = delta.normalized * 0.8f;
            }
            
        }

        protected override void OnSetTribe()
        {
           gameObject.SetActive(true);
           enabled = true;


        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (!tribe.IsMainTribe || tribe == null)
            {
               return;
            }
            
            Food food = other.transform.GetComponent<Food>();
            // check if collided object is food, and consume
            Debug.Log("Collide: " + other);
            if (food != null)
            {
                // consume food
                food.NetworkDestroy();
                tribe.CollectFood();
            }
            else
            {
                           
                Tribesman tribesman = other.transform.GetComponent<Tribesman>();
                // check if collided object is enemy, and destroy
                if (tribesman != null && tribesman.tribe != tribe)
                {
                    if (Random.Range(0, 10) == 0)
                        tribesman.NetworkDestroy();
                } 
            }

        }
    }
}
