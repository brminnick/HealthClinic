using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;

namespace HealthClinic
{
    public static class AppCenterService
    {
        public static void Start() => Start(AppCenterConstants.AppCenterAPIKey);

        public static void TrackEvent(string trackIdentifier, IDictionary<string, string>? table = null) =>
            Analytics.TrackEvent(trackIdentifier, table);

        public static void TrackEvent(string trackIdentifier, string key, string value)
        {
            IDictionary<string, string>? table = new Dictionary<string, string> { { key, value } };

            if (string.IsNullOrWhiteSpace(key) && string.IsNullOrWhiteSpace(value))
                table = null;

            TrackEvent(trackIdentifier, table);
        }

        public static void Report(Exception exception,
                          IDictionary<string, string>? properties = null,
                          [CallerMemberName] string callerMemberName = "",
                          [CallerLineNumber] int lineNumber = 0,
                          [CallerFilePath] string filePath = "")
        {
            LogException(exception, callerMemberName, lineNumber, filePath);

            Crashes.TrackError(exception, properties);
        }


        [Conditional("DEBUG")]
        static void LogException(Exception exception,
            [CallerMemberName] string callerMemberName = "",
            [CallerLineNumber] int lineNumber = 0,
            [CallerFilePath] string filePath = "")
        {
            var fileName = System.IO.Path.GetFileName(filePath);

            Debug.WriteLine(exception.GetType());
            Debug.WriteLine($"Error: {exception.Message}");
            Debug.WriteLine($"Line Number: {lineNumber}");
            Debug.WriteLine($"Caller Name: {callerMemberName}");
            Debug.WriteLine($"File Name: {fileName}");
        }

        static void Start(string apiKey) => Microsoft.AppCenter.AppCenter.Start(apiKey, typeof(Analytics), typeof(Crashes));
    }
}
