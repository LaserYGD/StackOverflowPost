using System;

namespace StackOverflowPost
{
    public class MessageHandler
    {
        private string _create = ConvertCommand(ProgramController.CreateCommand);
        private string _upVote = ProgramController.UpVoteCommand;
        private string _downVote = ProgramController.DownVoteCommand;
        private string _votes = ConvertCommand(ProgramController.VotesCommand);
        private string _exit = ConvertCommand(ProgramController.ExitCommand);

        private static string ConvertCommand(string command)
        {
            return char.ToUpper(command[0]) + command.ToLower().Substring(1);
        }

        public void ShowInstructions()
        {
            System.Console.WriteLine("'{0}' to create a new post.", _create);
            System.Console.WriteLine("'{0}' to upvote.", _upVote);
            System.Console.WriteLine("'{0}' to downvote.", _downVote);
            System.Console.WriteLine("'{0}' to show votes.", _votes);
            System.Console.WriteLine("'{0}' to close the program.", _exit);
        }

        public void OnInvalidCommand(object source, EventArgs empty)
        {
            System.Console.WriteLine("Invalid command. Please use '{0}', '{1}', '{2}', '{3}', '{4}'.", _create, _upVote, _downVote, _votes, _exit);
        }

        public void OnCreateTitle(object source, EventArgs empty)
        {
            System.Console.WriteLine("Please, enter the title of your post.");
        }

        public void OnCreateDescription(object source, EventArgs empty)
        {
            System.Console.WriteLine("Please, enter the description of your post.");
        }

        public void OnPostExists(object source, EventArgs empty)
        {
            System.Console.WriteLine("Post already exists.");
        }

        public void OnPostNotFound(object source, EventArgs empty)
        {
            System.Console.WriteLine("Please create a new post with '{0}' command.", _create);
        }

        public void OnPostCreation(object source, NewPostArgs args)
        {
            System.Console.WriteLine("A post '{0}' has been created.", args.Title);
        }

        public void OnUpVoteCommand(object source, PostHandlerArgs args)
        {
            System.Console.WriteLine("The post '{0}' has been upvoted.", args.Title);
        }

        public void OnDownVoteCommand(object source, PostHandlerArgs args)
        {
            System.Console.WriteLine("The post '{0}' has been downvoted.", args.Title);
        }

        public void OnShowVotes(object source, PostHandlerArgs args)
        {
            System.Console.WriteLine("The post '{0}' has {1} votes.", args.Title, args.Votes);
        }
    }
}