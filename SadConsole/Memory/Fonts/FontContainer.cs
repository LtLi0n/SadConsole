using System;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SadConsole
{
    public class FontContainer : ContainerBase<Font>
    {
        public FontContainer(IServiceProvider services) : base(services) { }

        public async Task AddFromJsonAsync(string path)
        {
            string jsonStr = await File.ReadAllTextAsync(path);

            IFontInformation fontInfo = JsonConvert.DeserializeObject<FontInformation>(jsonStr);
            _items.Add(fontInfo.Name, new Font(fontInfo));
        }

        public Task LoadAllAsync()
        {
            foreach(var font in _items)
            {
                font.Value.Load(_services);
            }

            return Task.CompletedTask;
        }
    }
}
