using System.Runtime.Serialization;

namespace GitHubRepos{
    [DataContract(Name="repo")]
    public class GitHubRepo{

        [DataMember(Name="name")]
        public string Name;

        [DataMember(Name="language")]
        public string Language;
        [DataMember(Name="stargazers_count")]
        public int Stars;
    }
}