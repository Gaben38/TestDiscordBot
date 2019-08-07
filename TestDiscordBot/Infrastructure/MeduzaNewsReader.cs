using System;
using System.Collections.Generic;
using System.ServiceModel.Syndication;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace MyDiscordBot.Infrastructure
{
    internal class MeduzaNewsReader
    {
        public string NewsFeedAddress { get; } = @"https://meduza.io/rss/all";
        public async Task<IEnumerable<SyndicationItem>> GetNewsAsync(int? maxArticles = 30)
        {
            var reader = new RssReader(NewsFeedAddress);
            return (await reader.ReadAsync()).Take(maxArticles.Value);
        }
    }
}
