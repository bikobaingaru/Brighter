﻿namespace Paramore.Brighter.Tests.CommandProcessors.TestDoubles
{
    public class MyResponse : Reply
    {
        public string ReplyValue { get; set; }
        
        public MyResponse(ReplyAddress sendersAddress) : base(sendersAddress) {}
        

   }
}
