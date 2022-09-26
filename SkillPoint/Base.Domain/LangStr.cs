namespace Base.Domain;

public class LangStr : Dictionary<string, string>
{

    private const string DefaultCulture = "en";
    
    public LangStr() : this("", Thread.CurrentThread.CurrentUICulture.Name)
    {
    }
    
    public LangStr(string value) : this(value, Thread.CurrentThread.CurrentUICulture.Name)
    {
    }
    
    public LangStr(string value, string  culture)
    {
        this[culture] = value;
    }

    public string? Translate(string? culture = null)
    {
        if (this.Count == 0) return null;
        culture = culture?.Trim() ?? Thread.CurrentThread.CurrentUICulture.Name;

        // object - query
        
        // en-GB - en-GB
        if (this.ContainsKey(culture))
        {
            return this[culture];
        }

        // en - en-US // no US => neutral en
        var neutralCulture = culture.Split("-")[0];
        if (this.ContainsKey(neutralCulture))
        {
            return this[neutralCulture];
        }
        
        // en - RU // no RU => default en
        if (this.ContainsKey(DefaultCulture))
        {
            return this[DefaultCulture];
        }
        
        // no culture
        return null;
    }

    public void SetTranslation(string value)
    {
        this[Thread.CurrentThread.CurrentUICulture.Name] = value;
    }
    
    public override string ToString()
    {
        return Translate() ?? "???";
    }

    // string xxx = new LangStr("zzz");
    public static implicit operator string(LangStr? langStr) => langStr?.ToString() ?? "null";
    
    // LangStr lstr = "xxx" ; // internally it will be lStr = new LangStr("xxx");
    public static implicit operator LangStr(string value) => new ();
}