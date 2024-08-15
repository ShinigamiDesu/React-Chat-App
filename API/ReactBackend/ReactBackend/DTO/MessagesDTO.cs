using ReactBackend.Entities;

namespace ReactBackend.DTO
{
    public class MessagesDTO
    {
        public int Id { get; set; }
        public int SenderID { get; set; }
        public string message { get; set; }
        public DateTime message_date { get; set; }


        public static MessagesDTO MapToDto(Messages Message)
        {
            return new MessagesDTO
            {
                Id = Message.message_ID,
                SenderID = Message.message_fromID,
                message = Message.message,
                message_date = Message.message_date
            };
        }
    }
}
