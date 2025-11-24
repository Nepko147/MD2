namespace YG
{
    public partial class SavesYG
    {
        public int ProgressData_Statistics_ReviveNumber = 0;
        public int ProgressData_Statistics_ReviveNumberBest = 0;
        public int ProgressData_Statistics_CoinsTotal = 0;
        public int ProgressData_Statistics_CoinsSpentOnRevivals = 0;
        public int ProgressData_Statistics_Defeats = 0;
        public int ProgressData_Statistics_TotalDrivings = 0;

        public ControlPers_DataHandler.ProgressData_Upgrades_State ProgressData_Upgrades_MoreCoins = ControlPers_DataHandler.ProgressData_Upgrades_State.buy;
        public ControlPers_DataHandler.ProgressData_Upgrades_State ProgressData_Upgrades_MoreBonuses = ControlPers_DataHandler.ProgressData_Upgrades_State.buy;
        public ControlPers_DataHandler.ProgressData_Upgrades_State ProgressData_Upgrades_CoinMagnet = ControlPers_DataHandler.ProgressData_Upgrades_State.buy;
        public ControlPers_DataHandler.ProgressData_Upgrades_State ProgressData_Upgrades_Revive = ControlPers_DataHandler.ProgressData_Upgrades_State.buy;

        public int ProgressData_Coins = 0;

        public float SettingsData_Audio_Sound = ControlPers_DataHandler.SETTINGSDATA_AUDIO_SOUND_DEFAULTVALUE;
        public float SettingsData_Audio_Music = ControlPers_DataHandler.SETTINGSDATA_AUDIO_MUSIC_DEFAULTVALUE;
    }
}
