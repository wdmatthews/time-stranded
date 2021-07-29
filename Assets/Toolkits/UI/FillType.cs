namespace Toolkits.UI
{
    /// <summary>
    /// Determines how a <see cref="FilledBar"/> is filled.
    /// </summary>
    [System.Serializable]
    public enum FillType
    {
        /// <summary>
        /// Sets the image fill amount.
        /// </summary>
        FillAmount,
        /// <summary>
        /// Sets the fill image's width to a percentage of the bar's width.
        /// </summary>
        Width,
        /// <summary>
        /// Sets the fill image's height to a percentage of the bar's height.
        /// </summary>
        Height
    }
}
