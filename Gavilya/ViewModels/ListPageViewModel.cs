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

using Gavilya.Models;
using System.Collections.Generic;
using System.Linq;

namespace Gavilya.ViewModels;

public class ListPageViewModel : ViewModelBase
{
	public GameList Games { get; set; }
	public List<GameListViewModel> GamesVm { get => _gamesVm; set { _gamesVm = value; OnPropertyChanged(nameof(GamesVm)); } }

	private ViewModelBase _viewModelBase;
	private List<GameListViewModel> _gamesVm;
	private List<GameListViewModel> _favGamesVm;
	private readonly List<Tag> _tags;
	private readonly MainViewModel _mainViewModel;

	public ViewModelBase CurrentGameView { get => _viewModelBase; set { _viewModelBase = value; OnPropertyChanged(nameof(CurrentGameView)); } }

	public ListPageViewModel(GameList games, List<Tag> tags, MainViewModel mainViewModel)
	{
		Games = games;
		_tags = tags;
		_mainViewModel = mainViewModel;
		CurrentGameView = new ListPlaceholder();

		GamesVm = Games.Where(g => (_mainViewModel.CurrentSettings.ShowHiddenGames || !g.IsHidden)).Select(g => new GameListViewModel(g, Games, _tags, this, _mainViewModel)).ToList();

		Games.CollectionChanged += (o, e) =>
		{
			GamesVm = Games.Where(g => (_mainViewModel.CurrentSettings.ShowHiddenGames || !g.IsHidden)).Select(g => new GameListViewModel(g, Games, _tags, this, _mainViewModel)).ToList();
		};
	}
}
