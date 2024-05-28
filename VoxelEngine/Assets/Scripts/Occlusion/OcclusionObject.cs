using UnityEngine;

namespace Occlusion
{
    public class OcclusionObject : MonoBehaviour
    {
        [SerializeField] private Renderer _myRend;
        public float displayTime;

        private void OnEnable()
        {
            if (_myRend == null)
            {
                _myRend = GetComponent<Renderer>();
                // (stepa) TODO: Переделать под отдельный класс для дебага
            }

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