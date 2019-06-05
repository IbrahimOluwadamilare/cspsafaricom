using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace CSBGlobal.Helpers
{
    public  class EmailTemplateHelper
    {
        private IHostingEnvironment _env { get; set; }
        public EmailTemplateHelper(IHostingEnvironment env)
        {
            _env = env;
        }
        public string GetTemplate(string MaitoGet)
        {

            var path = Path.Combine(_env.WebRootPath, ("EmailTemplates\\"  + MaitoGet + ".html"));

            return File.ReadAllText(path);
        }
    }
}

