using System;

namespace StackOverflowPost
{
    public class ProgramController
    {
        private static string _createCommand = "create";
        private static string _upVoteCommand = "+1";
        private static string _downVoteCommand = "-1";
        private static string _exitCommand = "exit";

        #region Properties
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
        #endregion

        #region Private Methods
        private static void ProcessUpVote(Post post)
        {
            if (CommandIsUpVote())
            {
                post.UpVote();
            }
        }

        private static void ProcessDownVote(Post post)
        {
            if (CommandIsDownVote())
            {
                post.DownVote();
            }
        }
        #endregion

        #region Public methods
        public static void GetUserCommand()
        {
            var input = Console.ReadLine();
            UserCommand = input.ToLower();
        }

        public static bool CommandIsValid()
        {
            if (String.IsNullOrWhiteSpace(UserCommand))
            {
                return false;
            }

            var isCreate = UserCommand == _createCommand;
            var isVote = UserCommand == _upVoteCommand || UserCommand == _downVoteCommand;
            var isExit = UserCommand == _exitCommand;

            if (!(isCreate || isVote || isExit))
            {
                return false;
            }

            return true;
        }

        public static bool CommandIsCreate()
        {
            return UserCommand == _createCommand;
        }

        public static bool CommandIsUpVote()
        {
            return UserCommand == _upVoteCommand;
        }

        public static bool CommandIsDownVote()
        {
            return UserCommand == _downVoteCommand;
        }

        public static bool CommandIsExit()
        {
            return UserCommand == _exitCommand;
        }

        public static Post CreatePost(Post post)
        {
            var title = "My first post on Stack Overflow!";
            var description = "Some insteresting stupid queston.";

            if (post == null)
            {
                if (ProgramController.CommandIsCreate())
                {
                    post = new Post(title, description);
                    System.Console.WriteLine("Post '{0}' is created.", post.Title);
                }
            }
            else if (ProgramController.CommandIsCreate())
            {
                System.Console.WriteLine("Post '{0}' is already created", post.Title);
            }

            return post;
        }

        public static void VoteForPost(Post post)
        {
            if (post != null)
            {
                ProcessUpVote(post);
                ProcessDownVote(post);
            }
            else if (CommandIsUpVote() || CommandIsDownVote())
            {
                System.Console.WriteLine("Please, create a new post.");
            }
        }
        #endregion
    }
}