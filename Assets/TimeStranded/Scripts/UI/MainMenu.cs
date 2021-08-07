using UnityEngine;
using Toolkits.Events;

namespace TimeStranded.UI
{
    /// <summary>
    /// Controls the main menu.
    /// </summary>
    [AddComponentMenu("Time Stranded/UI/Main Menu")]
    [DisallowMultipleComponent]
    public class MainMenu : MonoBehaviour
    {
        /// <summary>
        /// The title screen.
        /// </summary>
        [Tooltip("title")]
        [SerializeField] private Transform _titleScreen = null;

        /// <summary>
        /// The event channel to raise when showing the screen transition.
        /// </summary>
        [Tooltip("The event channel to raise when showing the screen transition.")]
        [SerializeField] private IntEventChannelSO _onScreenTransitionShowChannel = null;

        /// <summary>
        /// The event channel to raise when the screen transition finishes.
        /// </summary>
        [Tooltip("The event channel to raise when the screen transition finishes.")]
        [SerializeField] private EventChannelSO _onScreenTransitionFinishChannel = null;

        /// <summary>
        /// The main menu's current screen.
        /// </summary>
        private Transform _currentScreen = null;

        /// <summary>
        /// The main menu's current screen.
        /// </summary>
        private Transform _nextScreen = null;

        /// <summary>
        /// The current transition stage.
        /// </summary>
        private int _transitionStage = 0;

        private void Awake()
        {
            _currentScreen = _titleScreen;
        }

        /// <summary>
        /// Opens the given screen.
        /// </summary>
        /// <param name="screen">The screen to open.</param>
        public void OpenScreen(Transform screen)
        {
            _nextScreen = screen;
            _onScreenTransitionFinishChannel.OnRaised += OnTransitionFinish;
            _onScreenTransitionShowChannel.Raise(0);
            _transitionStage = 0;
        }

        /// <summary>
        /// Transition screens if needed.
        /// </summary>
        private void OnTransitionFinish()
        {
            _onScreenTransitionFinishChannel.OnRaised -= OnTransitionFinish;

            // Turn off the old screen and turn on the new one.
            if (_transitionStage == 0)
            {
                _transitionStage++;
                _nextScreen.gameObject.SetActive(true);
                _currentScreen.gameObject.SetActive(false);
                _currentScreen = _nextScreen;
                _nextScreen = null;

                // Close the transition screen.
                _onScreenTransitionFinishChannel.OnRaised += OnTransitionFinish;
                _onScreenTransitionShowChannel.Raise(1);
            }
        }
    }
}
