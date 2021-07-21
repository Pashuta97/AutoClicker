using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;
using System.Windows.Interop;
using Microsoft.Win32;

namespace Clicker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Bot _bot;
        private IniFile _ini = new IniFile("config.ini");
        private readonly Thread _runCommand;

        public MainWindow()
        {
            try
            {
                InitializeComponent();
                InitializeBot();
                InitializeTextBox();

                _runCommand = new Thread(() => RunCommand());
                _runCommand.Start();

            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK, MessageBoxOptions.ServiceNotification);
                MainWindow_Closing(this, new System.ComponentModel.CancelEventArgs());
            }
        }

        private void InitializeTextBox()
        {
            textBoxDelay.Text = _bot.Delay.ToString();
            textBoxRepeat.Text = _bot.Repeat.ToString();
            textBoxX.Text = _bot.Point.X.ToString();
            textBoxY.Text = _bot.Point.Y.ToString();
        }

        private void InitializeBot()
        {
            _bot = new Bot (ReadIniSettingsArray("Bot", "OptimalClicks"), ReadIniSettingsArray("Bot", "CursorPosition.X"), ReadIniSettingsArray("Bot", "CursorPosition.Y"))
            {
                Point = new POINT(int.Parse(_ini.Read("Bot", "Point.X")), int.Parse(_ini.Read("Bot", "Point.Y"))),
                Delay = int.Parse(_ini.Read("Bot", "Delay")),
                Repeat = int.Parse(_ini.Read("Bot", "Repeat")),
            };

            _bot.Stopped += DisplayMessageBox;
            _bot.Finished += DisplayMessageBox;
            _bot.Repeated += SetTextBoxRepeat;
            _bot.Changed += SetTextBoxXY;

            //foreach(string s in strSplited)
            //    _ini.Write("Bot", "OpimalClicks-"+s, s);
            //string cursorPosX = "";
            //string cursorPosY = "";
            //foreach (var cursorPos in _bot.CursorPosition)
            //{
            //    cursorPosX = cursorPosX + cursorPos.X.ToString() + ",\t";
            //    cursorPosY = cursorPosY + cursorPos.Y.ToString() + ",\t";
            //}
            //_ini.Write("Bot", "Point.X", _bot.Point.X.ToString());
            //_ini.Write("Bot", "Point.Y", _bot.Point.Y.ToString());
            //_ini.Write("Bot", "CursorPosition.X", cursorPosX);
            //_ini.Write("Bot", "CursorPosition.Y", cursorPosY);
            //_ini.Write("Bot", "Delay", _bot.Delay.ToString());
            //_ini.Write("Bot", "Repeat", _bot.Repeat.ToString());
        }

        private string[] ReadIniSettingsArray(string section, string key)
        {
            char[] separators = new char[] { ' ', ',', '.', ';', '\t' };
            string str = _ini.Read(section, key);
            return str.Split(separators, StringSplitOptions.RemoveEmptyEntries);
        }

        private void SetTextBoxXY(POINT point)
        {
            Dispatcher.Invoke(() => {
                textBoxX.Text = _bot.Point.X.ToString();
                textBoxY.Text = _bot.Point.Y.ToString();
            });
        }

        private void SetTextBoxRepeat(int repeat)
        {
            Dispatcher.Invoke(() => { textBoxRepeat.Text = _bot.Repeat.ToString(); });
        }

        private void DisplayMessageBox(string message)
        {
            MessageBox.Show(message, "Notifaction", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.OK, MessageBoxOptions.ServiceNotification);

        }

        private void Button_Click_Start(object sender, RoutedEventArgs e)
        {
            if (_bot.IsRun)
                _bot.Stop();
            else
                _bot.Run();
        }
        private void Button_Click_StartComdo(object sender, RoutedEventArgs e)
        {
            if (!_bot.IsRun) 
                _bot.RunCombo();
        }

        /// <summary>
        /// Check commands from keyboard each 100ms
        /// </summary>
        private void RunCommand()
        {
            while (true)
            {
                // stop _bot   
                if ((InputDeviceCommand.GetAsyncKeyState(InputDeviceCommand.VK_ESCAPE) != 0) && _bot.IsRun)
                {
                    _bot.Stop(); 
                    Dispatcher.Invoke(() => { buttonStart.Content = "Start"; });
                }
                //run _bot
                //if ((InputDeviceCommand.GetAsyncKeyState(InputDeviceCommand.VK_CONTROL) != 0) && (InputDeviceCommand.GetAsyncKeyState(InputDeviceCommand.VK_SPACE) != 0) && !_bot.IsRun)
                //{
                //    _bot.Run();
                //    Dispatcher.Invoke(() => { buttonStart.Content = "Stop"; });
                //}
                //set mouse position
                if ((InputDeviceCommand.GetAsyncKeyState(InputDeviceCommand.VK_CONTROL) != 0) && (InputDeviceCommand.GetAsyncKeyState(InputDeviceCommand.VK_MENU) != 0) &&
                    (InputDeviceCommand.GetAsyncKeyState(InputDeviceCommand.VK_Q) != 0) && !_bot.IsRun)
                    GetCursorPosition();

                Thread.Sleep(100); 
            }
        }

        /// <summary>
        /// Get cursor position and write coordinates to Point in Bot
        /// </summary>
        private void GetCursorPosition()
        {
            //POINT _point;
            InputDeviceCommand.GetCursorPos(out var point);

            _bot.Point = point;

            Dispatcher.Invoke(() => { 
                textBoxX.Text = _bot.Point.X.ToString();
                textBoxY.Text = _bot.Point.Y.ToString();
            });
        }

        /// <summary>
        /// Change number inside TextBox and save it in num
        /// </summary>
        private void ChangeNumTextBox(ref int num, TextBox textBox)
        {
            var temp = num;
            try
            {
                num = (int)Convert.ToUInt32(textBox.Text);
            }
            catch (Exception)
            {
                num = temp;
                textBox.Text = temp.ToString();
                textBox.Select(textBox.Text.Length, 0);
            }
        }

        private void TextBoxDelay_TextChanged(object sender, TextChangedEventArgs e)
        {
            var delay = _bot.Delay;
            ChangeNumTextBox(ref delay, textBoxDelay);
            _bot.Delay = delay;
        }

        private void TextBoxRepeat_TextChanged(object sender, TextChangedEventArgs e)
        {
            var repeat = _bot.Repeat;
            ChangeNumTextBox(ref repeat, textBoxRepeat);
            _bot.Repeat = repeat;
        }

        private void TextBoxY_TextChanged(object sender, TextChangedEventArgs e)
        {
            var point = _bot.Point;
            ChangeNumTextBox(ref point.Y, textBoxY);
            _bot.Point = point;
        }

        private void TextBoxX_TextChanged(object sender, TextChangedEventArgs e)
        {
            var point = _bot.Point;
            ChangeNumTextBox(ref point.X, textBoxX);
            _bot.Point = point;
        }
        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (_bot != null)
                if(_bot.IsRun) 
                    _bot.Stop();
            _runCommand?.Abort();
            Application.Current.Shutdown();
        }

        private void MenuItemLoadConfig_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Ini files(*.ini)|*.ini|All files (*.*)|*.*";
                if (openFileDialog.ShowDialog() == true)
                {
                    _ini = new IniFile(openFileDialog.FileName);
                    InitializeBot();
                    InitializeTextBox();
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK, MessageBoxOptions.ServiceNotification);
                MainWindow_Closing(this, new System.ComponentModel.CancelEventArgs());
            }
            
                
        }
    }

}
