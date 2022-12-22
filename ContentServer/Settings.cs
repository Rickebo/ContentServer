namespace ContentServer;

public class Settings
{
    public string ContentDirectory { get; set; } = "content";
    public string KeyFile { get; set; } = "key.txt";
    public string SaltFile { get; set; } = "salt.txt";

    public static Settings TemplateSettings { get; } = new ();
}