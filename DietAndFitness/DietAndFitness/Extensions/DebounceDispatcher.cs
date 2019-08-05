using System;
using System.Timers;

namespace DietAndFitness.Extensions
{
    public class DebounceDispatcher
    {
        private Timer timer = null;
        private Action methodToExecute;
        /// <summary>
        /// Debounce an event by resetting the event timeout every time the event is 
        /// fired. The behavior is that the Action passed is fired only after events
        /// stop firing for the given timeout period.
        /// 
        /// Use Debounce when you want events to fire only after events stop firing
        /// after the given interval timeout period.
        /// 
        /// Wrap the logic you would normally use in your event code into
        /// the  Action you pass to this method to debounce the event.
        /// </summary>
        /// <param name="interval">Timeout in Milliseconds</param>
        /// <param name="action">Action<object> to fire when debounced event fires</object></param>
        /// <param name="param">optional parameter</param>
        /// <param name="priority">optional priorty for the dispatcher</param>
        /// <param name="disp">optional dispatcher. If not passed or null CurrentDispatcher is used.</param>        
        public void Debounce(double interval, Action action)
        {
            // kill pending timer and pending ticks
            if (timer != null)
            {
                timer.Stop();
                timer.Elapsed -= MethodToExecute;
                timer = null;
            }

            methodToExecute = action;
            // timer is recreated for each event and effectively
            // resets the timeout. Action only fires after timeout has fully
            // elapsed without other events firing in between

            timer = new Timer(interval);
            timer.Elapsed += MethodToExecute;
            timer.Start();
            //timer = new Timer(TimeSpan.FromMilliseconds(interval), (s, e) =>
            //{
            //    if (timer == null)
            //        return;

            //    timer?.Stop();
            //    timer = null;
            //    action.Invoke(param);
            //});


        }

        private void MethodToExecute(object sender, ElapsedEventArgs e)
        {
            methodToExecute.Invoke();
            //prevent neverending loop
            timer.Stop();
        }
    }
}
