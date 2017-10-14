using System;
using Avalonia;

namespace GitHubRepos
{
    class Program
    {
        static void Main(string[] args)
        {
            AppBuilder
                .Configure<App>()
                .UsePlatformDetect()
                .Start<MainWindow>();
        }
    }
}
