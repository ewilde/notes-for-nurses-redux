// -----------------------------------------------------------------------
// <copyright file="Retry.cs">
// Copyright Edward Wilde (c) 2013.
// </copyright>
// -----------------------------------------------------------------------
namespace Edward.Wilde.Note.For.Nurses.Core
{
    using System;
    using System.Linq.Expressions;
    using System.Threading;

    using Edward.Wilde.Note.For.Nurses.Core.Xamarin;

    public static class Execute
    {
        public static double DefaultTimeout = System.TimeSpan.FromSeconds(5).TotalMilliseconds;
        public static ExecuteResult Until(Action method)
        {
            
                var timer = new System.Diagnostics.Stopwatch();
                timer.Start();
                var sleepTime = 100;
                var result = new ExecuteResult();
                do
                {
                    try
                    {
                        method.Invoke();
                        return result;
                    }
                    catch (Exception exception)
                    {
                        Thread.Sleep(sleepTime);
                        sleepTime = sleepTime + sleepTime; // increase waits between retry attempts
                        result.LastException = exception;
                        ConsoleD.WriteError(exception.ToString());
                    }
                }
                while (timer.ElapsedMilliseconds <= DefaultTimeout );

            return result;
        }
    }

    public class ExecuteResult
    {
        public Exception LastException { get; set; }

        public bool ErrorOccured
        {
            get
            {
                return this.LastException != null;
            }
        }
    }
}