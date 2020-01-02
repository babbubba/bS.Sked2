using bS.Sked2.Model;
using FluentNHibernate.Mapping;

namespace bS.Sked2.Extensions.Common.Models
{
    public class CommonModuleEntry : ElementEntity
    {
        public CommonModuleEntry()
        {
            Key = "Common";
            Name = "Common Module";
            Description = "This module contains the common elements.";
        }

        //public virtual string SourceFilePath { get; set; }
        //public virtual int? SkipStartingDataRows { get; set; }
        //public virtual bool FirstRowHasHeader { get; set; }
        //public virtual string ColumnDelimiter { get; set; }
        //public virtual int? LimitToRows { get; set; }

    }

    class CommonModuleEntityMap : SubclassMap<CommonModuleEntry>
    {
        public CommonModuleEntityMap()
        {
            DiscriminatorValue("Common");
            //Map(x => x.SourceFilePath);
            //Map(x => x.SkipStartingDataRows);
            //Map(x => x.FirstRowHasHeader);
            //Map(x => x.ColumnDelimiter);
            //Map(x => x.LimitToRows);
        }
    }
}
