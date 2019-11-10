using System;

namespace StackOverflowPost
{
    public class ProgramController
    {
        private string _userCommand;
        private string _createCommand = "create";
        private string _upVoteCommand = "+1";
        private string _downVoteCommand = "-1";
        private string _exitCommand = "exit";

        public static string UserCommand { get; set; }

        public string CreateCommand
        {
            get { return _createCommand; }
        }

        public string UpVoteCommand
        {
            get { return _upVoteCommand; }
        }

        public string DownVoteCommand
        {
            get { return _downVoteCommand; }
        }

        public string ExitCommand
        {
            get { return _exitCommand; }
        }

        public static void GetUserCommand()
        {
            var input = Console.ReadLine();
            UserCommand = input.ToLower();
        }
    }
}