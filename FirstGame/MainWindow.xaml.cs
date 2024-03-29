﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace FirstGame
{
	using System.Windows.Threading;
	public partial class MainWindow : Window
	{
		DispatcherTimer timer = new DispatcherTimer();
		int matchesFound;
		int secondsPassed;
		public MainWindow()
		{
			InitializeComponent();

			timer.Interval = TimeSpan.FromSeconds(.1);
			timer.Tick += Timer_Tick;
			SetUpGame();
		}

		private void Timer_Tick(object sender, EventArgs e)
		{
			secondsPassed++;
			timeTextBlock.Text = (secondsPassed / 10F).ToString("0.0s");
			if (matchesFound == 8)
			{
				timer.Stop();
				timeTextBlock.Text = timeTextBlock.Text + " - Play again?";
			}
		}

		private void SetUpGame()
		{

			List<string> animalEmoji = new List<string>()
			{
				"🦊", "🦊",
				"🐎", "🐎",
				"🦄", "🦄",
				"🐸", "🐸",
				"🐲", "🐲",
				"🦈", "🦈",
				"🐋", "🐋",
				"🦨", "🦨"
			};
			Random rand = new Random();

			foreach (TextBlock textblock in mainGrid.Children.OfType<TextBlock>())
			{
				textblock.Visibility = Visibility.Visible;
				if (textblock.Name != "timeTextBlock")
				{
					Console.WriteLine();
					int index = rand.Next(animalEmoji.Count);
					string nextEmoji = animalEmoji[index];
					textblock.Text = nextEmoji;
					animalEmoji.RemoveAt(index);
				}
			}
			timer.Start();
			secondsPassed = 0;
			matchesFound = 0;


		}

		TextBlock lastTextBlockClicked;
		bool findingMatch = false;

		private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
		{
			TextBlock textBlock = sender as TextBlock;
			if (findingMatch == false)
			{
				textBlock.Visibility = Visibility.Hidden;
				lastTextBlockClicked = textBlock;
				findingMatch = true;
			}
			else if (textBlock.Text == lastTextBlockClicked.Text)
			{
				matchesFound++;
				textBlock.Visibility = Visibility.Hidden;
				findingMatch = false;
			}
			else
			{
				lastTextBlockClicked.Visibility = Visibility.Visible;
				findingMatch = false;
			}
		}

		private void timeTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
		{
			if (matchesFound == 8)
			{
				SetUpGame();
			}
		}
	}
}
