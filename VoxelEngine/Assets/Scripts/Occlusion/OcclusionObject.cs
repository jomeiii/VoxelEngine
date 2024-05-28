using UnityEngine;

namespace Occlusion
{
    public class OcclusionObject : MonoBehaviour
    {
        [SerializeField] private Renderer _myRend;
        public float displayTime;

        private void OnEnable()
        {
            _myRend = GetComponent<Renderer>();
            displayTime = -1;
        }

        private void Update()
        {
            if (displayTime > 0)
            {
                displayTime -= Time.deltaTime;
                _myRend.enabled = true;
            }
            else
            {
                _myRend.enabled = false;
            }
        }

        public void HitOcclude(float time)
        {
            displayTime = time;
            _myRend.enabled = true;
        }
    }
}