using System;
using System.Media;
using System.Threading;
using System.Windows.Forms;

namespace DrunkPC
{
    class Program
    {
        public static Random _random = new Random();
        /// <summary>
        /// Entry point
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            //Create the threads
            Thread drunkMouseThread = new Thread(new ThreadStart(DrunkMouseThread));
            Thread drunkKeyboardThread = new Thread(new ThreadStart(DrunkKeyboardThread));
            Thread drunkSoundThread = new Thread(new ThreadStart(DrunkSoundThread));
            Thread drunkPopupThread = new Thread(new ThreadStart(DrunkPopupThread));
            
            DateTime future = DateTime.Now.AddSeconds(10);
            while (future > DateTime.Now)
            {
                Thread.Sleep(1000);
            }

            //Start the threads
            drunkMouseThread.Start();
            drunkKeyboardThread.Start();
            drunkSoundThread.Start();
            drunkPopupThread.Start();

            future = DateTime.Now.AddSeconds(10);
            while (future > DateTime.Now)
            {
                Thread.Sleep(1000);
            }

            //Abort all threads
            drunkMouseThread.Abort();
            drunkKeyboardThread.Abort();
            drunkSoundThread.Abort();
            drunkPopupThread.Abort();
        }
        #region Thread Functions
        /// <summary>
        /// This thread will randomly affect the mouse movements to screw with the victimn
        /// </summary>
        public static void DrunkMouseThread()
        {
            int moveX = 0, moveY = 0;
            while (true)
            {
                //Console.WriteLine(Cursor.Position.ToString());

                //Generate random X and Y
                moveX = _random.Next(20) - 10;
                moveY = _random.Next(20) - 10;

                //Move the mouse
                Cursor.Position = new System.Drawing.Point(Cursor.Position.X + moveX, Cursor.Position.Y + moveY);
                Thread.Sleep(50);
            }
        }
        /// <summary>
        /// This thread generate random keyboard outputs to screw with the vitcim
        /// </summary>
        public static void DrunkKeyboardThread()
        {
            while (true)
            {
                //Get a random char
                char key = (char)_random.Next(65, 90 + 1);

                //Upper or lowercase
                if (_random.Next(2) == 0)
                {
                    key = char.ToLower(key);
                }

                SendKeys.SendWait(key.ToString());
                Thread.Sleep(_random.Next(500));
            }
        }
        /// <summary>
        /// This will play system sounds at random to screw with the victim
        /// </summary>
        public static void DrunkSoundThread()
        {
            while (true)
            {
                //If the random number is above 80 (about 20% odds) then play a sound
                if (_random.Next(100 + 1) > 80)
                {
                    //Pick a random sound
                    switch (_random.Next(5))
                    {
                        case 0:
                            SystemSounds.Asterisk.Play();
                            break;
                        case 1:
                            SystemSounds.Beep.Play();
                            break;
                        case 2:
                            SystemSounds.Exclamation.Play();
                            break;
                        case 3:
                            SystemSounds.Hand.Play();
                            break;
                        case 4:
                            SystemSounds.Question.Play();
                            break;
                    }
                }
                Thread.Sleep(1000);
            }
        }
        /// <summary>
        /// This thread will make fake error popups
        /// </summary>
        public static void DrunkPopupThread()
        {
            while (true)
            {
                //10% chance
                if (_random.Next(100 + 1) > 90)
                {
                    switch (_random.Next(2))
                    {
                        case 0:
                            MessageBox.Show("Internet Explorer has stopped working", "Internet Explorer", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        case 1:
                            MessageBox.Show("Your system is running low on resources", "Microsoft Windows", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                    }
                }
                Thread.Sleep(10 * 1000);
            }
        }
        #endregion
    }
}
