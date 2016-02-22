using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace LocallySourced.Models
{
    //Used out of convience to separate concerns and remove bloat
    
    [MetadataType(typeof(MemberMetaData))]
    public partial class Member { }

    [MetadataType(typeof(MessageMetaData))]
    public partial class Message { }
    
}