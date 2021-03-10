using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using Photon.Pun;
using Photon.Realtime;


namespace Com.MyCompany.MyGame
{
    

    public class PlayerManager : MonoBehaviourPunCallbacks
    {
        #region Private Fields

        [Tooltip("The Beams GameObject to control")]
        [SerializeField]
        private GameObject beams;

        bool IsFiring;
        #endregion


        #region Public Fields

         [Tooltip("The current Health of of our player")]
        public float Health = 1f;

        #endregion 


        #region MonoBehaviour CallBacks

        void Awake()
        {
            if (beams == null)
            {
                Debug.LogError("<Color=Red><a>Missing</a></Collor> Beams Reference.", this);
            }
            else
            {
                beams.SetActive(false);
            }
        }


        // Update is called once per frame
        void Update()
        {
           ProcessInputs();

           if (beams != null && IsFiring != beams.activeInHierarchy)
           {
               beams.SetActive(IsFiring);
           } 
           
           if (photonView.IsMine)
           {
               ProcessInputs();
               if (Health <= 0f )
               {
                   GameManager.Instance.LeaveRoom();
               }
           }
        }


        void OnTriggerEnter(Collider other)
        {
            if (! photonView.IsMine)
            {
                return;
            }

            if (!other.name.Contains("Beam"))
            {
                return;
            }
            Health -= 0.1f;
        }

        void OnTriggerStay(Collider other)
        {
            if (! photonView.IsMine)
            {
                return;
            }
            Health -= 0.1f*Time.deltaTime;
        }
        #endregion

        #region Custom

        void ProcessInputs()
        {
            if (Input.GetButtonDown("Fire1"))
            {
                if (!IsFiring)
                {
                    IsFiring = true;
                }
            }
            if (Input.GetButtonUp("Fire1"))
            {
                if (IsFiring)
                {
                    IsFiring = false;
                }
            }
        }

        #endregion
    }
}
