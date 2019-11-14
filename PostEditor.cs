using System;

namespace StackOverflowPost
{
    public class PostEditor : IPostEditor
    {
        private Post _newPost;

        public Post NewPost
        {
            get { return _newPost; }
        }

        public void CreatePost(string title, string description)
        {
            _newPost = new Post(title, description);
            System.Console.WriteLine("PostCreated");
            OnPostCreation();
        }

        public event EventHandler PostCreation;

        protected virtual void OnPostCreation()
        {
            if (PostCreation != null)
            {
                PostCreation(this, EventArgs.Empty);
            }
        }
    }
}