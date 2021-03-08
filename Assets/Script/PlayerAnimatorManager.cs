using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Com.MyCompany.MyGame
{ 
    public class PlayerAnimatorManager : MonoBehaviour
    {
        #region Private Fields

        [SerializeField]
        private float directionDampTime = 0.25f;
        private Animator animator;

        #endregion



        #region MonoBehaviour Callbacks

         
        // Start is called before the first frame update
        void Start()
        {
            animator = GetComponent<Animator>();
            if (!animator)
            {
                Debug.LogError("PlayerAnimatorManager is Missing Animator Component", this);
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (!animator)
            {
                return;
            }
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            if (stateInfo.IsName("Base Layer.Run"))
            {
                if (Input.GetButtonDown("Fire2"))
                {
                    animator.SetTrigger("Jump");
                }

            }
            
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");
            if (v < 0)
            {
                v = 0;
            }
            // 両方の入力を二乗しているのは値を常に正の絶対値にして簡単にしておくため
            animator.SetFloat("Speed",h * h + v * v);
            animator.SetFloat("Direction",h, directionDampTime, Time.deltaTime);
            
        }

        #endregion
    }
}
