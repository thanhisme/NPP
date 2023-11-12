namespace Utils.HelperFuncs
{
    public static class DeviceDetection
    {
        public static bool IsFromMobileDevice(string userAgent)
        {
            return userAgent.Contains("Mobile") || userAgent.Contains("Android");
        }

        public static bool IsFromTabletDevice(string userAgent)
        {
            return userAgent.Contains("Tablet") || userAgent.Contains("iPad");
        }
    }
}
