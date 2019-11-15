using System;

namespace StackOverflowPost
{
    class Program
    {
        static void Main(string[] args)
        {
            var postEditor = new PostEditor();
            var programController = new ProgramController(postEditor);
            var messageHandler = new MessageHandler();

            postEditor.PostCreation += programController.OnPostCreation;
            postEditor.PostCreation += messageHandler.OnPostCreation;

            programController.InvalidCommand += messageHandler.OnInvalidCommand;
            programController.CreateTitle += messageHandler.OnCreateTitle;
            programController.CreateDescription += messageHandler.OnCreateDescription;
            programController.PostExists += messageHandler.OnPostExists;
            programController.PostNotFound += messageHandler.OnPostNotFound;
            programController.CommandUpVote += messageHandler.OnUpVoteCommand;
            programController.CommandDownVote += messageHandler.OnDownVoteCommand;
            programController.ShowVotes += messageHandler.OnShowVotes;

            messageHandler.ShowInstructions();

            programController.ProcessSite();
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
