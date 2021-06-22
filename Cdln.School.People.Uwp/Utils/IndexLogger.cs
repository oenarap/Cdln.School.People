using System;
using Windows.Storage;
using School.People.Core;
using System.Collections.Generic;

namespace Cdln.School.People.Uwp.Utils
{
    public sealed class IndexLogger : IIndexLogger
    {
        public int GetLog(string key)
        {
            try { return Logs[key]; }
            catch { return 0; }
        }

        public void Log(string key, int index)
        {
            Logs[key] = index;
            updated = true;
        }

        public void SaveLogs()
        {
            if (updated)
            {
                ApplicationDataContainer container = ApplicationData.Current.RoamingSettings;
                foreach (var log in Logs) { container.Values[log.Key] = log.Value; }
                updated = false;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    SaveLogs();
                    Logs.Clear();
                }
                disposed = true;
            }
        }

        public void RegisterKey(string key)
        {
            if (Logs.ContainsKey(key) || Logs.Count > MaxLogKeys) { return; }
            ApplicationDataContainer container = ApplicationData.Current.RoamingSettings;
            int index = (int?)container.Values[key] ?? 0;
            Logs.Add(key, index);
        }

        private bool updated;
        private bool disposed;
        private readonly Dictionary<string, int> Logs = new Dictionary<string, int>();

        public const int MaxLogKeys = 16;
    }
}
