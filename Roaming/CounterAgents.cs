namespace Roaming;

public class CounterAgents
{
    public string title;
    public string inn;
    public string kpp;
    public int label; //номер в списке 
    public string fullName;
    public string email;
    public string telephone;


    public CounterAgents(string title = null, string inn = null, string kpp = null, int label = default,
        string fullName = null, string email = null, string telephone = null)
    {
        this.title = title ?? throw new ArgumentNullException(nameof(title));
        this.inn = inn ?? throw new ArgumentNullException(nameof(inn));
        this.kpp = kpp ?? throw new ArgumentNullException(nameof(kpp));
        this.label = label;
        this.fullName = fullName ?? throw new ArgumentNullException(nameof(fullName));
        this.email = email ?? throw new ArgumentNullException(nameof(email));
        this.telephone = telephone ?? throw new ArgumentNullException(nameof(telephone));
    }
}