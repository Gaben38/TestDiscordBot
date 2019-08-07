using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Syndication;
using System.Threading.Tasks;
using System.Xml;

namespace MyDiscordBot.Infrastructure
{
    internal class RssReader
    {
        private string FeedUrl { get; }

        public RssReader(string feedUrl)
        {
            FeedUrl = feedUrl;
        }

        public async Task<IEnumerable<SyndicationItem>> ReadAsync()
        {
            using (var reader = XmlReader.Create(FeedUrl))
            {
                var feed = SyndicationFeed.Load(reader);
                return feed.Items.ToList();
            }
        }
    }
}
