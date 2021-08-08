using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Toolkits.Events;
using Toolkits.UI;
using TimeStranded.Locations;
using TimeStranded.UI;

namespace TimeStranded.Management
{
    /// <summary>
    /// Controls the loading screen.
    /// </summary>
    [AddComponentMenu("Time Stranded/Management/Loading Screen")]
    [DisallowMultipleComponent]
    public class LoadingScreen : MonoBehaviour
    {
        /// <summary>
        /// The loading progress bar.
        /// </summary>
        [Tooltip("The loading progress bar.")]
        [SerializeField] private FilledBar _progressBar = null;

        /// <summary>
        /// The loading progress bar fill.
        /// </summary>
        [Tooltip("The loading progress bar fill.")]
        [SerializeField] private Image _progressBarFill = null;

        /// <summary>
        /// The label showing the loading progress.
        /// </summary>
        [Tooltip("The label showing the loading progress.")]
        [SerializeField] private TextMeshProUGUI _progressLabel = null;

        /// <summary>
        /// The compass needle in the loading animation.
        /// </summary>
        [Tooltip("The compass needle in the loading animation.")]
        [SerializeField] private Transform _loadingNeedle = null;

        /// <summary>
        /// The main menu's location.
        /// </summary>
        [Tooltip("The main menu's location.")]
        [SerializeField] private LocationSO _mainMenu = null;

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
        /// The event channel to raise when a location starts loading.
        /// </summary>
        [Tooltip("The event channel to raise when a location starts loading.")]
        [SerializeField] private LocationSceneLoadEventChannelSO _onLocationSceneLoadStartChannel = null;

        /// <summary>
        /// The event channel to raise when a location ticks during loading.
        /// </summary>
        [Tooltip("The event channel to raise when a location ticks during loading.")]
        [SerializeField] private LocationSceneLoadEventChannelSO _onLocationSceneLoadTickChannel = null;

        /// <summary>
        /// The event channel to raise when a location finishes loading.
        /// </summary>
        [Tooltip("The event channel to raise when a location finishes loading.")]
        [SerializeField] private LocationSceneLoadEventChannelSO _onLocationSceneLoadFinishChannel = null;

        /// <summary>
        /// The channel to raise when applying a theme.
        /// </summary>
        [Tooltip("The channel to raise when applying a theme.")]
        [SerializeField] private ThemeEventChannelSO _onThemeApplyChannel = null;

        /// <summary>
        /// Whether or not the main menu was already loaded.
        /// </summary>
        private bool _loadedInitialMainMenu = false;

        /// <summary>
        /// Whether or not a location is loading.
        /// </summary>
        private bool _isLoading = false;

        /// <summary>
        /// The previous load progress.
        /// </summary>
        private float _previousLoadProgress = 0;

        /// <summary>
        /// Whether or not the loading screen should hide.
        /// </summary>
        private bool _shouldHide = false;

        /// <summary>
        /// Whether or not the screen is still transitioning.
        /// </summary>
        private bool _isTransitioning = false;

        private void Awake()
        {
            // Subscribe to events then turn off.
            _onScreenTransitionFinishChannel.OnRaised += OnTransitionFinish;
            _onLocationSceneLoadStartChannel.OnRaised += ShowLoadingScreen;
            _onLocationSceneLoadTickChannel.OnRaised += UpdateLoadingScreen;
            _onLocationSceneLoadFinishChannel.OnRaised += HideLoadingScreen;
            _onThemeApplyChannel.OnRaised += ApplyTheme;
            gameObject.SetActive(false);
        }

        private void Update()
        {
            // Hide the loading screen after the show transition is done.
            if (!_isTransitioning && !_isLoading && _shouldHide)
            {
                _shouldHide = false;
                _onScreenTransitionShowChannel.Raise(0);
                gameObject.SetActive(false);
            }
        }

        /// <summary>
        /// Applies the given theme to the text.
        /// </summary>
        /// <param name="theme">The theme to apply.</param>
        public void ApplyTheme(ThemeSO theme)
        {
            _progressBarFill.color = theme.ButtonColor;
        }

        /// <summary>
        /// Called when a screen transition finishes.
        /// </summary>
        private void OnTransitionFinish()
        {
            _isTransitioning = false;
            if (_isLoading) gameObject.SetActive(true);
        }

        /// <summary>
        /// Shows the loading screen.
        /// </summary>
        /// <param name="location">The location to load.</param>
        /// <param name="progress">The load progress.</param>
        private void ShowLoadingScreen(LocationSO location, float progress)
        {
            if (!_loadedInitialMainMenu && location == _mainMenu)
            {
                _loadedInitialMainMenu = true;
                return;
            }

            _isLoading = true;
            _previousLoadProgress = 0;
            _isTransitioning = true;
            _onScreenTransitionShowChannel.Raise(1);
            UpdateLoadingScreen(location, 0);
        }

        /// <summary>
        /// Shows the loading screen.
        /// </summary>
        /// <param name="location">The location to load.</param>
        /// <param name="progress">The load progress.</param>
        private void UpdateLoadingScreen(LocationSO location, float progress)
        {
            int progressPercent = Mathf.FloorToInt(progress * 100);
            int previousProgressPercent = Mathf.FloorToInt(_previousLoadProgress * 100);
            _progressBar.SetFill(progress, false);
            if (previousProgressPercent != progressPercent) _progressLabel.text = $"{progressPercent}%";
            _loadingNeedle.localEulerAngles = new Vector3(0, 0, -360 * progress);
            _previousLoadProgress = progress;
        }

        /// <summary>
        /// Shows the loading screen.
        /// </summary>
        /// <param name="location">The location to load.</param>
        /// <param name="progress">The load progress.</param>
        private void HideLoadingScreen(LocationSO location, float progress)
        {
            _isLoading = false;
            _previousLoadProgress = 1;
            _shouldHide = true;
        }
    }
}
