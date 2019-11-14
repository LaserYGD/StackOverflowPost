using System;

namespace StackOverflowPost
{
    public class ProgramController
    {
        private string _userCommand;
        private const string _createCommand = "create";
        private const string _upVoteCommand = "+1";
        private const string _downVoteCommand = "-1";
        private const string _exitCommand = "exit";
        private IPostEditor _postEditor;
        private IPostController _postController;

        #region Properties
        public string UserCommand
        {
            get { return _userCommand; }
        }

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

        public IPostController RegisterPost
        {
            set { _postController = value; }
        }
        #endregion

        public ProgramController(IPostEditor postEditor)
        {
            _postEditor = postEditor;
        }

        #region Private Methods
        private void GetUserCommand()
        {
            var input = Console.ReadLine();
            _userCommand = input.ToLower();
        }

        private bool CommandIsValid()
        {
            if (String.IsNullOrWhiteSpace(_userCommand))
            {
                return false;
            }

            var isCreate = _userCommand == _createCommand;
            var isVote = _userCommand == _upVoteCommand || _userCommand == _downVoteCommand;
            var isExit = _userCommand == _exitCommand;

            if (!(isCreate || isVote || isExit))
            {
                return false;
            }

            return true;
        }

        private string EnterTitle()
        {
            return Console.ReadLine();
        }

        private string EnterDescription()
        {
            return Console.ReadLine();
        }

        private void ProcessUserCommand()
        {
            GetUserCommand();

            var isValid = CommandIsValid();

            if (isValid)
            {
                switch (_userCommand)
                {
                    case (_createCommand):
                        OnCreateCommand();
                        System.Console.WriteLine("Please enter title");
                        var title = EnterTitle();
                        System.Console.WriteLine("Please enter description");
                        var description = EnterDescription();
                        _postEditor.CreatePost(title, description);
                        break;

                    case (_upVoteCommand):
                        OnUpVoteCommand();
                        _postController.UpVote();
                        break;

                    case (_downVoteCommand):
                        OnDownVoteCommand();
                        _postController.DownVote();
                        break;

                    case (_exitCommand):
                        OnExitCommand();
                        break;
                }
            }
            else
            {
                OnInvalidCommand();
            }
        }
        #endregion

        #region Events
        public event EventHandler InvalidCommand;

        protected virtual void OnInvalidCommand()
        {
            if (InvalidCommand != null)
            {
                InvalidCommand(this, EventArgs.Empty);
            }
        }

        public event EventHandler CommandCreate;

        protected virtual void OnCreateCommand()
        {
            if (CommandCreate != null)
            {
                CommandCreate(this, EventArgs.Empty);
            }
        }

        public event EventHandler CommandUpVote;

        protected virtual void OnUpVoteCommand()
        {
            if (CommandUpVote != null)
            {
                CommandUpVote(this, EventArgs.Empty);
            }
        }

        public event EventHandler CommandDownVote;

        protected virtual void OnDownVoteCommand()
        {
            if (CommandDownVote != null)
            {
                CommandDownVote(this, EventArgs.Empty);
            }
        }

        public event EventHandler CommandExit;

        protected virtual void OnExitCommand()
        {
            if (CommandExit != null)
            {
                CommandExit(this, EventArgs.Empty);
            }
        }
        #endregion

        #region Public methods
        public void ProcessSite()
        {
            while (true)
            {
                ProcessUserCommand();

                if (!CommandIsValid())
                {
                    continue;
                }

                if (_userCommand == _exitCommand)
                {
                    break;
                }
            }
        }

        public void OnPostCreation(object source, EventArgs empty)
        {
            _postController = _postEditor.NewPost;
            System.Console.WriteLine("Post registered");
        }
        #endregion
    }
}