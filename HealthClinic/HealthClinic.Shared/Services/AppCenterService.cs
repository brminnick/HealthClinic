using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

using Microsoft.AppCenter.Analytics;

namespace HealthClinic.Shared
{
    public static class AppCenterService
    {
        #region Enums
        enum _pathType { Windows, Linux };
        #endregion

        #region Methods
        public static void TrackEvent(string trackIdentifier, IDictionary<string, string> table = null) =>
            Analytics.TrackEvent(trackIdentifier, table);

        public static void TrackEvent(string trackIdentifier, string key, string value)
        {
            IDictionary<string, string> table = new Dictionary<string, string> { { key, value } };

            if (string.IsNullOrWhiteSpace(key) && string.IsNullOrWhiteSpace(value))
                table = null;

            TrackEvent(trackIdentifier, table);
        }


        [Conditional("DEBUG")]
        public static void LogException(Exception exception,
            [CallerMemberName] string callerMemberName = "",
            [CallerLineNumber] int lineNumber = 0,
            [CallerFilePath] string filePath = "")
        {
            var fileName = GetFileNameFromFilePath(filePath);

            Debug.WriteLine(exception.GetType());
            Debug.WriteLine($"Error: {exception.Message}");
            Debug.WriteLine($"Line Number: {lineNumber}");
            Debug.WriteLine($"Caller Name: {callerMemberName}");
            Debug.WriteLine($"File Name: {fileName}");
        }

        static string GetFileNameFromFilePath(string filePath)
        {
            string fileName;
            _pathType pathType;

            var directorySeparator = new Dictionary<_pathType, string>
            {
                { _pathType.Linux, "/" },
                { _pathType.Windows, @"\" }
            };

            pathType = filePath.Contains(directorySeparator[_pathType.Linux]) ? _pathType.Linux : _pathType.Windows;

            while (true)
            {
                if (!(filePath.Contains(directorySeparator[pathType])))
                {
                    fileName = filePath;
                    break;
                }

                var indexOfDirectorySeparator = filePath.IndexOf(directorySeparator[pathType], StringComparison.Ordinal);
                var newStringStartIndex = indexOfDirectorySeparator + 1;

                filePath = filePath.Substring(newStringStartIndex, filePath.Length - newStringStartIndex);
            }

            return fileName;
        }
        #endregion
    }
}
