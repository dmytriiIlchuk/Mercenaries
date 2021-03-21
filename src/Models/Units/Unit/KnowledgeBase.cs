using System.Collections.Generic;

public class KnowledgeBase
{
    public IList<Unit> Units { get; private set; }

    public KnowledgeBase(IList<Unit> units)
    {
        this.Units = units;
    }

    public KnowledgeBase()
    {
        this.Units = new List<Unit>();
    }
}