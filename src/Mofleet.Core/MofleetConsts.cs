﻿using Mofleet.Debugging;

namespace Mofleet
{
    public class MofleetConsts
    {
        public const string LocalizationSourceName = "Mofleet";

        public const string ConnectionStringName = "Default";

        public const bool MultiTenancyEnabled = true;

        public const string AppServerRootAddressKey = "App:ServerRootAddress";

        public const int VerificationTimeCodeInMinutes = 10;

        public const string EnumNameSpace = "Mofleet.Enums";


        /// <summary>
        /// Default pass phrase for SimpleStringCipher decrypt/encrypt operations
        /// </summary>
        public static readonly string DefaultPassPhrase =
            DebugHelper.IsDebug ? "gsKxGZ012HLL3MI5" : "005cb6ebdaff41ed8cdf01194f308b94";
    }
}
