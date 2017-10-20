using Akka.Actor;
using MovieStreaming.Messages;
using System;

namespace MovieStreaming.Actors
{
    public class UserActor : ReceiveActor
    {
        private string _currentlyWatching;
        private int _userId;

        public UserActor(int userId)
        {
            _userId = userId;
            Stopped();
        }

        private void Print(string message)
        {
            ColorConsole.WriteLineYellow($"UserActor {_userId} {message}");
        }

        private void Playing()
        {
            Receive<PlayMovieMessage>(
                message => ColorConsole.WriteLineRed("Error: cannot start playing another movie before stoping existing one"));
            Receive<StopMovieMessage>(message => StopPlayingCurrentMovie());

            Print("has now become Playing");
        }

        private void Stopped()
        {
            Receive<PlayMovieMessage>(message => StartPlayingMovie(message.MovieTitle));
            Receive<StopMovieMessage>(
                message => ColorConsole.WriteLineRed("Error: cannot stop if nothing is playing"));

            Print("has now become Stopped");
        }

        private void StartPlayingMovie(string title)
        {
            _currentlyWatching = title;

            Print($"is currently watching '{_currentlyWatching}'");

            Context.ActorSelection("/user/Playback/PlaybackStatistics/MoviePlayCounter")
                .Tell(new IncrementPlayCountMessage(title));

            Become(Playing);
        }

        private void StopPlayingCurrentMovie()
        {
            Print($"has stopped watching '{_currentlyWatching}'");

            _currentlyWatching = null;

            Become(Stopped);
        }

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
    }
}
