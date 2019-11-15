using System;

namespace StackOverflowPost
{
    public class NewPostArgs : EventArgs
    {
        public Post Post { get; set; }
        public string Title { get; set; }
    }

    public class PostEditor : IPostEditor
    {
        public void CreatePost(string title, string description)
        {
            var newPost = new Post(title, description);
            OnPostCreation(newPost);
        }

        public delegate void PostCreationHandler(object source, NewPostArgs args);
        public event PostCreationHandler PostCreation;
        protected virtual void OnPostCreation(Post post)
        {
            if (PostCreation != null)
            {
                PostCreation(this, new NewPostArgs() { Post = post, Title = post.Title });
            }
        }
    }
}