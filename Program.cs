using System;

namespace StackOverflowPost
{
    class Program
    {
        static void Main(string[] args)
        {
            var postEditor = new PostEditor();
            var programController = new ProgramController(postEditor);

            postEditor.PostCreation += programController.OnPostCreation;

            string create = ConvertCommand(ProgramController.CreateCommand);
            string upVote = ProgramController.UpVoteCommand;
            string downVote = ProgramController.DownVoteCommand;
            string exit = ConvertCommand(ProgramController.ExitCommand);

            System.Console.WriteLine("'{0}' to create a new post.", create);
            System.Console.WriteLine("'{0}' to upvote.", upVote);
            System.Console.WriteLine("'{0}' to downvote.", downVote);
            System.Console.WriteLine("'{0}' to close the program.", exit);

            programController.ProcessSite();
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
