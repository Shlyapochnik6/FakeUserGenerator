namespace FakeUserGenerator.Domain;

public class Country
{
    public long Id { get; set; }
    public string Name { get; set; }
    
    public List<Village> Villages { get; set; }
}