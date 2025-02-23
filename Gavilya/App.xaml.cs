﻿/*
MIT License

Copyright (c) Léo Corporation

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE. 
*/

using Gavilya.Helpers;
using Gavilya.Models;
using Gavilya.ViewModels;
using Gavilya.Views;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows;
using System.Windows.Shell;

namespace Gavilya;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
	protected override void OnStartup(StartupEventArgs e)
	{
		ProfileData profiles = new();
		profiles.Load();

		var currentProfile = profiles.Profiles.Where(p => p.ProfileUuid == profiles.SelectedProfileUuid).First();

		if (currentProfile.Settings.Language != Language.Default)
			Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(currentProfile.Settings.Language switch
			{
				Language.en_US => "en-US",
				Language.fr_FR => "fr-FR",
				Language.zh_CN => "zh-CN",
			});

		currentProfile.Settings.EnableSearchShortcut ??= true;

		if (currentProfile.Settings.MakeAutoSave && IsSaveDay(currentProfile.Settings.AutoSaveDay) && !File.Exists($@"{currentProfile.Settings.SavePath}\GavilyaProfiles_{DateTime.Now:yyyy_MM_dd}.g4v"))
		{
			profiles.Backup(currentProfile.Settings.SavePath);
		}

		if (currentProfile.Settings.CurrentTheme != "")
		{
			ThemeHelper.ChangeTheme(ThemeHelper.GetThemeFromPath(currentProfile.Settings.CurrentTheme), currentProfile.Settings.CurrentTheme.Replace(@"\theme.manifest", ""));
		}

		if (!currentProfile.Settings.IsFirstRun)
		{
			CreateJumpLists(currentProfile.Games);
			int? pageID = (e.Args.Length >= 2 && e.Args[0] == "/page") ? int.Parse(e.Args[1]) : null;

			MainWindow = new MainWindow();

			MainViewModel mvm = new(MainWindow, currentProfile, profiles, pageID == null ? null : (Page)pageID);
			MainWindow.DataContext = mvm;
			MainWindow.Show();
		}
		else
		{
			MainWindow = new FirstRunView();

			FirstRunViewModel firstRunViewModel = new(MainWindow, currentProfile, profiles);
			MainWindow.DataContext = firstRunViewModel;
			MainWindow.Show();
		}

		base.OnStartup(e);
	}

	private bool IsSaveDay(int day)
	{
		// Get the current date
		DateTime currentDate = DateTime.Today;

		// Get the last day of the current month
		int lastDayOfMonth = DateTime.DaysInMonth(currentDate.Year, currentDate.Month);

		// Check if the specified day matches the current day or the last day of the month
		if (day == currentDate.Day || (day == 31 && day >= lastDayOfMonth))
		{
			return true;
		}

		// Check special case for February
		if (currentDate.Month == 2)
		{
			// If the specified day is 29, 30, or 31, return true
			if ((day == 29 || day == 30 || day == 31))
			{
				return true;
			}

			// If the specified day is 28 (last day of February), return true
			if (day == 28)
			{
				return true;
			}
		}

		return false;
	}

	private void CreateJumpLists(GameList games)
	{
		JumpList jumpList = new();

		var gTasks = games.Where(g => g.IsFavorite).Select(g => new JumpTask()
		{
			Title = g.Name,
			Arguments = $"{g.Command}",
			Description = g.Command,
			CustomCategory = Gavilya.Properties.Resources.Favorites,
			IconResourcePath = g.GameType == Enums.GameType.Win32 ? g.Command : Assembly.GetEntryAssembly()?.Location,
			ApplicationPath = "explorer.exe"
		});

		jumpList.JumpItems.AddRange(gTasks);

		jumpList.JumpItems.Add(new JumpTask()
		{
			Title = Gavilya.Properties.Resources.Home,
			Arguments = "/page 0",
			Description = Gavilya.Properties.Resources.Home,
			CustomCategory = Gavilya.Properties.Resources.Tasks,
			IconResourcePath = Assembly.GetEntryAssembly()?.Location
		});

		jumpList.JumpItems.Add(new JumpTask()
		{
			Title = Gavilya.Properties.Resources.Library,
			Arguments = "/page 1",
			Description = Gavilya.Properties.Resources.Library,
			CustomCategory = Gavilya.Properties.Resources.Tasks,
			IconResourcePath = Assembly.GetEntryAssembly()?.Location
		});

		jumpList.JumpItems.Add(new JumpTask()
		{
			Title = Gavilya.Properties.Resources.Favorites,
			Arguments = "/page 2",
			Description = Gavilya.Properties.Resources.Favorites,
			CustomCategory = Gavilya.Properties.Resources.Tasks,
			IconResourcePath = Assembly.GetEntryAssembly()?.Location
		});

		jumpList.JumpItems.Add(new JumpTask()
		{
			Title = Gavilya.Properties.Resources.MyProfile,
			Arguments = "/page 3",
			Description = Gavilya.Properties.Resources.MyProfile,
			CustomCategory = Gavilya.Properties.Resources.Tasks,
			IconResourcePath = Assembly.GetEntryAssembly()?.Location
		});

		jumpList.ShowFrequentCategory = false;
		jumpList.ShowRecentCategory = false;

		JumpList.SetJumpList(Current, jumpList);
	}
}
