using System;

namespace Tetris
{
    public interface IScore
    {
        int InitialInterval { get; }
        int TimerInterval { get; }

        event EventHandler NewLevel;

        void AddLines(int lines);
        void Clear();
    }

    public class Score : IScore
    {
		private readonly int _linesPerLevel, _intervalDecreasePerLevel, _initialInterval;
		private readonly int[] _scoreMultiplier;
		private int _totalLines, _level, _timerInterval, _value;
		
		public event EventHandler NewLevel;
		
		public int Level 
		{ 
			get { return _level; } 
			private set 
			{
				if (_level == value) return;
				
				_level = value;
				_timerInterval = _initialInterval - _level * _intervalDecreasePerLevel;
				OnNewLevel(new EventArgs());
			}
		}
		
		public int TotalLines 
		{ 
			get { return _totalLines; } 
			set
			{
				if (_totalLines == value) return;
				
				_totalLines = value;
				Level = _totalLines / _linesPerLevel;
			}
		}
		
		public int LinesPerLevel { get { return _linesPerLevel; } }
        public int InitialInterval { get { return _initialInterval; } }
        public int IntervalDecreasePerLevel { get { return _intervalDecreasePerLevel; } }
		public int TimerInterval { get { return _timerInterval; } }
		public int Value { get { return _value; } }

		public void AddLines(int lines)
		{
		   TotalLines += lines;
	
		    if (lines == 0) return;
		    
            _value += (_level + 1) * _scoreMultiplier[lines - 1];
        }

		public void Clear()
		{
            TotalLines = 0;
		    _value = 0;
		}
		
		public static int[] DefaultScoreMultiplier()
		{
			return new [] { 40, 100, 300, 1200 };
		}
		
		public Score(int linesPerLevel, int initialInteval, int intervalDecreasePerLevel, int[] scoreMultiplier)
		{
			_linesPerLevel = linesPerLevel;
		    _initialInterval = initialInteval;
            _intervalDecreasePerLevel = intervalDecreasePerLevel;
			_scoreMultiplier = scoreMultiplier; 
		}
		
		protected virtual void OnNewLevel(EventArgs e)
		{
			if (NewLevel != null) NewLevel.Invoke(this, e);
		}
		
		public override string ToString()
		{
			return string.Format("Total lines: {0}\nLevel: {1}\nScore: {2}", _totalLines, _level, _value);
		}

	}
}
