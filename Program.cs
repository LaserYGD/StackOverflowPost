using System;

namespace StackOverflowPost
{
    class Program
    {
        static void Main(string[] args)
        {
            Post post = null;

            string create = ConvertCommand(ProgramController.CreateCommand);
            string upVote = ProgramController.UpVoteCommand;
            string downVote = ProgramController.DownVoteCommand;
            string exit = ConvertCommand(ProgramController.ExitCommand);

            System.Console.WriteLine("'{0}' to create a new post.", create);
            System.Console.WriteLine("'{0}' to upvote.", upVote);
            System.Console.WriteLine("'{0}' to downvote.", downVote);
            System.Console.WriteLine("'{0}' to close the program.", exit);

            while (true)
            {
                ProgramController.GetUserCommand();

                if (!ProgramController.CommandIsValid())
                {
                    System.Console.WriteLine("Invalid command. Use '{0}', '{1}', '{2}' or '{3}'.", create, upVote, downVote, exit);
                    continue;
                }

                post = ProgramController.CreatePost(post);

                ProgramController.VoteForPost(post);

                if (ProgramController.CommandIsExit())
                {
                    break;
                }
            }

            ShowResultVotes(post);
        }

        private static string ConvertCommand(string command)
        {
            return char.ToUpper(command[0]) + command.ToLower().Substring(1);
        }

        private static void ShowResultVotes(Post post)
        {
            if (post != null)
            {
                System.Console.WriteLine("Votes: " + post.Votes);
            }
            else
            {
                System.Console.WriteLine("You haven't create a post.");
            }
        }
    }
}
