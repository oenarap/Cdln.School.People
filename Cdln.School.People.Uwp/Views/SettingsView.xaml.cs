using System;
using System.Reflection;
using Windows.UI.Xaml.Controls;

namespace Cdln.School.People.Uwp.Views
{
    public sealed partial class SettingsView : Page
    {
        public string AppInfo { get; }

        public SettingsView()
        {
            this.InitializeComponent();

            Assembly assembly = Assembly.GetExecutingAssembly();
            string version = assembly.GetName().Version.ToString();
            string copyright = assembly.GetCustomAttribute<AssemblyCopyrightAttribute>().Copyright;
            string company = assembly.GetCustomAttribute<AssemblyCompanyAttribute>().Company;
            AppInfo = $"Version { version }\n{ copyright } { company }. All rights reserved.";
        }
    }
}
