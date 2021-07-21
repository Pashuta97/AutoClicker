using System;
using System.Windows;
using System.Threading;
using System.Collections.Generic;

namespace Clicker
{
    /// <summary>
    /// class Bot for Clicker
    /// </summary>
    public delegate void BotMessageHandler(string message);
    public delegate void BotRepeatHandler(int repeat);
    public delegate void BotPointHandler(POINT point);
    public class Bot
    {
        private Thread _thread;
        private POINT _point;

        //private readonly int[] _optimalClicks = { 5, 5, 5, 5, 5, 5, 5, 5, 5 };
        private readonly List<int> _optimalClicks;
        private readonly List<POINT> _cursorPosition;

        public event BotMessageHandler Stopped;
        public event BotMessageHandler Finished;
        public event BotRepeatHandler Repeated;
        public event BotPointHandler Changed;

        public int Repeat { get; set; } // Counter repeat combos
        public int Delay { get; set; }
        public bool IsRun { get; private set; }
        public POINT Point {
            get 
            {
                return _point;
            }
            set 
            { 
                _point = value;  
            } 
        }

        public Bot()
        {
            _optimalClicks = new List<int>();//{ 833, 375, 150, 42, 50, 32, 25, 47, 10 };
            _cursorPosition = new List<POINT>();//{ new POINT(1340, 360), new POINT(1340, 400), new POINT(1340, 440), new POINT(1340, 480), new POINT(1340, 520),
            //                                   new POINT(1340, 560), new POINT(1340, 600), new POINT(1340, 640), new POINT(1340, 680) };
        }

        public Bot(string [] strOptimalClicks, string [] strCursorPositionX, string[] strCursorPositionY)
        {
            if ((strOptimalClicks.Length != strCursorPositionX.Length)||(strOptimalClicks.Length != strCursorPositionY.Length))
                throw new Exception("OptimalClicks.Length != CursorPosition.Length, check config!");
            else
            {
                _optimalClicks = new List<int>();
                _cursorPosition = new List<POINT>();
                for (int index = 0; index < strOptimalClicks.Length; index++)
                {
                    _optimalClicks.Add(int.Parse(strOptimalClicks[index]));
                    _cursorPosition.Add(new POINT(int.Parse(strCursorPositionX[index]), int.Parse(strCursorPositionY[index])));
                }
            }
        }
        /// <summary>
        /// Run Bot
        /// </summary>
        public void Run()
        {
            if (!IsRun)
            {
                IsRun = true;
                _thread = new Thread(() => SendClickLooped());
                _thread.Start();
            }
        }

        /// <summary>
        /// Stop Bot
        /// </summary>
        public void Stop(string message = "")
        {
            IsRun = false;
            if (_thread != null) _thread.Abort();
            Stopped?.Invoke($"Clicker stopped! {message}");
        }

        /// <summary>
        /// Send left click on Point coordinates with some Delay
        /// </summary>
        private void SendClickLooped()
        {
            while (IsRun)
            {
                InputDeviceCommand.SendClick(Point);
                Thread.Sleep(Delay);
            }
        }

        public void RunCombo()
        {
            if (!IsRun)
            {
                IsRun = true;
                if (Repeat == 0) _thread = new Thread(() => ComboUnlimited());
                    else _thread = new Thread(() => Combo());
                _thread.Start();
            }
        }

        private void ComboUnlimited()
        {
            while (IsRun)
            {
                ComboCycle();
            }
        }

        private void Combo()
        {
            while (Repeat > 0)
            {
                ComboCycle();
                Repeat--;
                Repeated?.Invoke(Repeat);
            }
            IsRun = false;

            Finished?.Invoke("Clicker`s combo work has finished!");
        }

        private void ComboCycle()
        {
            for (var index = 0; index < _optimalClicks.Count; index++)
            {
                _point = _cursorPosition[index];
                Changed?.Invoke(_point);
                for (var click = 0; click < _optimalClicks[index]; click++)
                {
                    InputDeviceCommand.SendClick(_point);
                    Thread.Sleep(Delay);
                }
            }
        }

        //private void Combo()
        //{
        //    POINT point = _point;
        //    while (Repeat > 0)
        //    {
        //        foreach (var clicks in _optimalClicks)
        //        {
        //            for (var index = 0; index < clicks; index++)
        //            {
        //                InputDeviceCommand.SendClick(_point);
        //                Thread.Sleep(Delay);
        //            }
        //            _point.Y += 40;
        //            Changed?.Invoke(_point);
        //        }
        //        _point = point;
        //        Changed?.Invoke(_point);

        //        Repeat--;
        //        Repeated?.Invoke(Repeat);
        //    }
        //    IsRun = false;

        //    Finished?.Invoke("Clicker`s combo work has finished!");
        //}
    }

}
