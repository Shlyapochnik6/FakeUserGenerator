namespace FakeUserGenerator.Domain;

public class Village
{
    public long Id { get; set; }
    public string Name { get; set; }
    
    public Country Country { get; set; }
}