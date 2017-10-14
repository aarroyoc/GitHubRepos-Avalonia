using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Collections.Generic;
using System.Runtime.Serialization.Json;
using System.Linq;

namespace GitHubRepos
{
    public class MainWindow : Window
    {
        Button refresh;
        TextBox username;
        TextBlock status;
        ListBox repos;
        public MainWindow()
        {
            Initialize();

            refresh = this.Find<Button>("refresh");
            refresh.Click += RefreshList;

            username = this.Find<TextBox>("username");
            status = this.Find<TextBlock>("status");
            repos = this.Find<ListBox>("repos");
        }
        private void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private async void RefreshList(object sender, RoutedEventArgs e)
        {
            var user = username.Text;
            status.Text = $"Obteniendo repositorios de {user}";
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            client.DefaultRequestHeaders.Add("User-Agent", "GitHubRepos - Avalonia");
            try{
                var downloadTask = client.GetStreamAsync($"https://api.github.com/users/{user}/repos");

                var serializer = new DataContractJsonSerializer(typeof(List<GitHubRepo>));
                var repoList = serializer.ReadObject(await downloadTask) as List<GitHubRepo>;

                repos.Items = repoList.OrderByDescending(t => t.Stars).Select(repo => {
                    var item = new ListBoxItem();
                    item.Content=$"{repo.Name} - {repo.Language} - {repo.Stars}";
                    return item;
                });
                status.Text = $"Repositorios de {user} cargados";
            }catch(HttpRequestException){
                status.Text = "Hubo un error en la petición HTTP";
            }catch(System.Runtime.Serialization.SerializationException){
                status.Text = "El fichero JSON es incorrecto";
            }
        }
    }
}