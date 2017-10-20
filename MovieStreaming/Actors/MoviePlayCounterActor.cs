using Akka.Actor;
using MovieStreaming.Messages;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace MovieStreaming.Actors
{
    public class MoviePlayCounterActor : ReceiveActor
    {
        private readonly Dictionary<string, int> _moviePlayCounts;

        public MoviePlayCounterActor()
        {
            Print("constructor executing");

            _moviePlayCounts = new Dictionary<string, int>();

            Receive<IncrementPlayCountMessage>(message => HandleIncrementMessage(message));
        }

        private void HandleIncrementMessage(IncrementPlayCountMessage message)
        {
            if (_moviePlayCounts.ContainsKey(message.MovieTitle))
            {
                _moviePlayCounts[message.MovieTitle]++;
            }
            else
            {
                _moviePlayCounts.Add(message.MovieTitle, 1);
            }

            if (message.MovieTitle == "fail")
            {
                throw new SimulatedFailException();
            }

            Print($"'{message.MovieTitle}' has been watched {_moviePlayCounts[message.MovieTitle]} times");
        }

        private void Print(string message)
        {
            ColorConsole.WriteLineMagenta($"MoviePlayCounterActor {message}");
        }

        #region Lifecycle hooks
        protected override void PreStart()
        {
            Print("PreStart");
        }

        protected override void PostStop()
        {
            Print("PostStop");
        }

        protected override void PreRestart(Exception reason, object message)
        {
            Print($"PreRestart because: {reason.Message}");

            base.PreRestart(reason, message);
        }

        protected override void PostRestart(Exception reason)
        {
            Print($"PostRestart because: {reason.Message}");

            base.PostRestart(reason);
        }

        [Serializable]
        private class SimulatedFailException : Exception
        {
            public SimulatedFailException() { }
        }
        #endregion Lifecycle hooks
    }
}
