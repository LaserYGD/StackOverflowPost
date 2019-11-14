using System;

namespace StackOverflowPost
{
    public class Post : IPostController
    {
        private readonly DateTime _creationDate = new DateTime();
        private int _votesCount = 0;

        public string Title { get; private set; }
        public string Description { get; private set; }

        public DateTime CreationDate
        {
            get { return _creationDate; }
        }

        public int Votes
        {
            get { return _votesCount; }
        }

        public Post(string title, string description)
        {
            Title = title;
            Description = description;
        }

        public void UpVote()
        {
            _votesCount++;
        }

        public void DownVote()
        {
            _votesCount--;
        }
    }
}