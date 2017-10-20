using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;
using MovieStreaming.Messages;

namespace MovieStreaming.Actors
{
    public class PlaybackActor : ReceiveActor
    {
        public PlaybackActor()
        {
            Console.WriteLine("Creating a PlaybackActor");

            Receive<PlayMovieMessage>(message => HandlePlayMovieMessage(message));
        }

        private void HandlePlayMovieMessage(PlayMovieMessage message)
        {
            ColorConsole.WriteLineYellow($"PlayMovieMessage '{message.MovieTitle}' for user {message.UserId}");
        }
    }
}
