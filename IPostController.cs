namespace StackOverflowPost
{
    public interface IPostController
    {
        public int Votes { get; }
        public void UpVote();
        public void DownVote();
    }
}