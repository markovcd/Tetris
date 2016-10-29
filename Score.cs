﻿using System;

namespace Tetris
{
		public class Score
	{
		private readonly int _linesPerLevel, _intervalDecreasePerLevel, _initialInterval;
		private readonly int[] _scoreMultiplier;
		private int _totalLines, _level, _timerInterval, _value;
		
		public int Level { get { return _level; } }
		public int LinesPerLevel { get { return _linesPerLevel; } }
        public int InitialInterval { get { return _initialInterval; } }
        public int IntervalDecreasePerLevel { get { return _intervalDecreasePerLevel; } }
		public int TimerInterval { get { return _timerInterval; } }
		public int Value { get { return _value; } }
		
		public int TotalLines { get { return _totalLines; } }

		public void AddLines(int lines)
		{
		    _totalLines += lines;
            _level = _totalLines / _linesPerLevel;
            _timerInterval = _initialInterval - _level * _intervalDecreasePerLevel;
		    if (lines == 0) return;
            _value = (_level + 1) * _scoreMultiplier[lines - 1];
        }

		public void Clear()
		{
		    _totalLines = 0;
		    _level = 0;
		    _timerInterval = _initialInterval;
		    _value = 0;
		}
		
		public static int[] DefaultScoreMultiplier()
		{
			return new int[] { 40, 100, 300, 1200 };
		}
		
		public Score(int linesPerLevel = 10, int initialInteval = 1000, int intervalDecreasePerLevel = 100, int[] scoreMultiplier = null)
		{
			_linesPerLevel = linesPerLevel;
		    _initialInterval = initialInteval;
            _intervalDecreasePerLevel = intervalDecreasePerLevel;
			_scoreMultiplier = scoreMultiplier ?? DefaultScoreMultiplier();
		}
		
		public override string ToString()
		{
			return string.Format("Total lines: {0}\nLevel: {1}\nScore: {2}", _totalLines, _level, _value);
		}

	}
}
