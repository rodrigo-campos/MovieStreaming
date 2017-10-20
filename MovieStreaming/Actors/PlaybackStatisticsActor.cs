using Akka.Actor;
using System;

namespace MovieStreaming.Actors
{
    public class PlaybackStatisticsActor : ReceiveActor
    {
        public PlaybackStatisticsActor()
        {
            Context.ActorOf(Props.Create<MoviePlayCounterActor>(), "MoviePlayCounter");
        }

        private void Print(string message)
        {
            ColorConsole.WriteLineWhite($"PlaybackStatisticsActor {message}");
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
            Print($"PreRestart because: {reason}");

            base.PreRestart(reason, message);
        }

        protected override void PostRestart(Exception reason)
        {
            Print($"PostRestart because: {reason}");

            base.PostRestart(reason);
        }
        #endregion Lifecycle hooks
    }
}
