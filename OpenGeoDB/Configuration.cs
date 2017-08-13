namespace OpenGeoDB
{
    public static class Configuration
    {
        /// <summary>
        /// Gets a value indicating whether database updates should forced.
        /// E.g. update will be performed every time when application starts if value is <c>true</c>
        /// and performs only one update otherwise.
        /// </summary>
        public static bool ShouldForceUpdate => false;

        public static string DataResourceName => "OpenGeoDB.Resources.PLZ.tab";

        public static int NearbyZipCodesCount => 10;
    }
}
