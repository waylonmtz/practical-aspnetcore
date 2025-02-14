using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder();
builder.Configuration.SetBasePath(Directory.GetCurrentDirectory()).AddIniFile("settings.ini");
builder.Services.AddOptions();
builder.Services.Configure<XmlOptions>(builder.Configuration);

var app = builder.Build();

app.Run(async context =>
{
    var options = context.RequestServices.GetService<IOptions<XmlOptions>>().Value;

    var res = context.Response;

    await res.WriteAsync($"webpages:value : {options.WebPages.Value}\n");
    await res.WriteAsync($"config.password : {options.Config.Password}\n");
    await res.WriteAsync($"config.username : {options.Config.Username}\n");
    await res.WriteAsync($"config.server : {options.Config.Server}\n");
    await res.WriteAsync($"config.port : {options.Config.Port}\n");
    await res.WriteAsync($"config.googleMap : {options.GoogleMap}\n");
    await res.WriteAsync($"app:password : {options.App.Password} \n");
    await res.WriteAsync($"app:user : {options.App.User} \n");
    await res.WriteAsync($"app:priorities:task : {options.App.Priorities.Task} \n");
    await res.WriteAsync($"app:priorities:limit : {options.App.Priorities.Limit} \n");
    await res.WriteAsync($"app:privacy:individual:sharedKey : {options.App.Privacy.Individual.SharedKey}\n");
    await res.WriteAsync($"app:privacy:individual:publicKey : {options.App.Privacy.Individual.PublicKey}\n");
    await res.WriteAsync($"app:privacy:organization : {options.App.Privacy.Organization}\n");
});

app.Run();

public class XmlOptions
{
    public XmlOptionsWebPages WebPages { get; set; }

    public class XmlOptionsWebPages
    {
        public string Value { get; set; }
    }

    public XmlOptionsConfig Config { get; set; }

    public class XmlOptionsConfig
    {
        public string Password { get; set; }
        public string Username { get; set; }

        public string Server { get; set; }

        public int Port { get; set; }
    }

    public string GoogleMap { get; set; }

    public XmlOptionsApp App { get; set; }

    public class XmlOptionsApp
    {
        public string Password { get; set; }

        public string User { get; set; }

        public XmlOptionsAppPriorities Priorities { get; set; }

        public class XmlOptionsAppPriorities
        {
            public int Task { get; set; }

            public int Limit { get; set; }
        }

        public XmlOptionsPrivacy Privacy { get; set; }

        public class XmlOptionsPrivacy
        {
            public XmlOptionsPrivacyKeys Individual { get; set; }

            public string Organization { get; set; }

            public class XmlOptionsPrivacyKeys
            {
                public string SharedKey { get; set; }

                public string PublicKey { get; set; }
            }
        }
    }
}

