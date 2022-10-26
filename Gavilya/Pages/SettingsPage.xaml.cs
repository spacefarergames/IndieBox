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

using Gavilya.Classes;
using Gavilya.Pages.SettingsPages;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Gavilya.Pages;
/// <summary>
/// Interaction logic for SettingsPage.xaml
/// </summary>
public partial class SettingsPage : Page
{
	readonly SaveOptionsPage saveOptionsPage = new(); // Create a page
	readonly LanguagePage languagePage = new(); // Create a page
	readonly StartupPage startupPage = new(); // Create a page
	readonly DataOptionsPage dataOptionsPage = new(); // Create a page
	readonly HomeOptionsPage homeOptionsPage = new(); // Create a page
	readonly SearchOptionsPage searchOptionsPage = new(); // Create a page
	readonly FpsOptionsPage fpsOptionsPage = new(); // Create a page
	public SettingsPage()
	{
		InitializeComponent();
	}

	private void NavigateToPage(Enums.SettingsPages settingsPage)
	{
		switch (settingsPage)
		{
			case Enums.SettingsPages.Languages:
				UnCheckAll(); // Uncheck all buttons

				LanguageOptions.Background = Definitions.HomeButtonBackColor; // Set the new background
				languagePage.Visibility = Visibility.Visible; // Show the page
				OptionsDisplayer.Navigate(languagePage); // Navigate
				break;
			case Enums.SettingsPages.SaveOptions:
				UnCheckAll(); // Uncheck all buttons

				SaveOptions.Background = Definitions.HomeButtonBackColor; // Set the new background
				saveOptionsPage.Visibility = Visibility.Visible; // Show the page
				OptionsDisplayer.Navigate(saveOptionsPage); // Navigate
				break;
			case Enums.SettingsPages.Startup:
				UnCheckAll(); // Uncheck all buttons

				StartupOptions.Background = Definitions.HomeButtonBackColor; // Set the new background
				startupPage.Visibility = Visibility.Visible; // Show the page
				OptionsDisplayer.Navigate(startupPage); // Navigate
				break;
			case Enums.SettingsPages.Data:
				UnCheckAll(); // Uncheck all buttons

				DataOptions.Background = Definitions.HomeButtonBackColor; // Set the new background
				dataOptionsPage.Visibility = Visibility.Visible; // Show the page
				OptionsDisplayer.Navigate(dataOptionsPage); // Navigate
				break;
			case Enums.SettingsPages.Home:
				UnCheckAll(); // Uncheck all buttons

				HomeOptions.Background = Definitions.HomeButtonBackColor; // Set the new background
				homeOptionsPage.Visibility = Visibility.Visible; // Show the page
				OptionsDisplayer.Navigate(homeOptionsPage); // Navigate
				break;
			case Enums.SettingsPages.Search:
				UnCheckAll(); // Uncheck all buttons

				SearchOptions.Background = Definitions.HomeButtonBackColor; // Set the new background
				searchOptionsPage.Visibility = Visibility.Visible; // Show the page
				OptionsDisplayer.Navigate(searchOptionsPage); // Navigate
				break;
			case Enums.SettingsPages.FPS:
				UnCheckAll(); // Uncheck all buttons

				FpsOptions.Background = Definitions.HomeButtonBackColor; // Set the new background
				fpsOptionsPage.Visibility = Visibility.Visible; // Show the page
				OptionsDisplayer.Navigate(fpsOptionsPage); // Navigate
				break;
		}
	}

	private void UnCheckAll()
	{
		SaveOptions.Background = new SolidColorBrush { Color = Colors.Transparent }; // Remove the background
		LanguageOptions.Background = new SolidColorBrush { Color = Colors.Transparent }; // Remove the background
		StartupOptions.Background = new SolidColorBrush { Color = Colors.Transparent }; // Remove the background
		DataOptions.Background = new SolidColorBrush { Color = Colors.Transparent }; // Remove the background
		HomeOptions.Background = new SolidColorBrush { Color = Colors.Transparent }; // Remove the background
		SearchOptions.Background = new SolidColorBrush { Color = Colors.Transparent }; // Remove the background
		FpsOptions.Background = new SolidColorBrush { Color = Colors.Transparent }; // Remove the background
	}

	private void SaveOptions_Click(object sender, RoutedEventArgs e)
	{
		NavigateToPage(Enums.SettingsPages.SaveOptions); // Show the "SaveOptions" page
	}

	private void LanguageOptions_Click(object sender, RoutedEventArgs e)
	{
		NavigateToPage(Enums.SettingsPages.Languages); // Show the "Languages" page
	}

	private void StartupOptions_Click(object sender, RoutedEventArgs e)
	{
		NavigateToPage(Enums.SettingsPages.Startup); // Show the "Startup" page
	}

	private void DataOptions_Click(object sender, RoutedEventArgs e)
	{
		NavigateToPage(Enums.SettingsPages.Data); // Show the "Data" page
	}

	private void HomeOptions_Click(object sender, RoutedEventArgs e)
	{
		NavigateToPage(Enums.SettingsPages.Home); // Show the "Home" page
	}

	private void SearchOptions_Click(object sender, RoutedEventArgs e)
	{
		NavigateToPage(Enums.SettingsPages.Search); // Show the "Search" page
	}

	private void FpsOptions_Click(object sender, RoutedEventArgs e)
	{
		NavigateToPage(Enums.SettingsPages.FPS); // Show the "FPS" page
	}
}
