using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace UnityEngine.XR.ARFoundation.Samples
{
    public class ProceedButton : MonoBehaviour
    {
        [SerializeField]
        GameObject m_Proceed;

        public GameObject proceedButton
        {
            get => m_Proceed;
            set => m_Proceed = value;
        }


        void Start()
        {
            if (Application.CanStreamedLevelBeLoaded("PointFilter"))
            {
                m_Proceed.SetActive(true);
            }
                
        }

        public void ProceedButtonPressed()
        {
            if (Application.CanStreamedLevelBeLoaded("PointFilter"))
            {
                SceneManager.LoadScene("PointFilter", LoadSceneMode.Single);
            }
        }
    }
}