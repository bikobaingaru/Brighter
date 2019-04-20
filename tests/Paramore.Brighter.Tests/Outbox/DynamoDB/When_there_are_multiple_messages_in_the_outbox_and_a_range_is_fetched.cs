﻿#region Licence
/* The MIT License (MIT)
Copyright © 2015 Ian Cooper <ian_hammond_cooper@yahoo.co.uk>

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the “Software”), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE. */

#endregion

using System;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace Paramore.Brighter.Tests.Outbox.DynamoDB
{
    [Trait("Category", "DynamoDB")]
    [Collection("DynamoDB OutBox")]
    public class DynamoDbOutboxRangeRequestTests : BaseDynamoDBOutboxTests
    {
        private readonly string _TopicFirstMessage = "test_topic";
        private readonly string _TopicLastMessage = "test_topic3";

        public DynamoDbOutboxRangeRequestTests()
        {
            var messageEarliest = new Message(new MessageHeader(Guid.NewGuid(), _TopicFirstMessage, MessageType.MT_DOCUMENT), new MessageBody("message body"));
            var message1 = new Message(new MessageHeader(Guid.NewGuid(), "test_topic2", MessageType.MT_DOCUMENT), new MessageBody("message body2"));
            var message2 = new Message(new MessageHeader(Guid.NewGuid(), _TopicLastMessage, MessageType.MT_DOCUMENT), new MessageBody("message body3"));

            DynamoDbOutbox.Add(messageEarliest);
            Task.Delay(100);
            DynamoDbOutbox.Add(message1);
            Task.Delay(100);
            DynamoDbOutbox.Add(message2);
        }

        [Fact]
        public void When_there_are_multiple_messages_in_the_outbox_and_a_range_is_fetched()
        {
            var exception = Catch.Exception(() => DynamoDbOutbox.GetAsync(1, 3).Wait());
            exception.Should().BeOfType<NotSupportedException>();
        }
    }
}
