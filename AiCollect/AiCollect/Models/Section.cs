using AICollect;
using System;
using System.Collections.Generic;
using System.Text;

namespace AiCollect.Models
{
    public class Section : DataCollectionObject
    {
        public string Description { get; set; }
        public List<Question> Questions { get; set; }
        public List<SubSection> SubSections { get; set; }
    }
}
