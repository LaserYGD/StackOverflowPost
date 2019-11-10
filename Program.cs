using System;

namespace StackOverflowPost
{
    class Program
    {
        static void Main(string[] args)
        {
            var title = "My first post on Stack Overflow!";
            var description = "Some insteresting stupid queston.";

            var post = new Post(title, description);
        }
    }
}
