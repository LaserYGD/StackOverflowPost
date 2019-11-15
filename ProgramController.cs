using System;
using System.Collections.Generic;

namespace StackOverflowPost
{
    public class PostHandlerArgs : EventArgs
    {
        public Post Post { get; set; }
        public string Title { get; set; }
        public int Votes { get; set; }
    }

    public class ProgramController
    {
        private string _userCommand;
        private const string _createCommand = "create";
        private const string _upVoteCommand = "+1";
        private const string _downVoteCommand = "-1";
        private const string _votesCommand = "votes";
        private const string _exitCommand = "exit";
        private List<string> commands;
        private IPostEditor _postEditor;
        private Post _currentPost;
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

        public static string VotesCommand
        {
            get { return _votesCommand; }
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
            commands = new List<string>();
        }

        #region Private Methods
        private void ProcessUserCommand()
        {
            FormListOfCommands();

            GetUserCommand();

            var isValid = CommandIsValid();

            if (isValid)
            {
                switch (_userCommand)
                {
                    case (_createCommand):
                        ProcessCreateCommand();
                        break;

                    case (_upVoteCommand):
                        ProcessUpVoteCommand();
                        break;

                    case (_downVoteCommand):
                        ProcessDownVote();
                        break;

                    case (_votesCommand):
                        ProcessVotesCommand();
                        break;

                    case (_exitCommand):
                        ProcessExitCommand();
                        break;
                }
            }
            else
            {
                OnInvalidCommand();
            }
        }

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

            foreach (var command in commands)
            {
                if (_userCommand == command)
                {
                    return true;
                }
            }

            return false;
        }

        private void FormListOfCommands()
        {
            if (commands.Count == 0)
            {
                commands.Add(_createCommand);
                commands.Add(_upVoteCommand);
                commands.Add(_downVoteCommand);
                commands.Add(_votesCommand);
                commands.Add(_exitCommand);
            }
        }

        private string EnterData()
        {
            return Console.ReadLine();
        }

        private void ProcessDownVote()
        {
            if (_postController != null)
            {
                OnDownVoteCommand(_currentPost);
                _postController.DownVote();
            }
            else
            {
                OnPostNotFound();
            }
        }

        private void ProcessUpVoteCommand()
        {
            if (_postController != null)
            {
                OnUpVoteCommand(_currentPost);
                _postController.UpVote();
            }
            else
            {
                OnPostNotFound();
            }
        }

        private void ProcessCreateCommand()
        {
            if (_postController == null)
            {
                OnCreateCommand();

                OnCreateTitle();
                var title = EnterData();

                OnCreateDescription();
                var description = EnterData();

                _postEditor.CreatePost(title, description);
            }
            else
            {
                OnPostExists();
            }
        }

        private void ProcessVotesCommand()
        {
            if (_currentPost != null)
            {
                OnShowVotes(_currentPost);
            }
            else
            {
                OnPostNotFound();
            }
        }

        private void ProcessExitCommand()
        {
            if (_currentPost != null)
            {
                OnShowVotes(_currentPost);
            }

            OnExitCommand();
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

        public delegate void PostDataHandler(object source, PostHandlerArgs args);
        public event PostDataHandler CommandUpVote;
        protected virtual void OnUpVoteCommand(Post post)
        {
            if (CommandUpVote != null)
            {
                CommandUpVote(this, new PostHandlerArgs() { Title = post.Title });
            }
        }

        public event PostDataHandler CommandDownVote;
        protected virtual void OnDownVoteCommand(Post post)
        {
            if (CommandDownVote != null)
            {
                CommandDownVote(this, new PostHandlerArgs() { Title = post.Title });
            }
        }

        public event PostDataHandler ShowVotes;
        protected virtual void OnShowVotes(Post post)
        {
            if (ShowVotes != null)
            {
                ShowVotes(this, new PostHandlerArgs() { Title = post.Title, Votes = post.Votes });
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

        public event EventHandler CreateTitle;
        protected virtual void OnCreateTitle()
        {
            if (CreateTitle != null)
            {
                CreateTitle(this, EventArgs.Empty);
            }
        }

        public event EventHandler CreateDescription;

        protected virtual void OnCreateDescription()
        {
            if (CreateDescription != null)
            {
                CreateDescription(this, EventArgs.Empty);
            }
        }

        public event EventHandler PostExists;

        protected virtual void OnPostExists()
        {
            if (PostExists != null)
            {
                PostExists(this, EventArgs.Empty);
            }
        }

        public event EventHandler PostNotFound;
        protected virtual void OnPostNotFound()
        {
            if (PostNotFound != null)
            {
                PostNotFound(this, EventArgs.Empty);
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

        public void OnPostCreation(object source, NewPostArgs args)
        {
            _currentPost = args.Post;
            _postController = args.Post;
        }
        #endregion
    }
}