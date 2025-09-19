using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.DataAccess.Enums;

namespace UserService.DataAccess.Entities
{
    /// <summary>
    /// Сообщиние для RabbitMQ
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PublishMassage<T> where T : class
    {
        /// <summary>
        /// сущность 
        /// </summary>
        public T Entity { get; set; }
        /// <summary>
        /// Время 
        /// </summary>
        public DateTime RaisedOn { get;  set; }
        /// <summary>
        /// Действие 
        /// </summary>
        public MessageAction Action { get; set; } = MessageAction.UnKnown;

        public string Message { get; set; }
        public PublishMassage(T enitity,DateTime raiseon,MessageAction messageAction,string message="") { 
            Entity = enitity;
            Action = messageAction;
            RaisedOn = raiseon;
            Message = message;
        }
    }
}
