using System;
using LiteDB;

namespace LiteDBSamples
{
    internal class Customer
    {
        //[BsonId]
        public Guid No { get; set; }

        public string Name { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }

        public DateTime Birthday { get; set; }

        public int Code { get; set; }
    }
}