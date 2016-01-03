using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webcorp.common;
using System.Reactive.Linq;



namespace Webcorp.Model
{
    [CollectionName("user")]
    public class Utilisateur : ErpEntity
    {
  
        string _iden,_pwd, _nom, _prenom, _email;

        public Utilisateur()
        {
            _iden = _nom = _prenom = _email = string.Empty;
            ShouldDispose(Roles.CountChanged.Subscribe(_ => { IsChanged = true; }));
        }

        [BsonId(IdGenerator =typeof(UserIdGenerator))]
        public override string Id
        {
            get;
            set;
        }
        [BsonElement("identifiant"),BsonRequired]
        public string Identifiant  { get { return _iden; } set { this.SetAndRaise(ref _iden, value); } }
        [BsonElement("pwd"), BsonRequired]
        public string Password { get { return _pwd; } set { this.SetAndRaise(ref _pwd, value); } }
        [BsonElement("nom")]
        public string Nom { get { return _nom; } set { this.SetAndRaise(ref _nom, value); } } 
        [BsonElement("prenom")]
        public string Prenom { get { return _prenom; } set { this.SetAndRaise(ref _prenom, value); } }
        [BsonElement("email")]
        public string Email { get { return _email; } set { this.SetAndRaise(ref _email, value); } } 
        [BsonElement("roles")]
        public ReactiveList<string> Roles { get; set; } =new ReactiveList<string>();
        
    }

    public class UserIdGenerator : IIdGenerator
    {
        public object GenerateId(object container, object document)
        {
            var art = document as Utilisateur;
            return string.Format("{0}-{1}", art.Societe, art.Identifiant);
        }

        public bool IsEmpty(object id)
        {
            return id == null || String.IsNullOrEmpty(id.ToString());
        }
    }
}
