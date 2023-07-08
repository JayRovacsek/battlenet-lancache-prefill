﻿namespace BattleNetPrefill
{
    public static class AppConfig
    {
        static AppConfig()
        {
            // Create required folders
            Directory.CreateDirectory(ConfigDir);
            Directory.CreateDirectory(CacheDir);
        }

        /// <summary>
        /// https://wowdev.wiki/TACT#HTTP_URLs
        /// </summary>
        public static readonly Uri BattleNetPatchUri = new Uri("http://us.patch.battle.net:1119");

        /// <summary>
        /// Downloaded archive indexes, as well as other metadata, are saved into this directory to speedup future prefill runs.
        /// All data in here should be able to be deleted safely.
        /// </summary>
        public static readonly string CacheDir = CacheDirUtils.GetCacheDirBaseDirectories("BattlenetPrefill", cacheDirVersion: "");

        /// <summary>
        /// Contains user configuration.  Should not be deleted, doing so will reset the app back to defaults.
        /// </summary>
        private static readonly string ConfigDir = Path.Combine(ConfigDir, "Config");

        public static readonly string UserSelectedAppsPath = Path.Combine(ConfigDir, "selectedAppsToPrefill.json");

        public static readonly string LogFileBasePath = @$"{DirectorySearch.TryGetSolutionDirectory()}/Logs";
        private static bool _compareAgainstRealRequests;

        /// <summary>
        /// Global retry policy that will wait increasingly longer periods after a failed request
        /// </summary>
        public static AsyncRetryPolicy RetryPolicy => Policy.Handle<Exception>()
                                                            .WaitAndRetryAsync(5, retryAttempt => TimeSpan.FromMilliseconds(100 * retryAttempt));


        public static TransferSpeedUnit TransferSpeedUnit { get; set; } = TransferSpeedUnit.Bits;

        /// <summary>
        /// If set to true, will skip making any non-required requests, and instead record them to later be compared against for accuracy.
        /// Dramatically speeds up debugging since bandwidth use is a small fraction of the full download size (ex. 100mb vs a possible 30gb download).
        /// </summary>
        public static bool SkipDownloads { get; set; }

        /// <summary>
        /// When enabled, will compare the requests that this application made against the previously recorded requests that the real Battle.Net launcher makes.
        /// A comparison will be output to screen, giving feedback on how accurate our application is vs Battle.Net.
        ///
        /// While not required, enabling <see cref="SkipDownloads"/> will allow for significantly faster debugging.
        /// </summary>
        public static bool CompareAgainstRealRequests
        {
            get => _compareAgainstRealRequests;
            set
            {
                _compareAgainstRealRequests = value;
                // Need to set this to true as well, otherwise you will still need to wait for the whole download to finish, which isn't useful for running the comparison
                SkipDownloads = true;
            }
        }
    }
}
