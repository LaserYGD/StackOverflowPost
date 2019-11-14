namespace StackOverflowPost
{
    public interface IPostEditor
    {
        public void CreatePost(string title, string description);
        public Post NewPost{ get; }
    }
}