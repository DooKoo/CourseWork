using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CourseWork
{
    class Command
    {
        public String Text;
        public String Description;
        public delegate void MethodDelegate();
        private MethodDelegate StorageAction;

        /// <summary>
        /// Default constructor for Command class
        /// </summary>
        /// <param name="text">text of command</param>
        /// <param name="description">description of command</param>
        public Command(String text, String description)
        {
            Text = text;
            Description = description;
        }

        /// <summary>
        /// Set action for current command
        /// </summary>
        /// <param name="action">function that will be run</param>
        public void SetAction(MethodDelegate action)
        {
            StorageAction = action;
        }

        /// <summary>
        /// method that run the function
        /// </summary>
        /// <param name="callBack">funciton that will be run after done!</param>
        public void Action(MethodDelegate callBack)
        {
            try
            {
                StorageAction();
                Console.WriteLine("Done!");
            }
            catch (Exception e)
            {
                Console.WriteLine("Oops. {0}. Try again please!", e.Message);
                Thread.Sleep(3000);
            }
            finally
            {
                callBack();
            }
        }
    }
}
