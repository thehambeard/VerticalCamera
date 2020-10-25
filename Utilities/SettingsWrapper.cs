using static VerticalCamera.Main;

namespace VerticalCamera.Utilities
{
    public static class SettingsWrapper
    {
        public static string LocalizationFileName
        {
            get => Mod.Settings.localizationFileName;
            set => Mod.Settings.localizationFileName = value;
        }

        public static string ModPath
        {
            get => Mod.Settings.modPath;
            set => Mod.Settings.modPath = value;
        }
    }
}