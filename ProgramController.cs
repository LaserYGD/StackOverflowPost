using System;

namespace StackOverflowPost
{
    public class ProgramController
    {
        private static string _createCommand = "create";
        private static string _upVoteCommand = "+1";
        private static string _downVoteCommand = "-1";
        private static string _exitCommand = "exit";

        public static string UserCommand { get; set; }

        public static string CreateCommand
        {
            get { return _createCommand; }
        }

        public static string UpVoteCommand
        {
            get { return _upVoteCommand; }
        }

        public static string DownVoteCommand
        {
            get { return _downVoteCommand; }
        }

        public static string ExitCommand
        {
            get { return _exitCommand; }
        }

        public static void GetUserCommand()
        {
            var input = Console.ReadLine();
            UserCommand = input.ToLower();
        }

        public static bool ValidateUserCommand()
        {
            if (String.IsNullOrWhiteSpace(UserCommand))
            {
                return false;
            }

            var isCreate = UserCommand == _createCommand;
            var isUpVote = UserCommand == _upVoteCommand;
            var isDownVote = UserCommand == _downVoteCommand;
            var isExit = UserCommand == _exitCommand;

            if ((!isCreate) || (!isUpVote) || (!isDownVote) || (!isExit))
            {
                return false;
            }

            return true;
        }
    }
}