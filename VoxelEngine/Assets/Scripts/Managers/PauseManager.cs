using System;
using UnityEngine;

namespace Managers
{
    public class PauseManager : MonoBehaviour
    {
        public static bool IsPause;

        private void Start()
        {
            Continue();
        }

        private void OnEnable()
        {
            PauseEvent += Pause;
            ContinueEvent += Continue;
        }

        private void OnDisable()
        {
            PauseEvent -= Pause;
            ContinueEvent -= Continue;
        }

        public static event Action PauseEvent;
        public static event Action ContinueEvent;


        public static void OnPauseEvent()
        {
            PauseEvent?.Invoke();
        }

        public static void OnContinueEvent()
        {
            ContinueEvent?.Invoke();
        }

        public void Pause()
        {
            Time.timeScale = 0f;
            IsPause = true;

            // (stepa) TODO: Переделать под отдельный класс для дебага
            Debug.Log(
                $"<color=green>[{nameof(PauseManager)}]</color> <color=yellow>{nameof(Pause)}()</color>: Game on pause.");
        }

        public void Continue()
        {
            Time.timeScale = 1f;
            IsPause = false;

            // (stepa) TODO: Переделать под отдельный класс для дебага
            Debug.Log(
                $"<color=green>[{nameof(PauseManager)}]</color> <color=yellow>{nameof(Continue)}()</color>:Game continue.");
        }

        private void TogglePause()
        {
            if (IsPause)
                ContinueEvent?.Invoke();
            else
                PauseEvent?.Invoke();
        }
    }
}