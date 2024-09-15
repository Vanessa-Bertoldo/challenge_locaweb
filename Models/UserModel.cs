﻿using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.Net;

namespace Challenge_Locaweb.Models
{
    public class UserModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("_id")]
        public string Id { get; set; }
        [BsonElement("fullName")]
        public string FullName { get; set; }

        [BsonElement("email")]
        public string Email { get; set; }

        [BsonElement("password")]
        public string Password { get; set; }

        [BsonElement("phoneNumber")]
        public string PhoneNumber { get; set; }

        [BsonElement("birthDate")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime BirthDate { get; set; }

        [BsonElement("isEmailVerified")]
        public bool IsEmailVerified { get; set; } = false;

        [BsonElement("registrationDate")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime RegistrationDate { get; set; } = DateTime.Now;

        [BsonElement("isActive")]
        public bool IsActive { get; set; } = true;
    }
}
