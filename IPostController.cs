namespace StackOverflowPost
{
    public interface IPostController
    {
        public void UpVote();
        public void DownVote();
        public int Votes { get; }
    }
}